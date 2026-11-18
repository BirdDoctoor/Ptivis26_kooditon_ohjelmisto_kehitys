
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class MathManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public AnswerBlock[] answerBlocks;
    public GameObject barrier;

    private int correctAnswer;
    private bool questionActive = true;

    // List of addition and subtraction questions
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

    void Awake()
    {
        // Find the barrier inside this MathManager
        foreach (Transform child in GetComponentsInChildren<Transform>(true))
        {
            if (child.name == "Barrier")
            {
                barrier = child.gameObject;
                break;
            }
        }

        // Check if the barrier was found
        if (barrier != null)
        {
            Debug.Log(gameObject.name + " controls " + barrier.name);
        }
        else
        {
            Debug.LogError("No Barrier found for " + gameObject.name);
        }
    }

    void Start()
    {
        // Check if all references are assigned
        if (questionText == null)
        {
            Debug.LogError("Question Text is not assigned!");
            return;
        }

        if (answerBlocks == null || answerBlocks.Length < 3)
        {
            Debug.LogError("You need to assign 3 Answer Blocks!");
            return;
        }

        if (barrier == null)
        {
            Debug.LogError("Barrier is missing!");
            return;
        }

        GenerateQuestion();
    }

    // Generate a random question and three answers
    void GenerateQuestion()
    {
        string calculation = calculations[
            Random.Range(0, calculations.Count)
        ];

        // Split the calculation into two numbers
        string[] numbers = calculation.Split(
            new char[] { '+', '-' }
        );

        int number1 = int.Parse(numbers[0]);
        int number2 = int.Parse(numbers[1]);

        // Calculate the correct answer
        if (calculation.Contains("+"))
            correctAnswer = number1 + number2;
        else
            correctAnswer = number1 - number2;

        questionText.text = calculation + " = ?";

        // Choose a random block for the correct answer
        int correctBox = Random.Range(0, 3);

        List<int> usedAnswers = new List<int>();
        usedAnswers.Add(correctAnswer);
        for (int i = 0; i < 3; i++)
        {
            int answer;

            if (i == correctBox)
            {
                answer = correctAnswer;
            }
            else
            {
                // Generate a unique wrong answer
                do
                {
                    answer = Random.Range(0, 20);
                }
                while (usedAnswers.Contains(answer));
            }

            usedAnswers.Add(answer);

            // Set the answer and show the block
            answerBlocks[i].SetAnswer(
                answer,
                answer == correctAnswer
            );

            answerBlocks[i].gameObject.SetActive(true);
        }
    }

    // Check the player's answer
    public void CheckAnswer(bool isCorrect)
    {
        if (!questionActive)
            return;

        if (isCorrect)
        {
            questionActive = false;

            // Remove the barrier
            barrier.SetActive(false);

            questionText.text = "CORRECT!";

            // Hide all answer blocks
            foreach (AnswerBlock block in answerBlocks)
            {
                block.gameObject.SetActive(false);
            }

            Debug.Log("Barrier removed!");
        }
        else
        {
            Debug.Log("Wrong answer! Try again.");
        }
    }
}
