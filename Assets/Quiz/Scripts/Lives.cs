using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    [SerializeField] private Image[] lives;
    [SerializeField] private Color normalCol;
    [SerializeField] private Color usedCol;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        index = lives.Length-1;
    }

    public void ReduceLife()
    {
        lives[index].color = usedCol;
        index--;
        CheckLives();
    }

    public void ResetLives()
    {
        Debug.Log("called");
        foreach(Image img in lives)
        {
            img.color = normalCol;
        }
        index = lives.Length - 1;
    }

    private void CheckLives()
    {
        if(index<0)
        {
            GameObject.FindObjectOfType<QuizManager>().GameOver();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
