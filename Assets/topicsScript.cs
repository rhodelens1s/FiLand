using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class topicsScript : MonoBehaviour
{
    public Image CurrentPage;
    public int CurrentPageIndex;
    public List<Sprite> Topics;
    public GameObject NextBtn, PreviousBtn;
    void Start()
    {
        CurrentPageIndex = 0;
        
    }

    void Update()
    {
        CurrentPage.GetComponent<Image>().sprite = Topics[CurrentPageIndex];
        
        if (CurrentPageIndex == 0)
        {
            NextBtn.SetActive(true);
            PreviousBtn.SetActive(false);
        }
        else if (CurrentPageIndex == (Topics.Count - 1)) 
        {
            PreviousBtn.SetActive(true);
            NextBtn.SetActive(false);
        }
        else
        {
            PreviousBtn.SetActive(true);
            NextBtn.SetActive(true);
        }
    }

    public void NextTopic()
    {
        CurrentPageIndex = CurrentPageIndex +1;
        ButtonSound();
    }

    public void PreviousTopic()
    {
        CurrentPageIndex = CurrentPageIndex -1;
        ButtonSound();
    }

    public void ButtonSound()
    {
        AudioManagerScript.instance.ClickButtons();
    }
}
