using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuizUI : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Text scoreText, timerText;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private GameObject gameOverPanel, mainMenuPanel, gameMenuPanel;
    [SerializeField] private Image questionImage;
    [SerializeField] private Image questionImage2;
    [SerializeField] private UnityEngine.Video.VideoPlayer questionVideo;
    [SerializeField] private AudioSource questionAudio;
    [SerializeField] private List<Button> options;
    [SerializeField] private Button uiButton;
    [SerializeField] private Color correctCol, wrongCol, normalCol;
    private Question question;
    private bool answered;
    private float audioLength;

    public Text ScoreText { get { return scoreText;}}
    public Text TimerText { get { return timerText;}}

    // Start is called before the first frame update
    void Awake()
    {
        for (int i=0; i< options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => onClick(localBtn));
        }
        uiButton.onClick.AddListener(() => onClick(uiButton));

    }

    //private void OnEnable()
   // {
    //    quizManager.NewGame();
    //}

    public void setQuestion(Question question)
    {
        this.question = question;
        switch (question.questionType)
        {
            case QuestionType.TEXT:
            questionImage.transform.parent.gameObject.SetActive(false);
            break;
            case QuestionType.IMAGE:
            ImageHolder();
            questionImage.transform.gameObject.SetActive(true);
            questionImage.sprite = question.questionImg;
            questionImage2.transform.gameObject.SetActive(true);
            questionImage2.sprite = question.questionImg2;
            break;

            case QuestionType.VIDEO:
            ImageHolder();
            questionVideo.transform.gameObject.SetActive(true);
            questionVideo.clip = question.questionVideo;
            questionVideo.Play();
            break;

            case QuestionType.AUDIO:
            ImageHolder();
            questionAudio.transform.gameObject.SetActive(true);
            audioLength = question.questionClip.length;
            StartCoroutine(PlayAudio());
            break;
        }

        questionText.text = question.questionInfo;
        List<string> answerList = question.options;
        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = normalCol;
        }
        answered = false;
    }

    IEnumerator PlayAudio()
    {
        if (question.questionType == QuestionType.AUDIO)
        {
            questionAudio.PlayOneShot(question.questionClip);
            yield return new WaitForSeconds(audioLength + 0.5f);
            StartCoroutine(PlayAudio());
        }
        else
        {
            StopCoroutine(PlayAudio());
            yield return null;
        }
    }


    void ImageHolder()
    {
            questionImage.transform.parent.gameObject.SetActive(true);
            questionImage.transform.gameObject.SetActive(false);
            questionVideo.transform.gameObject.SetActive(false);
            questionAudio.transform.gameObject.SetActive(false);
    }

    private void onClick(Button btn)
    {
        if (!answered)
        {
            answered = true;
            if (quizManager.Answer(btn.name))
            {
                btn.image.color = correctCol;
            }
            else
            {
                btn.image.color = wrongCol;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
