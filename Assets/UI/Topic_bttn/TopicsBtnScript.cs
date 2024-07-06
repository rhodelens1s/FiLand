using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopicsBtnScript : MonoBehaviour
{
    public GameObject scrollbar;
    float distance = 0.25f;
    int topicbtn = 0;
    public GameObject[] NextBackButton;
    
   

    public void NextBtn()
    {
        scrollbar.GetComponent<Scrollbar>().value = scrollbar.GetComponent<Scrollbar>().value + distance;
        topicbtn++;
    }

    public void BackBtn()
    {
        scrollbar.GetComponent<Scrollbar>().value = scrollbar.GetComponent<Scrollbar>().value - distance;
        topicbtn--;
    }

 
    void Update()
    {

        if ( topicbtn == 0)
        {
            NextBackButton[1].SetActive(false);
            NextBackButton[0].SetActive(true);
        }
        else if ( topicbtn == 4 )
        {
            NextBackButton[0].SetActive(false);
            NextBackButton[1].SetActive(true);
        }
        else
        {
            NextBackButton[0].SetActive(true);
            NextBackButton[1].SetActive(true);
        }
    }
}
