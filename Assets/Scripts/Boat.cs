using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Player.instance.LoadPlayer();
    }
}
