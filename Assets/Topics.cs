using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Topics : MonoBehaviour
{
    public void BackBtn()
    {
        SceneController.instance.mainMenu();
    }

    public void ButtonSound()
    {
        AudioManagerScript.instance.ClickButtons();
    }
}
