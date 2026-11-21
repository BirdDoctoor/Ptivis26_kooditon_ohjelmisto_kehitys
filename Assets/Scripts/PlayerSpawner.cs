
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        GameObject player = Instantiate(
            CharacterManager.Instance.GetSelectedCharacter(),
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}
