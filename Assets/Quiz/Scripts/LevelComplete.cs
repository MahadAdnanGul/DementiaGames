using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private Image nextTheme;
    public QuizManager qm;
    [SerializeField] private TextMeshProUGUI themeText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI congrats;
    [SerializeField] private Button continueButton;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if(qm.selectThemeMode)
        {
            nextTheme.gameObject.SetActive(false);
            congrats.gameObject.SetActive(false);
            themeText.text = "";
            titleText.text = "LevelComplete";
            continueButton.gameObject.SetActive(false);
        }
        else if(qm.dataIndex+1==5)
        {
            nextTheme.gameObject.SetActive(false);
            themeText.text = "";
            titleText.text = "You Have Cleared All Levels!";
            congrats.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(false);
        }
        else
        {
            qm.SetIndex(qm.dataIndex + 1);
            congrats.gameObject.SetActive(false);
            nextTheme.gameObject.SetActive(true);
            nextTheme.sprite = qm.themeImages[qm.dataIndex];
            themeText.text = "Next Theme: " + qm.themeImages[qm.dataIndex].name;
            titleText.text = "Level Complete!";
            continueButton.gameObject.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
