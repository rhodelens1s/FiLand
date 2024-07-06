using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    [Header("Player Profile")]
    public string playerName = string.Empty;
    public int currentChar = 0;
    [Header("Player Data")]
    public int playerScore = 0;
    public int playerCoin = 0;
    [Header("Game Data")]
    public int placeUnlocked = 1;
    public int lvlUnlocked = 1;

    public void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {
        Load();
    }
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "gamedata.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamedata.txt", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);

            playerName = data.playerName;
            currentChar = data.currentChar;
            playerScore = data.playerScore;
            playerCoin = data.playerCoin;
            placeUnlocked = data.placeUnlocked;
            lvlUnlocked = data.lvlUnlocked;

            file.Close();
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamedata.txt");
        PlayerData_Storage data = new PlayerData_Storage();

        data.playerName = playerName ;
        data.currentChar = currentChar;
        data.playerScore = playerScore;
        data.playerCoin = playerCoin;
        data.placeUnlocked = placeUnlocked;
        data.lvlUnlocked = lvlUnlocked;

        bf.Serialize(file, data);
        file.Close();

    }
}

[Serializable]
class PlayerData_Storage
{
    public string playerName;
    public int currentChar;
    public int playerScore;
    public int playerCoin;
    public int placeUnlocked;
    public int lvlUnlocked;
}
