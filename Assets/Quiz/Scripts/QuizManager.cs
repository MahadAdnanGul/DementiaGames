using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuizManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private QuizDataScriptable quizData;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject lives;
    [SerializeField] private GameObject timeObject;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject gameScreen;
    public int scoreBonus = 50;
    public int score;
    private List<Question> questions;
    private Question selectedQuestion;

    void Start()
    {
        questions = quizData.questions;
        UpdateScore();
        selectQuestion();
    }
    
    public void NewGame()
    {
        timeObject.GetComponent<Timer>().ResetTime();
        lives.GetComponent<Lives>().ResetLives();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        gameScreen.SetActive(false);
    }
    void selectQuestion()
    {
        int val = Random.Range(0, questions.Count);
        selectedQuestion = questions[val];
        timeObject.GetComponent<Timer>().ResetTime();
        quizUI.setQuestion(selectedQuestion);
    }
    void UpdateScore()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = "Score: " + score;
    }

    void CorrectAnswerBonus()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score", 0) + scoreBonus);
        UpdateScore();
    }

    public bool Answer(string answered)
    {
        bool correctAns = false;
        if (answered == selectedQuestion.correctAns)
        {
            correctAns = true;
            
        }
        AnswerCheck(correctAns);
        

        return correctAns;
    }

    public void AnswerCheck(bool answer)
    {
        if(answer)
        {
            CorrectAnswerBonus();
            Invoke("selectQuestion", 0.4f);

        }
        else
        {
            lives.GetComponent<Lives>().ReduceLife();
            Invoke("selectQuestion", 0.4f);
        }
        
    }    
}


[System.Serializable]
public class Question
{
    public QuestionType questionType;
    public Sprite questionImg;
    public Sprite questionImg2;
    public AudioClip questionClip;
    public UnityEngine.Video.VideoClip questionVideo;
    public string questionInfo;
    public List<string> options;
    public string correctAns;
}
[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    VIDEO,
    AUDIO
}