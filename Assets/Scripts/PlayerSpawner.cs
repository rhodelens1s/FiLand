using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject BoyCharacter;
    [SerializeField] private GameObject GirlCharacter;
    
    private void Start()
    {
        switch (Player.instance.currentChar)
        {
            case 0:
                BoyCharacter.SetActive(true);
                GirlCharacter.SetActive(false);
                break;
            case 1:
                BoyCharacter.SetActive(false);
                GirlCharacter.SetActive(true);
                break;
        }
    }
}
