
using UnityEngine;
using UMA.CharacterSystem;

public class UMAHairCustomizer : MonoBehaviour
{
    public DynamicCharacterAvatar avatar;

    [Header("Acconciatura")]
    public UMAWardrobeRecipe longHair01;

    public void ApplyLongHair()
    {
        if (avatar == null || longHair01 == null)
        {
            Debug.LogWarning("Avatar o ricetta non assegnati!");
            return;
        }

        avatar.SetSlot(longHair01);
        avatar.BuildCharacter();

        Debug.Log("Acconciatura applicata: Long Hair 01");
    }
}

