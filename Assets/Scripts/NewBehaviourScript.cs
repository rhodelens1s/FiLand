using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public InputField inputField;
    public void ButtonFunction()
    {
        StartCoroutine(DelaySceneLoad());
    }

    IEnumerator DelaySceneLoad()
    {
        yield return new WaitForSeconds(1f); // Wait 1 seconds
        SceneManager.LoadScene("AfterMenu"); // Change to the ID or Name of the scene to load
    }

    public void renameBtn()
    {
        if(inputField.interactable == true) {
            inputField.interactable = false;
        }
        else
        {
            inputField.interactable = true;
        }
    }
}
