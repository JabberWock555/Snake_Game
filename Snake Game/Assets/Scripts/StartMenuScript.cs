using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        Application.Quit();
    }
}
