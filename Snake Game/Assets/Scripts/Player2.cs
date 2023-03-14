using UnityEngine;
public class Player2 : PlayerController
{
    public GameObject Snake2;
    public bool Is2Alive = true;
    private void Awake()
    {
        if (!MultiPlayer)
        {
            Snake2.SetActive(false);
            enabled = false;
        }
        transform.position = new Vector3(-15f, -7f, 0f);
    }
    public override void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("WASD Horizontal");
        vertical = Input.GetAxisRaw("WASD Vertical");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ShieldUp)
        {
            if (collision.CompareTag("Body2"))
            {
                Is2Alive = false;
                SoundManager.Instance.Play(SoundEvents.GameOver);
                enabled = false;
            }
            else if (collision.CompareTag("Body1"))
            {
                Is2Alive = true;
                Player1.win = false;
                SoundManager.Instance.Play(SoundEvents.GameOver);
                enabled = false;
            }
            else if (collision.CompareTag("Player1"))
            {
                if (GameUIManager.score2 > GameUIManager.score1)
                {
                    Is2Alive = true;
                    Player1.win = false;
                }
                else
                {
                    Is2Alive = false;
                    Player1.win = true;
                }
                SoundManager.Instance.Play(SoundEvents.GameOver);
                enabled = false;
            }
        }
        //--------Food
        if (collision.CompareTag("Apple"))
        {
            SoundManager.Instance.Play(SoundEvents.EatApple);
            GameUIManager.score2 += foodPoints;
            food.AppleEaten(segments.Count);
            Grow();
        }
        else if (collision.CompareTag("Skull"))
        {
            SoundManager.Instance.Play(SoundEvents.EatSkull);
            if (GameUIManager.score2 > 10)
            {
                GameUIManager.score2 -= foodPoints;
                food.SkullEaten();
            }
            else { food.SkullEaten(); }
            Destroy(segments[segments.Count - 1]);
            segments.RemoveAt(segments.Count - 1);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            SoundManager.Instance.Play(SoundEvents.GameOver);
            enabled = false;
            Is2Alive = false;
            Player1.win = true;
        }
    }
}
