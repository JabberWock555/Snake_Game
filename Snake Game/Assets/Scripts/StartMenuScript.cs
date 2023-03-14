using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button SinglePlayerButton;
    [SerializeField] private Button MultiPlayerButton;

    private void Awake()
    {
        PlayButton.onClick.AddListener(PlayGame);
        SinglePlayerButton.onClick.AddListener(singlePlayer);
        MultiPlayerButton.onClick.AddListener(multiPlayer);
    }
    private void PlayGame()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        PlayButton.gameObject.SetActive(false);
        SinglePlayerButton.gameObject.SetActive(true);
        MultiPlayerButton.gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        Application.Quit();
    }

    private void singlePlayer()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        SceneManager.LoadScene(1);
        PlayerController.MultiPlayer = false;
    }
    private void multiPlayer()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        SceneManager.LoadScene(1);
        PlayerController.MultiPlayer = true;
    }
}

