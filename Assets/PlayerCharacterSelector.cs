using System;
using System.Collections;
using UnityEngine;

public class PlayerCharacterSelector : MonoBehaviour
{
    public Animator anim;
    private void Start()
    {
        SetCharacter();
    }

    public void SetCharacter()
    {
        int indexRequired = Convert.ToInt32(PlayerDataGetter.Instance.GetReadDataAtKey(PlayerConstants.CharacterSheet));
        AnimatorOverrideController playerCharacterOverride = PlayerCharacterSelectorManager.Instance.GetCharacterOverride(indexRequired);
        anim.runtimeAnimatorController = playerCharacterOverride;
    }
}

[Serializable]
public class PlayerCharacterID
{
    public int ID;
    public AnimatorOverrideController characterOverride;
}