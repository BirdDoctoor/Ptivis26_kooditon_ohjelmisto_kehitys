
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MathManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public AnswerBlock[] answerBlocks;

    public int score = 0;

    private List<string> calculations = new List<string>()
    {
        "2+3", "4+4", "5+5", "6+3",
        "7+7", "8+2", "10+5", "9+9",
        "11+3", "8+5", "3+4", "6+2",
        "7+1", "9+2", "1+6", "5+9",
        "5-2", "8-4", "10-5", "7-3",
        "9-6", "3-1", "5-4", "8-5",
        "6-2", "4-1"
    };

    private List<string> questionOrder;
    private int correctAnswer;

    void Start()
    {
        ShuffleQuestions();
        GenerateQuestion();
    }

    void ShuffleQuestions()
    {
        questionOrder = new List<string>(calculations);

        for (int i = 0; i < questionOrder.Count; i++)
        {
            int randomIndex = Random.Range(i, questionOrder.Count);

            string temp = questionOrder[i];
            questionOrder[i] = questionOrder[randomIndex];
            questionOrder[randomIndex] = temp;
        }
    }

    public void GenerateQuestion()
    {
        if (questionOrder.Count == 0)
        {
            questionText.text = "You finished all questions!";
            foreach (AnswerBlock block in answerBlocks)
                block.gameObject.SetActive(false);

            Debug.Log("All questions completed! Final score: " + score);
            return;
        }

        string calculation = questionOrder[0];
        questionOrder.RemoveAt(0);

        string[] numbers = calculation.Split(
            new char[] { '+', '-' }
        );

        int number1 = int.Parse(numbers[0]);
        int number2 = int.Parse(numbers[1]);

        if (calculation.Contains("+"))
            correctAnswer = number1 + number2;
        else
            correctAnswer = number1 - number2;

        questionText.text = calculation + " = ?";

        int correctBox = Random.Range(0, 3);

        List<int> usedAnswers = new List<int>();

        for (int i = 0; i < answerBlocks.Length; i++)
        {
            int answer;

            if (i == correctBox)
            {
                answer = correctAnswer;
            }
            else
            {
                do
                {
                    answer = Random.Range(0, 20);
                }
                while (answer == correctAnswer ||
                       usedAnswers.Contains(answer));
            }

            usedAnswers.Add(answer);

            answerBlocks[i].SetAnswer(
                answer,
                answer == correctAnswer
            );

            answerBlocks[i].gameObject.SetActive(true);
        }
    }

    public void CheckAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            score++;
            Debug.Log("Correct! Score: " + score);
        }
        else
        {
            Debug.Log("Wrong answer!");
        }

        GenerateQuestion();
    }
}
