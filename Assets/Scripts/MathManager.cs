
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MathManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public AnswerBlock[] answerBlocks;
    public GameObject barrier;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip introductionSound;

    public AudioClip[] calculationSounds;
    public AudioClip[] correctSounds;
    public AudioClip[] wrongSounds;

    private int correctAnswer;
    private bool questionActive = true;
    private bool questionCompleted = false;

    private AudioClip currentCalculationSound;

    // Addition and subtraction questions
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
        // Find Barrier inside this MathManager
        foreach (Transform child in GetComponentsInChildren<Transform>(true))
        {
            if (child.name == "Barrier")
            {
                barrier = child.gameObject;
                break;
            }
        }

        if (barrier != null)
        {
            Debug.Log(gameObject.name + " controls " + barrier.name);
        }
        else
        {
            Debug.LogError("No Barrier found for " + gameObject.name);
        }
    }

    IEnumerator Start()
    {
        if (questionText == null)
        {
            Debug.LogError("Question Text is not assigned!");
            yield break;
        }

        if (answerBlocks == null || answerBlocks.Length < 3)
        {
            Debug.LogError("You need to assign 3 Answer Blocks!");
            yield break;
        }

        if (barrier == null)
        {
            Debug.LogError("Barrier is missing!");
            yield break;
        }

        if (audioSource == null)
        {
            Debug.LogError("Audio Source is not assigned!");
            yield break;
        }

        if (calculationSounds == null ||
            calculationSounds.Length != calculations.Count)
        {
            Debug.LogError(
                "You need exactly one calculation sound for each calculation!"
            );
            yield break;
        }

        // Make sure barrier starts enabled
        barrier.SetActive(true);

        // Play introduction
        questionText.text = "";

        if (introductionSound != null)
        {
            audioSource.PlayOneShot(introductionSound);
            yield return new WaitForSeconds(introductionSound.length);
        }

        GenerateQuestion();
    }

    // Generate a random question and three answers
    void GenerateQuestion()
    {
        questionActive = true;
        questionCompleted = false;

        // Make sure barrier is enabled for the new question
        if (barrier != null)
        {
            barrier.SetActive(true);
        }

        int randomIndex = Random.Range(0, calculations.Count);

        string calculation = calculations[randomIndex];

        currentCalculationSound = calculationSounds[randomIndex];

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

        // Choose correct answer position
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
                do
                {
                    answer = Random.Range(0, 20);
                }
                while (usedAnswers.Contains(answer));
            }

            usedAnswers.Add(answer);

            answerBlocks[i].SetAnswer(
                answer,
                answer == correctAnswer
            );

            answerBlocks[i].gameObject.SetActive(true);
        }

        PlayCalculationSound();
    }

    // Repeat current calculation
    public void RepeatCalculation()
    {
        if (!questionActive)
            return;

        PlayCalculationSound();
    }

    void PlayCalculationSound()
    {
        if (currentCalculationSound == null)
            return;

        audioSource.Stop();
        audioSource.PlayOneShot(currentCalculationSound);
    }

    // Check player's answer
    public void CheckAnswer(bool isCorrect)
    {
        if (!questionActive)
            return;

        if (isCorrect)
        {
            questionActive = false;
            questionCompleted = true;

            questionText.text = "CORRECT!";

            // Play correct sound
            PlayRandomSound(correctSounds);


            // DISABLE BARRIER
            if (barrier != null)
            {
                barrier.SetActive(false);
                Debug.Log("Barrier disabled!");
            }

            Debug.Log("Correct answer! Walk through the barrier.");
        }
        else
        {
            // Play wrong sound
            PlayRandomSound(wrongSounds);

            Debug.Log("Wrong answer! Try again.");
        }
    }

    // Play random sound
    void PlayRandomSound(AudioClip[] sounds)
    {
        if (sounds == null || sounds.Length == 0)
            return;

        AudioClip clip = sounds[
            Random.Range(0, sounds.Length)
        ];

        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Called when player walks through barrier
    public void NextQuestion()
    {
        if (!questionCompleted)
            return;

        // GenerateQuestion will enable the barrier again
        GenerateQuestion();
    }
}
