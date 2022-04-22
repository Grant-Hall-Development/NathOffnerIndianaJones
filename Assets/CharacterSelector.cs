using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSelector : Singleton<CharacterSelector>
{
    public List<CharacterData> allCharacters = new List<CharacterData>();

    public CharacterData GetDataAtIndex(int index)
    {
        return allCharacters.FirstOrDefault(c => c.characterIndex == index);
    }
}

[System.Serializable]
public class CharacterData
{
    public AnimatorOverrideController character;
    public int characterIndex;
}
