using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCharacterSelectorManager : Singleton<PlayerCharacterSelectorManager>
{
    public List<PlayerCharacterID> allCharacters = new List<PlayerCharacterID>();

    public AnimatorOverrideController GetCharacterOverride(int ID)
    {
        PlayerCharacterID playerCharacter = allCharacters.FirstOrDefault(pc => pc.ID == ID);
        if(playerCharacter == null)
        {
            Debug.LogError($"Can't find the Player Character with ID:{ID}");
            return null;
        }

        return playerCharacter.characterOverride;
    }
}
