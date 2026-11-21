
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Clock_manager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Clock_Anserw[] answerBlocks;

    [System.Serializable]
    public class ClockAnswer
    {
        public string time;
        public Sprite clockImage;
    }

    public List<ClockAnswer> clockAnswers = new List<ClockAnswer>();

    private string[] questions =
    {
        "7:00",
        "8:00",
        "9:00",
        "11:00",
        "16:00",
        "19:00",
        "21:00"
    };

    private int currentQuestionIndex = 0;
    private bool questionActive = true;

    void Start()
    {
        if (questionText == null || answerBlocks == null ||
            answerBlocks.Length != 2)
        {
            Debug.LogError("Assign Question Text and exactly 2 Answer Blocks!");
            return;
        }

        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            questionText.text = "ALL QUESTIONS COMPLETED!";

            questionActive = false;

            foreach (Clock_Anserw block in answerBlocks)
                block.gameObject.SetActive(false);

            return;
        }

        string correctTime = questions[currentQuestionIndex];

        questionText.text = "Which clock shows " + correctTime + "?";

        // Find the correct clock image
        ClockAnswer correctClock = clockAnswers.Find(
            x => x.time == correctTime
        );

        if (correctClock == null || correctClock.clockImage == null)
        {
            Debug.LogError("Missing clock image for " + correctTime);
            return;
        }

        // Choose a random incorrect clock
        List<ClockAnswer> wrongClocks = clockAnswers.FindAll(
            x => x.time != correctTime &&
                 x.clockImage != null &&
                 x.clockImage != correctClock.clockImage
        );

        if (wrongClocks.Count == 0)
        {
            Debug.LogError("You need at least one different clock image!");
            return;
        }

        ClockAnswer wrongClock = wrongClocks[
            Random.Range(0, wrongClocks.Count)
        ];

        // Randomly choose which block is correct
        int correctBox = Random.Range(0, 2);

        answerBlocks[correctBox].SetAnswer(
            correctClock.clockImage, true
        );

        answerBlocks[1 - correctBox].SetAnswer(
            wrongClock.clockImage, false
        );

        answerBlocks[0].gameObject.SetActive(true);
        answerBlocks[1].gameObject.SetActive(true);

        questionActive = true;
    }

    public void CheckAnswer(bool isCorrect)
    {
        if (!questionActive)
            return;

        if (isCorrect)
        {
            questionActive = false;

            currentQuestionIndex++;

            GenerateQuestion();
        }
        else
        {
            Debug.Log("Wrong answer! Try again.");
        }
    }
}
