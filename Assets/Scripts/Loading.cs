using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Text textt;

    public void Awake()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        textt.text = data.playerName;
        
    }
}
