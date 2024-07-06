
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroAnimation : MonoBehaviour
{
    public GameObject Animated, SkipBtn;

    public Slider slider;

    bool on_drag = false;

    void Start()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path) && Player.instance.IntroUnlock == true)
        {
            EndIntro();
        }
        else
        {
            IntroAnim();
        }
        
    }

    void Update()
    {
        if (AnimatedScript.instance.animated.isPlaying && !on_drag)
        {
            slider.value = (float)AnimatedScript.instance.animated.frame / (float)AnimatedScript.instance.animated.frameCount;
        }
    }
    public void IntroAnim()
    {
        Debug.Log("start");
        Animated.SetActive(true);

        AnimatedScript.instance.intro();

        float videoTime = (float)AnimatedScript.instance.animated.length;

        AudioManagerScript.instance.MusicSFX.Pause();
        AudioManagerScript.instance.SoundSFX.Pause();

        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path))
        {
            SkipIntro();
        }

    }
    public void EndIntro()
    {
        AudioManagerScript.instance.MusicSFX.Play();
        AudioManagerScript.instance.SoundSFX.Play();

        string path = Application.persistentDataPath + "/player.txt";
        if (!File.Exists(path))
        {
            Animated.SetActive(false);

            AnimatedScript.instance.animated.Stop();
        }
        else
        {
            SceneManager.LoadScene(3);
            AnimatedScript.instance.animated.Stop();
        }
        
    }
    void SkipIntro()
    {
        SkipBtn.SetActive(true);
    }

    public void OnDrag()
    {
        on_drag = true;

    }

    public void OnUp()
    {
        on_drag = false;
        float frame = (float)slider.value * (float)AnimatedScript.instance.animated.frameCount;
        AnimatedScript.instance.animated.frame = (long)frame;
    }

    public void PlayPause()
    {
        if (AnimatedScript.instance.animated.isPlaying)
        {
            AnimatedScript.instance.animated.Pause();
        }
        else
        {
            AnimatedScript.instance.animated.Play();
        }
    }
}
