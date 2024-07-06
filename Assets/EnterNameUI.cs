using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterNameUI : MonoBehaviour
{
    public InputField inputField;
    public GameObject EnterNamePanel, CharacterSelectionPanel;
    public Button NextButton;
    public List<Sprite> BG;
 
    private void Update()
    {
        if (Player.instance.playerName == inputField.GetComponent<InputField>().text || inputField.GetComponent<InputField>().text.Length > 12)
        {
            NextButton.interactable = false;
        }
        else
        {
            NextButton.interactable=true;
        }
    }
    public void setPlayerName(string PlayerName)
    {
        Player.instance.playerName = PlayerName;

    }

    public void OnCLickNextButton()
    {
        string name = inputField.GetComponent<InputField>().text;

        setPlayerName(name);
        EnterNamePanel.SetActive(false);
        CharacterSelectionPanel.SetActive(true);
        var currentBG = GameObject.Find("BG").GetComponentInChildren<Image>();
        currentBG.sprite = BG[1];

        ButtonSound();
    }

    public void ButtonSound()
    {
        AudioManagerScript.instance.ClickButtons();
    }
}
