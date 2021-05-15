using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuizManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private Animator scoreAnim;
    [SerializeField] public Animator timeAnim;
    public Sprite[] themeImages;
    private SoundManager sm;
    [SerializeField] private QuizDataScriptable[] quizData;
    public int dataIndex;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI highScoreText;
    [SerializeField] public TextMeshProUGUI ReactionText;
    [SerializeField] private GameObject lives;
    [SerializeField] private GameObject timeObject;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject levelComplete;
    [SerializeField] private GameObject gameScreen;
    public int baseScoreBonus = 10;
    public int scoreBonus;
    public int score;
    public int levelScore;
    private List<Question> questions;
    private Question selectedQuestion;

    public bool selectThemeMode = false;
    public bool transitioning = false;

    void Start()
    {
        float temp = PlayerPrefs.GetFloat("Reaction", 999);
        temp = (float)System.Math.Round(temp, 2);
        ReactionText.text = "Reaction: " + (temp).ToString() + "ms";
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        scoreBonus = baseScoreBonus;
        sm = GetComponent<SoundManager>();
        //questions = quizData[dataIndex].questions;
        //tempQuestions = questions;
        UpdateScore();
        //selectQuestion();
    }
    
    public void NewGame(int ind)
    {
        selectThemeMode = true;
        levelScore = 0;
        dataIndex = ind;
        questions = new List<Question>(quizData[dataIndex].questions);
        selectQuestion();
        timeObject.GetComponent<Timer>().ResetTime();
        lives.GetComponent<Lives>().ResetLives();
    }

    public void NewGame()
    {
        selectThemeMode = false;
        levelScore = 0;
        dataIndex = 0;
        questions = new List<Question>(quizData[dataIndex].questions);
        selectQuestion();
        timeObject.GetComponent<Timer>().ResetTime();
        lives.GetComponent<Lives>().ResetLives();
    }
    public void NextGame()
    {
        levelScore = 0;
        questions = new List<Question>(quizData[dataIndex].questions);
        selectQuestion();
        timeObject.GetComponent<Timer>().ResetTime();
        lives.GetComponent<Lives>().ResetLives();
    }

    public void SetIndex(int ind)
    {
        dataIndex = ind;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        gameScreen.SetActive(false);
    }
    void selectQuestion()
    {
        transitioning = false;
        if(questions.Count<=5)
        {
            LevelComplete();
        }
        else
        {
            int val = Random.Range(0, questions.Count);
            selectedQuestion = questions[val];
            questions.RemoveAt(val);
            timeObject.GetComponent<Timer>().ResetTime();
            quizUI.setQuestion(selectedQuestion);
        }
    }
    void UpdateScore()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = "Score: " + levelScore;
        scoreAnim.SetTrigger("ScoreIncrease");
        
    }

    void CorrectAnswerBonus()
    {
        sm.PlayClip(0);
        timeObject.GetComponent<Timer>().correctAnswer = true;
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score", 0) + scoreBonus);
        levelScore += scoreBonus;
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
            transitioning = true;
            CorrectAnswerBonus();
            Invoke("selectQuestion", 0.8f);

        }
        else
        {
            timeObject.GetComponent<Timer>().correctAnswer = false;
            transitioning = true;
            sm.PlayClip(1);
            lives.GetComponent<Lives>().ReduceLife();
            if(lives.GetComponent<Lives>().index>=0)
            {
                Invoke("selectQuestion", 0.8f);
            }
            
        }
        
    }
    public void clearprefs()
    {
        PlayerPrefs.DeleteAll();
        float temp = PlayerPrefs.GetFloat("Reaction", 999);
        temp = (float)System.Math.Round(temp, 2);
        ReactionText.text = "Reaction: " + (temp).ToString() + "ms";
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    
    private void LevelComplete()
    {
        levelComplete.SetActive(true);
        gameScreen.SetActive(false);
    }

    public void SetReaction(double temp)
    {
        if (timeObject.GetComponent<Timer>().reactionTime / timeObject.GetComponent<Timer>().n < temp)
        {
            PlayerPrefs.SetFloat("Reaction", (float)temp);
            temp = PlayerPrefs.GetFloat("Reaction", 999);
            double temp2 = System.Math.Round(temp, 2);
            ReactionText.text = "Reaction: " + (temp2).ToString() + "ms";
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