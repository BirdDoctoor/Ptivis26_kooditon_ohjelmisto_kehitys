
using UnityEngine;
using TMPro;

public class AnswerBlock : MonoBehaviour
{
    public TextMeshProUGUI answerText;

    // Assign the correct MathManager in the Inspector
    public MathManager manager;

    [HideInInspector]
    public int answer;

    private bool isCorrect;
    private bool alreadyHit = false;

    public void SetAnswer(int newAnswer, bool correct)
    {
        answer = newAnswer;
        isCorrect = correct;
        alreadyHit = false;

        answerText.text = answer.ToString();
    }

    public void HitBlock()
    {
        if (alreadyHit)
            return;

        alreadyHit = true;

        // Communicate with the assigned MathManager
        if (manager != null)
        {
            manager.CheckAnswer(isCorrect);
        }
        else
        {
            Debug.LogError("MathManager is not assigned to " + gameObject.name);
        }
    }
}
