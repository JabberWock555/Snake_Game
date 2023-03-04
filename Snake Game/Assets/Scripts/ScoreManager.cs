using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Text ScoreDisplay;
    public static int score = 0;

    private void Awake()
    {
        ScoreDisplay = GetComponent<Text>();
    }
    private void Update()
    {
        ScoreDisplay.text = " Score : " + score;
    }
}
