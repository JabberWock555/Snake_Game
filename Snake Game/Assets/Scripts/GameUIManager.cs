using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public PlayerController Player;
    public Text ScoreDisplay;
    public Text TimeRecord;
    public Text PowerStatus;
    public Text GameStartTime;
    public GameObject PauseScreen;
    public GameObject GameOverScreen;
    public static int score = 0;
    public static int Power = 4;
    public Slider VolumeSlider;

    private float time;
    private bool paused = true;
    private void Awake()
    {
        VolumeSlider.value = 1f;
        Player.enabled = false;
        Player.food.gameObject.SetActive(false);
        GameOverScreen.SetActive(false);
        PauseScreen.SetActive(false);
    }
    private void Update()
    {
        if (!GameStartTime.IsActive())
        {
            TimeRecord.text = "  Time: " + Timer();
            ScoreDisplay.text = " Score : " + score;
            PowerInfo(Power);
            SoundManager.Instance.Volume = VolumeSlider.value;

            if (!PlayerController.IsAlive)
            {
                GameOverScreen.SetActive(true);
                paused = true;
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
        Player.enabled = false;
    }
    public void Continue()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        paused = false;
        PauseScreen.gameObject.SetActive(false);
        Player.enabled = true;
    }
    public void ExitGame()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        time = 0;
        SceneManager.LoadScene(0);
    }

    private void GameStartTimer()
    {
        paused = false;
        GameStartTime.text = ""+ Timer();
        if (Timer() > 3)
        {
            Player.enabled = true;
            GameStartTime.gameObject.SetActive(false);
            Player.food.gameObject.SetActive(true);
            time = 0;
        }
    }
}
