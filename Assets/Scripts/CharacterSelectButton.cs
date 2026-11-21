
using UnityEngine;

public class CharacterSelectButton : MonoBehaviour
{
    public int characterIndex;

    public void SelectCharacter()
    {
        CharacterManager.Instance.SelectCharacter(characterIndex);
        Debug.Log("Character selected: " + characterIndex);
    }
}
