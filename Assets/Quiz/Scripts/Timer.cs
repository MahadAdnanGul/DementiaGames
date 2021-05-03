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


    // Start is called before the first frame update
    void Start()
    {
        ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining = timeRemaining - Time.deltaTime;
        timeText.text = "Time: " + ((int)timeRemaining).ToString();
        CheckTime();
    }

    public void ResetTime()
    {
        timeRemaining = defaultTime;
    }
    void CheckTime()
    {
        if(timeRemaining<0)
        {
            ResetTime();
            GameObject.FindObjectOfType<QuizManager>().AnswerCheck(false);
        }
    }
}
