using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public List<Button> levels;
    public GameObject map;
    public List<GameObject> Place;
    public List<Button> Place_;
    public List<GameObject> levelPanel;
    public List<GameObject> Boat;
    public List<GameObject> LevelLock;
    public List<GameObject> PlaceLock;
    public static LevelSelection instance;


    private void Awake()
    {
        Player.instance.LoadPlayer();
        int unlockedLevel = Player.instance.lvlUnlocked;
        int unlockedPlace = Player.instance.placeUnlocked;
        try
        {
            for (int i = 0; i < levels.Count; i++)
            {
                levels[i].interactable = false;
            }

            for (int i = 0; i < unlockedLevel; i++)
            {
                levels[i].interactable = true;
            }

            for (int i = 0; i < LevelLock.Count; i++)
            {
                LevelLock[i].SetActive(true);
            }

            for (int i = 0; i < unlockedLevel; i++)
            {
                LevelLock[i].SetActive(false);
            }

            for (int i = 0; i < Place_.Count; i++)
            {
                Place_[i].interactable = false;
            }
            for (int i = 0; i < unlockedPlace; i++)
            {
                Place_[i].interactable = true;
            }

            for (int i = 0; i < PlaceLock.Count; i++)
            {
                PlaceLock[i].SetActive(true);
            }

            for (int i = 0; i < unlockedPlace; i++)
            {
                PlaceLock[i].SetActive(false);
            }
        }
        catch (Exception)
        { 

        }
        
    }

    private void Update()
    {
        if (Player.instance.CurrentPlace == 0)
        {
            NounValleyBoat();
        }
        else if(Player.instance.CurrentPlace == 1)
        {

            PronounIslandBoat();
        }
        else if (Player.instance.CurrentPlace == 2)
        {
            VerbTownBoat();
        }
        else if (Player.instance.CurrentPlace == 3)
        {
            AdjectivePalaceBoat();
        }
        else 
        {
            AdverbParkBoat();
        }
    }
    public void OnLevelButtonClicked(int levelIndex)
    {
        Player.instance.currentLvl = levelIndex;
        Player.instance.SavePlayer();
        Debug.Log(levelIndex);

        SceneController.instance.Quiz();
      
    }


    public void OnBackButtonClicked()
    {
        try
        {
            for (var i = 0; i < levels.Count; i++)
            {
                levelPanel[i].SetActive(false);
                map.SetActive(true);
            }

        } catch (Exception) { }
    }

    public void OnMapButtonClicked(string place_)
    {
        AudioManagerScript.instance.ClickButtons();
        if (place_ == "Noun")
        {
            Player.instance.CurrentPlace = 0;
            Player.instance.SavePlayer();
            Place[0].SetActive(true);
            map.SetActive(false);

        }
        else if (place_ == "Pronoun")
        {
            Player.instance.CurrentPlace = 1;
            Player.instance.SavePlayer();
            Place[1].SetActive(true);
            map.SetActive(false);

        }
        else if (place_ == "Verb")
        {
            Player.instance.CurrentPlace = 2;
            Player.instance.SavePlayer();
            Place[2].SetActive(true);
            map.SetActive(false);

        }
        else if (place_ == "Adjective")
        {
            Player.instance.CurrentPlace = 3;
            Player.instance.SavePlayer();
            Place[3].SetActive(true);
            map.SetActive(false);

        }
        else
        {
            Player.instance.CurrentPlace = 4;
            Player.instance.SavePlayer();
            Place[4].SetActive(true);
            map.SetActive(false);

        }
        }


    public void NounValleyBoat()
    {
        Boat[0].SetActive(true);
        Boat[1].SetActive(false);
        Boat[2].SetActive(false);
        Boat[3].SetActive(false);
        Boat[4].SetActive(false);
    }

    public void PronounIslandBoat()
    {
        Boat[0].SetActive(false);
        Boat[1].SetActive(true);
        Boat[2].SetActive(false);
        Boat[3].SetActive(false);
        Boat[4].SetActive(false);
    }

    public void VerbTownBoat()
    {
        Boat[0].SetActive(false);
        Boat[1].SetActive(false);
        Boat[2].SetActive(true);
        Boat[3].SetActive(false);
        Boat[4].SetActive(false);
    }

    public void AdjectivePalaceBoat()
    {
        Boat[0].SetActive(false);
        Boat[1].SetActive(false);
        Boat[2].SetActive(false);
        Boat[3].SetActive(true);
        Boat[4].SetActive(false);
    }

    public void AdverbParkBoat()
    {
        Boat[0].SetActive(false);
        Boat[1].SetActive(false);
        Boat[2].SetActive(false);
        Boat[3].SetActive(false);
        Boat[4].SetActive(true);
    }
}

