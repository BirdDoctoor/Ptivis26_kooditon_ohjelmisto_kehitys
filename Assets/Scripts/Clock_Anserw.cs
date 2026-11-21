
using UnityEngine;

public class Clock_Anserw : MonoBehaviour
{
    public SpriteRenderer answerRenderer;
    public Clock_manager manager;

    [HideInInspector]
    public bool isCorrect;

    private bool alreadyHit = false;

    public void SetAnswer(Sprite newImage, bool correct)
    {
        answerRenderer.sprite = newImage;
        isCorrect = correct;
        alreadyHit = false;
    }

    public void HitBlock()
    {
        if (alreadyHit)
            return;

        alreadyHit = true;

        if (manager != null)
        {
            manager.CheckAnswer(isCorrect);
        }
        else
        {
            Debug.LogError("Clock manager is not assigned to " + gameObject.name);
        }
    }
}
