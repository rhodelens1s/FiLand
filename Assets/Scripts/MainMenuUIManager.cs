using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public int currentMusic;
    public int currentSound;
    public Text playerName1, playerName2;
    public Text playerScore1, playerScore2, HighestLvl;
    public InputField playerNameInput;
    public Button saveButton, renameXBtn;
    public Image CharacterIcon1, CharacterIcon2;
    public GameObject ProfileUI;

    void Update()
    {
        if (Player.instance.playerName == playerNameInput.GetComponent<InputField>().text  || playerNameInput.text == "" || playerNameInput.text.Length > 12)
        {
            saveButton.interactable = false;
            try
            {
                displayName();
                displayScore();
                displayLevel();

            }
            catch (Exception)
            {
            }
        }
        else 
        { 
            saveButton.interactable= true;
        }

        
    }

    private void Start()
    {
        displayNameRename();
        DisplayCharacterIcon1();
        DisplayCharacterIcon2();

        Player.instance.IntroUnlock = true;
        Player.instance.SavePlayer();
    }
    public void displayName()
    {

       playerName1.text = Player.instance.playerName;
       playerName2.text = Player.instance.playerName;
    }

    public void displayScore()
    {
        playerScore1.text = Player.instance.playerTotalScore.ToString();
        playerScore2.text = Player.instance.playerTotalScore.ToString();
    }

    public void displayLevel()
    {
        HighestLvl.text =  Player.instance.lvlUnlocked.ToString();
    }
    public void displayNameRename()
    {
        try
        {
            playerNameInput.text = Player.instance.playerName;
        }
        catch (Exception)
        {

        }

    }

    public void OnClickXButtonRename()
    {
        playerNameInput.text = Player.instance.playerName;
    }

    public void DisplayCharacterIcon1()
    {
        CharacterIcon1.sprite = GameManager.instance.currentCharacter.icon;

        Button button1 = CharacterIcon1.GetComponent<Button>();

        button1.onClick.AddListener(() => {

            Player.instance.LoadPlayer();

            if (!ProfileUI.activeInHierarchy)
            {
                ProfileUI.SetActive(true);
                ButtonSound();
            }

            
        });
    }

    public void DisplayCharacterIcon2()
    {
        CharacterIcon2.sprite = GameManager.instance.currentCharacter.icon;
    }

    public void ButtonSound()
    {
        AudioManagerScript.instance.ClickButtons();
    }
}
