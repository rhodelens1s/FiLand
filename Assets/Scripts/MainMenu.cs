using UnityEngine;


public class MainMenu : MonoBehaviour
{
  
    public void PlayGame()
    {
        SceneController.instance.playGame();
    }

    public void Home()
    {
        SceneController.instance.mainMenu();
    }

    public void Topic()
    {
        SceneController.instance.Topics();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Noun()
    {
        SceneController.instance.Quiz();
    }

    public void Gallery()
    {
        SceneController.instance.Gallery();
    }

    public void About()
    {
        SceneController.instance.About();
    }
}
