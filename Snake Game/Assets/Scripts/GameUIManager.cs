using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static int score1 = 0;
    public static int score2 = 0;
    public static int Power = 4;

    [SerializeField]private Player1 Player1;
    [SerializeField]private Player2 Player2;
    [SerializeField]private Text ScoreDisplay;
    [SerializeField]private Text TimeRecord;
    [SerializeField]private Text PowerStatus;
    [SerializeField]private Text GameStartTime;
    [SerializeField]private Text PlayerWinStatus;
    [SerializeField]private GameObject PauseScreen;
    [SerializeField]private GameObject GameOverScreen;
    [SerializeField]private Slider VolumeSlider;
    [SerializeField]private Button Unmute;
    
    private float time;
    private bool paused = true;

    private void Awake()
    {
        Unmute.gameObject.SetActive(false);
        VolumeSlider.value = 1f;
        Player1.enabled = false;
        Player1.food.gameObject.SetActive(false);
        Player2.enabled = false;
        Player2.food.gameObject.SetActive(false);
        GameOverScreen.SetActive(false);
        PauseScreen.SetActive(false);
        Unmute.onClick.AddListener(UnMute);
    }

    private void Update()
    {
        if (!GameStartTime.IsActive())
        {
            TimeRecord.text = "  Time: " + Timer();
            SoundManager.Instance.Volume = VolumeSlider.value;
            PowerInfo(Power);
            if (!PlayerController.MultiPlayer)
            {
                ScoreDisplay.text = " Player 1 Score : " + score1;
                if (!Player1.Is1Alive)
                {
                    Debug.Log("Dead");
                    PlayerWinStatus.text = "Game Over";
                    GameOverScreen.SetActive(true);
                    paused = true;
                }
            }
            else {
                ScoreDisplay.text = " Player 1 Score : " + score1 + "\n Player 2 score : " + score2;
                if (!Player1.Is1Alive || !Player2.Is2Alive)
                {
                    if (Player1.win)
                    {
                        PlayerWinStatus.text = "Player 1 Win !\n" +
                                "Player 1 Score : " + score1 + "\n Player 2 score : " + score2;
                    }
                    else if (!Player1.win)
                    {
                        PlayerWinStatus.text = "Player 2 Win !\n" +
                               "Player 1 Score : " + score1 + "\n Player 2 score : " + score2;
                    }
                    Debug.Log("Dead");
                    GameOverScreen.SetActive(true);
                    paused = true;
                }
            }
            
        }
        else { GameStartTimer(); }
    }

    private float Timer()
    {
        if (!paused)
        {
            time += Time.deltaTime;
            return Mathf.Round(time);
        }
        else
        { return Mathf.Round(time); }
    }

    private void PowerInfo(int type)
    {
        switch (type)
        {
            case 0:
                PowerStatus.text = "Shield On!";
                break;
            case 1:
                PowerStatus.text = "Speed Boost!";
                break;
            case 2:
                PowerStatus.text = "Score 2X!";
                break;
            default:
                PowerStatus.text = " ";
                break;
        }
    }

    private void GameStartTimer()
    {
        paused = false;
        GameStartTime.text = "" + Timer();
        if (Timer() > 3)
        {
            Player1.enabled = true;
            Player1.food.gameObject.SetActive(true);
            Player2.enabled = true;
            Player2.food.gameObject.SetActive(true);
            GameStartTime.gameObject.SetActive(false);
            time = 0;
        }
    }


    public void PlayAgain()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        paused = true;
        PauseScreen.gameObject.SetActive(true);
        Player1.enabled = false;
        Player2.enabled = false;
    }

    public void Continue()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        paused = false;
        PauseScreen.gameObject.SetActive(false);
        Player1.enabled = true;
        Player2.enabled = true;
    }

    public void ExitGame()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        time = 0;
        SceneManager.LoadScene(0);
    }

    public void Mute()
    {
        SoundManager.Instance.Mute = true;
        Unmute.gameObject.SetActive(true);
    }
    private void UnMute()
    {
        SoundManager.Instance.Mute = false;
        Unmute.gameObject.SetActive(false);
    }
}
