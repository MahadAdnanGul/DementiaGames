using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeRemaining;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private float defaultTime;
    [SerializeField] private QuizManager qm;
    [SerializeField] private SoundManager sm;
    private bool isCallable;

    public float reactionTime = 0;
    public int n = 0;
    private bool start = false;
    public bool correctAnswer;


    // Start is called before the first frame update
    void Start()
    {
        isCallable = true;
        ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining<3&&isCallable)
        {
            qm.timeAnim.SetBool("TimeOut", true);
            isCallable = false;
            sm.PlayClip(2);
        }
        timeRemaining = timeRemaining - Time.deltaTime;
        timeText.text = "Time: " + ((int)timeRemaining).ToString();
        qm.scoreBonus = qm.baseScoreBonus + (int)(timeRemaining / 4);
        CheckTime();
    }

    public void ResetTime()
    {
        if(!start)
        {
            start = true;
        }
        else
        {
            if(correctAnswer)
            {
                reactionTime += defaultTime - timeRemaining;
                n++;
            }
            else
            {
                reactionTime += defaultTime - timeRemaining;
            }
            
        }
        qm.timeAnim.SetBool("TimeOut", false);
        timeRemaining = defaultTime;
        isCallable = true;
    }
    void CheckTime()
    {
        if(timeRemaining<0&&!qm.transitioning)
        {
            ResetTime();
            GameObject.FindObjectOfType<QuizManager>().AnswerCheck(false);
        }
    }
}
