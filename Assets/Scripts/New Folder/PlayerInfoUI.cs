using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public static PlayerInfoUI playerinfoui;
    public GameObject infoPrefab;
    public GameObject ProfileUI;
    public Text playerName;
    public Text playerLevel;
    public Image CharacterIcon;

    void Awake()
    {
        Player.instance.LoadPlayer();

    }

    public void displayCharacterIcon()
    {
        GameObject info = Instantiate(infoPrefab, transform);

        Image image = info.GetComponentInChildren<Image>();
        image.sprite = GameManager.instance.currentCharacter.icon;

        Button button1 = info.GetComponent<Button>();

        button1.onClick.AddListener(() => {

            AudioManagerScript.instance.ClickButtons();
            Player.instance.LoadPlayer();
            if (!ProfileUI.activeInHierarchy)
            {
                ProfileUI.SetActive(true);
            }

            ButtonSound();
        });
    }

    public void testLang()
    {
        CharacterIcon.sprite = GameManager.instance.currentCharacter.icon;

        Button button1 = CharacterIcon.GetComponent<Button>();

        button1.onClick.AddListener(() => {

            AudioManagerScript.instance.ClickButtons();

            Player.instance.LoadPlayer();

            if (!ProfileUI.activeInHierarchy)
            {
                ProfileUI.SetActive(true);
            }

        });
    }


    void displayPlayerName()
    {
        try
        {
            playerName.text = Player.instance.playerName;
        }
        catch (Exception)
        {

        }
    }

    private void Start()
    {
       
    }

    private void Update()
    {
        displayPlayerName();

        testLang();

    }

    public void ButtonSound()
    {
        AudioManagerScript.instance.ClickButtons();
    }
}
