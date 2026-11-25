
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Clock_manager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI questionText;
    public Clock_Anserw[] answerBlocks;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip clip1;
    public AudioClip clip2;

    // Correct-answer sounds (Clip 3, 4, 5, etc.)
    public AudioClip[] correctSounds;

    // Four random wrong-answer sounds
    public AudioClip[] wrongSounds;

    [System.Serializable]
    public class ClockAnswer
    {
        public string time;
        public Sprite clockImage;
    }

    public List<ClockAnswer> clockAnswers =
        new List<ClockAnswer>();

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
    private bool questionActive = false;

    void Start()
    {
        if (questionText == null || answerBlocks == null ||
            answerBlocks.Length != 2)
        {
            Debug.LogError("Assign Question Text and exactly 2 Answer Blocks!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("Assign an AudioSource!");
            return;
        }

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        // Play startup clip 1
        if (clip1 != null)
        {
            audioSource.PlayOneShot(clip1);
            yield return new WaitForSeconds(clip1.length);
        }

        // Play startup clip 2
        if (clip2 != null)
        {
            audioSource.PlayOneShot(clip2);
            yield return new WaitForSeconds(clip2.length);
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

        questionText.text = "Kello on " + correctTime ;

        ClockAnswer correctClock = clockAnswers.Find(
            x => x.time == correctTime
        );

        if (correctClock == null || correctClock.clockImage == null)
        {
            Debug.LogError("Missing clock image for " + correctTime);
            return;
        }

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

            StartCoroutine(CorrectAnswerSequence());
        }
        else
        {
            PlayWrongSound();
            Debug.Log("Wrong answer! Try again.");
        }
    }

    void PlayWrongSound()
    {
        if (wrongSounds == null || wrongSounds.Length == 0)
            return;

        AudioClip randomWrongSound = wrongSounds[
            Random.Range(0, wrongSounds.Length)
        ];

        if (randomWrongSound != null)
            audioSource.PlayOneShot(randomWrongSound);
    }

    IEnumerator CorrectAnswerSequence()
    {
        // Play the correct-answer sound for this question
        if (currentQuestionIndex < correctSounds.Length)
        {
            AudioClip correctSound = correctSounds[currentQuestionIndex];

            if (correctSound != null)
            {
                audioSource.PlayOneShot(correctSound);
                yield return new WaitForSeconds(correctSound.length);
            }
        }

        currentQuestionIndex++;

        GenerateQuestion();
    }
}
