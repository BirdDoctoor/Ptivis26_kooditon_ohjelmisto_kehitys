
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    public GameObject[] characters;
    public int selectedCharacter = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetSelectedCharacter()
    {
        return characters[selectedCharacter];
    }

    public void SelectCharacter(int index)
    {
        if (index >= 0 && index < characters.Length)
        {
            selectedCharacter = index;
        }
    }
}
