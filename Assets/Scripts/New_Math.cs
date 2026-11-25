using UnityEngine;

public class New_Math : MonoBehaviour
{
    public GameObject Math;
    public GameObject Barrier;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Math.SetActive(true);
            Barrier.SetActive(true);
        }
    }
}