using UnityEngine;
public class Player1 : PlayerController
{
    public bool Is1Alive = true;
    public  static bool win;
    private void Awake()
    {
        transform.position = new Vector3(15f, -7f, 0f);
    }
    public override void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("Arrow Horizontal");
        vertical = Input.GetAxisRaw("Arrow Vertical");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ShieldUp)
        {
            if (collision.CompareTag("Body1"))
            {
                SoundManager.Instance.Play(SoundEvents.GameOver);
                enabled = false;
                Is1Alive = false;
                win = false;
            }
            else if (collision.CompareTag("Body2"))
            {
                SoundManager.Instance.Play(SoundEvents.GameOver);
                enabled = false;
                Is1Alive = true;
                win = true;
            }
            else if (collision.CompareTag("Player1"))
            {
                if (GameUIManager.score1 > GameUIManager.score2)
                {
                    Is1Alive = true;
                    win = true;
                }
                else
                {
                    Is1Alive = false;
                    win = false;
                }
                SoundManager.Instance.Play(SoundEvents.GameOver);
                enabled = false;
            }
            
        }

        //--------Food
        if (collision.CompareTag("Apple"))
        {
            SoundManager.Instance.Play(SoundEvents.EatApple);
            GameUIManager.score1 += foodPoints;
            food.AppleEaten(segments.Count);
            Grow();
        }
        else if (collision.CompareTag("Skull"))
        {
            SoundManager.Instance.Play(SoundEvents.EatSkull);
            if (GameUIManager.score1 > 10)
            {
                GameUIManager.score1 -= foodPoints;
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
            Is1Alive = false;
            win = false;
        }
    }
}
