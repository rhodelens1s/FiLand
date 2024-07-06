using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test1 : MonoBehaviour
{
    public static test1 instance;
    public GameObject testPrefab;
    public void Starto()
    {
        Player.instance.LoadPlayer();

        GameObject ProfileInfo = Instantiate(testPrefab, transform);
    }
}
