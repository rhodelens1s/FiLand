using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public void charcterselect()
    {
        Player.instance.SavePlayer();
        SceneManager.LoadScene("Main Menu");
    }
}
