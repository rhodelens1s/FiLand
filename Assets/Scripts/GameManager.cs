using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Character[] characters;
    public Character currentCharacter;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path))
        {
            if (characters.Length > 0)
            {
                Player.instance.LoadPlayer();
                currentCharacter = characters[(int)Player.instance.currentChar];
            }
        }
        else
        {
            if (characters.Length > 0)
            {
                currentCharacter = characters[0];
            }
        }
    }
        

    public void SetCharacter(Character character)
    {
        currentCharacter = character;
        Player.instance.currentChar = Convert.ToInt32(System.Array.IndexOf(characters, character));
    }

}
