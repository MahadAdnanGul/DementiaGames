using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] private QuizManager qm;
    [SerializeField] private Timer timings;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI reactionTime;
    private int highS;
    private bool highYes = false;
    public double temp2;

    // Start is called before the first frame update
    private void OnEnable()
    {
        highS = PlayerPrefs.GetInt("HighScore", 0);
        if(qm.levelScore>highS)
        {
            PlayerPrefs.SetInt("HighScore", qm.levelScore);
            qm.highScoreText.text = "Total Score: " + (PlayerPrefs.GetInt("HighScore",0)).ToString();
            highYes = true;
        }
        if (highYes)
        {
            highScore.gameObject.SetActive(true);
        }
        else
        {
            highScore.gameObject.SetActive(false);
        }
        score.text = "Score: "+qm.levelScore.ToString();
        double temp = (timings.reactionTime / timings.n);
        temp2 = System.Math.Round(temp, 2);
        reactionTime.text = "Reaction Time: "+(temp2).ToString()+"ms";
        qm.SetReaction(temp2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
