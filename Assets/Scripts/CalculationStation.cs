
using UnityEngine;

public class CalculationStation : MonoBehaviour
{
    public GameObject barrier;

    public bool completed = false;

    public void CompleteStation()
    {
        if (completed)
            return;

        completed = true;

        // Remove the barrier
        barrier.SetActive(false);

        Debug.Log("Station completed!");
    }
}
