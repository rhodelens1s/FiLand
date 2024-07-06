using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playGame()
    {
        AudioManagerScript.instance.Clouds();
        StartCoroutine(loadPlay());
    }

    IEnumerator loadPlay()
    {
        AudioManagerScript.instance.Clouds();
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Map");
        animator.SetTrigger("Start");
    }

    public void mainMenu()
    {
        StartCoroutine(loadMainMenu());
    }

    IEnumerator loadMainMenu()
    {
        AudioManagerScript.instance.Clouds();
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main Menu");
        animator.SetTrigger("Start");
    }

    public void Topics()
    {
        StartCoroutine(loadTopics());
    }

    IEnumerator loadTopics()
    {
        AudioManagerScript.instance.Clouds();
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Topics");
        animator.SetTrigger("Start");
    }

    public void Quiz()
    {
        StartCoroutine(loadQuiz());
    }
    IEnumerator loadQuiz()
    {
        AudioManagerScript.instance.Clouds();
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Quiz");
        animator.SetTrigger("Start");

    }
    public void PronounIsland()
    {
        StartCoroutine(loadPronounIsland());
    }
    IEnumerator loadPronounIsland()
    {
        AudioManagerScript.instance.Clouds();
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        animator.SetTrigger("Start");
    }

    public void Gallery()
    {
        StartCoroutine(LoadGallery());
    }

    IEnumerator LoadGallery()
    {
        AudioManagerScript.instance.Clouds();
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Gallery");
        animator.SetTrigger("Start");
    }

    public void About()
    {
        StartCoroutine(LoadAbout());
    }
    IEnumerator LoadAbout()
    {
        AudioManagerScript.instance.Clouds();
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("About");
        animator.SetTrigger("Start");
    }
}
