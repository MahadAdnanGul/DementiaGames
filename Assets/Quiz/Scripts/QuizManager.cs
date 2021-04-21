using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private QuizDataScriptable quizData;
    private List<Question> questions;
    private Question selectedQuestion;

    void Start()
    {
        questions = quizData.questions;
        selectQuestion();
    }

    void selectQuestion()
    {
        int val = Random.Range(0, questions.Count);
        selectedQuestion = questions[val];
        quizUI.setQuestion(selectedQuestion);
    }

    public bool Answer(string answered)
    {
        bool correctAns = false;
        if (answered == selectedQuestion.correctAns)
        {
            correctAns = true;
        }
        
        Invoke("selectQuestion", 0.4f);

        return correctAns;
    }
}


[System.Serializable]
public class Question
{
    public QuestionType questionType;
    public Sprite questionImg;
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