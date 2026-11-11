
using UnityEngine;

public class PlayerColision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player hit an answer block
        AnswerBlock answerBlock =
            collision.gameObject.GetComponent<AnswerBlock>();

        if (answerBlock != null)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Check if player hit the block from underneath
                if (contact.normal.y < -0.5f)
                {
                    answerBlock.HitBlock();
                    break;
                }
            }
        }
    }
}
