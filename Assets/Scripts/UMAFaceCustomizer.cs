
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UMA.CharacterSystem;

public class UMAFaceCustomizer : MonoBehaviour
{
    [Header("Avatar da personalizzare")]
    public DynamicCharacterAvatar avatar;

    [Header("Contenitore degli slider")]
    public Transform slidersRoot;

    private readonly Dictionary<string, string> parameters =
        new Dictionary<string, string>()
    {
        // TESTA
        { "HeadSizeSlider", "headSize" },
        { "HeadWidthSlider", "headWidth" },
        { "ForeheadSizeSlider", "foreheadSize" },
        { "CheekSizeSlider", "cheekSize" },

        // MANDIBOLA
        { "MandibleSizeSlider", "mandibleSize" },
        { "JawSizeSlider", "jawsSize" },
        { "ChinSizeSlider", "chinSize" },

        // OCCHI
        { "EyeSizeSlider", "eyeSize" },
        { "EyeSpacingSlider", "eyeDistance" },
        { "EyeRotationSlider", "eyeRotation" },

        // NASO
        { "NoseSizeSlider", "noseSize" },
        { "NoseWidthSlider", "noseWidth" },
        { "NosePositionSlider", "nosePosition" },

        // BOCCA
        { "MouthSizeSlider", "mouthSize" },
        { "LipsSizeSlider", "lipsSize" }
    };

    private IEnumerator Start()
    {
        if (avatar == null || slidersRoot == null)
        {
            Debug.LogError(
                "UMAFaceCustomizer: assegna Avatar e Sliders Root!"
            );
            yield break;
        }

        // Aspetta la generazione iniziale dell'avatar.
        yield return new WaitUntil(() =>
            avatar.umaData != null &&
            avatar.umaData.umaRecipe != null &&
            avatar.activeRace != null &&
            avatar.activeRace.data != null
        );

        var availableDNA = new HashSet<string>(
            avatar.activeRace.data.GetDNANames()
        );

        // Cerca tutti gli Slider nel contenitore.
        Slider[] sliders =
            slidersRoot.GetComponentsInChildren<Slider>(true);

        foreach (Slider slider in sliders)
        {
            if (!parameters.TryGetValue(
                slider.gameObject.name, out string dnaName))
            {
                continue;
            }

            if (!availableDNA.Contains(dnaName))
            {
                Debug.LogWarning(
                    "DNA non disponibile: " + dnaName
                );
                slider.interactable = false;
                continue;
            }

            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.wholeNumbers = false;

            // Il valore centrale iniziale è provvisorio.
            // Non modifica il DNA finché l'utente non interagisce.
            slider.SetValueWithoutNotify(0.5f);

            string currentDNA = dnaName;

            slider.onValueChanged.AddListener(
                value => SetParameter(currentDNA, value)
            );

            Debug.Log("Slider collegato: " + currentDNA);
        }
    }

    //public void SetParameter(string dnaName, float value)
    //{
    //    if (avatar == null || avatar.umaData == null)
    //        return;

    //    Debug.Log($"MODIFICA DNA: {dnaName} = {value:F2}");

    //    avatar.SetDNA(dnaName, Mathf.Clamp01(value), false);
    //    avatar.ForceUpdate(true, false, false);
    //}



    //public void SetParameter(string dnaName, float value)
    //{
    //    if (avatar == null || avatar.umaData == null)
    //        return;

    //    float newValue = Mathf.Clamp01(value);

    //    avatar.SetDNA(dnaName, newValue, false);
    //    avatar.ForceUpdate(true, false, false);

    //    var currentDNA = avatar.GetDNA();

    //    if (currentDNA.TryGetValue(
    //        dnaName, out UMA.DnaSetter dnaSetter))
    //    {
    //        Debug.Log(
    //            $"DNA {dnaName}: richiesto={newValue:F2}, " +
    //            $"memorizzato={dnaSetter.Value:F2}"
    //        );
    //    }
    //    else
    //    {
    //        Debug.LogWarning(
    //            $"DNA {dnaName} non trovato!"
    //        );
    //    }
    //}


    public void SetParameter(string dnaName, float value)
    {
        if (avatar == null || avatar.umaData == null)
            return;

        avatar.SetDNA(dnaName, Mathf.Clamp01(value), true);
    }



}
