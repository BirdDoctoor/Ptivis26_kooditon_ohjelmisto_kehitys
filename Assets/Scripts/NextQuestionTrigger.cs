
using UnityEngine;

public class NextQuestionTrigger : MonoBehaviour
{
    public MathManager mathManager;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            mathManager.NextQuestion();
        }
    }
}
