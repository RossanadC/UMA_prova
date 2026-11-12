using System.Collections;
using UnityEngine;
using UMA.CharacterSystem;

public class UMADNADebug : MonoBehaviour
{
    public DynamicCharacterAvatar avatar;

    private IEnumerator Start()
    {
        // Aspetta che UMA abbia generato il personaggio
        yield return new WaitUntil(
            () => avatar != null && avatar.umaData != null
                && avatar.umaData.umaRecipe != null
        );

        var dna = avatar.GetDNA();

        foreach (var entry in dna)
        {
            Debug.Log("DNA disponibile: " + entry.Key);
        }
    }
}
