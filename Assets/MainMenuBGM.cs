using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBGM : MonoBehaviour
{
    
    void Start()
    {
        AudioManagerScript.instance.MainMenuMusic();
    }
}
