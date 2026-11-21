
using UnityEngine;

public class PlayerColision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for AnswerBlock
        AnswerBlock answerBlock =
            collision.gameObject.GetComponent<AnswerBlock>();

        // Check for asd_anserw
        Clock_Anserw answerBlocks =
            collision.gameObject.GetComponent<Clock_Anserw>();

        // Check if player hit from underneath
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y < -0.5f)
            {
                if (answerBlock != null)
                {
                    answerBlock.HitBlock();
                }

                if (answerBlocks != null)
                {
                    answerBlocks.HitBlock();
                }

                break;
            }
        }
    }
}
