using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatCamera : MonoBehaviour
{
    public Camera cam;
    public GameObject boat;

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = boat.transform.position;
    }
}