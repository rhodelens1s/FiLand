using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashArtScript : MonoBehaviour
{

    public void OnSplashArtEnd()
    {
        SceneManager.LoadScene(1);
    }


}
