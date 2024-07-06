using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LoadingScreen : MonoBehaviour
{
    public Slider slider;
    public Text progressText;

    public void Start()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path))
        {
            //SceneManager.LoadSceneAsync("MainMenu");
            LoadLevel("Main Menu");
        }
        else
        {
            LoadLevel("EYN");
        }
        }
    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }


}