
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LoadingS : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public static LoadingS instance;
    float time, second;
    //public Text progressText;

    void Start()
    {
        second = 5;

        Invoke("CharacterSelection", second);    

    }

    void Update()
    {
        if (time < 5)
        {
            time += Time.deltaTime;
            slider.value = time / second;
            //percent = (time / second) * 100f;
            //progressText.text = (time/second) * 100 + "%";
        }
    }

    void CharacterSelection()
    {
        SceneManager.LoadScene(2);
    }

}