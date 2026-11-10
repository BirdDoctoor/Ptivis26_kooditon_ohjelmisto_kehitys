
using UnityEngine;
using TMPro;

public class AnswerBlock : MonoBehaviour
{
    public TextMeshProUGUI answerText;

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

        MathManager manager =
            FindFirstObjectByType<MathManager>();

        if (manager != null)
        {
            manager.CheckAnswer(isCorrect);
        }
    }
}
