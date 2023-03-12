using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{ 
    public Text Timer;
    public Text PowerStatus;
    public static int score = 0;
    public static int Power = 4;

    private Text ScoreDisplay;
    private float time;
    private void Awake()
    {
        ScoreDisplay = GetComponent<Text>();
    }
    private void Update()
    {
        
        Timer.text = "  Time: " + timer();
        ScoreDisplay.text = " Score : " + score;
        PowerInfo(Power);
    }
    private float timer()
    {
        time += Time.deltaTime;
        return Mathf.Round(time);
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
}
