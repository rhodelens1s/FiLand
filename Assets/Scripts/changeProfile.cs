using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changeProfile : MonoBehaviour
{
    public InputField inputField;
    public GameObject rename;
    public GameObject profileedit;
    public void changeProfiles()
    {
        AudioManagerScript.instance.ClickButtons();
        Player.instance.playerName = inputField.GetComponent<InputField>().text;
        Player.instance.SavePlayer();
        rename.SetActive(false);
        
    }

}
