using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ButtonsAlpha : MonoBehaviour
{
    public Image[] theButtons;
    public int numberOfButtons;
    void Start()
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            theButtons[i].alphaHitTestMinimumThreshold = 1f;
        }
    }
  
}
