using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tryy : MonoBehaviour
{
    public void loadScenes()
    {
        string path = "C:\\Users\\john rey cabarle\\Documents\\Unity Project\\FiLand\\Assets\\Save Files" + "/player.txt";
        if (File.Exists(path))
        {
            SceneManager.LoadSceneAsync("Loading...");
        }
        else
        {
            SceneManager.LoadSceneAsync("EYN");
        }
    }
}

