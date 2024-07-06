using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MapUIManager : MonoBehaviour
{

    public int currentMusic;
    public int currentSound;
    public Text playerScore;
    public GameObject Animated, SkipBtn;

    public Image Handle;
    public List<Sprite> Handles;

    public Slider slider;

    bool on_drag = false;
    void Start()
    {
        
    }

    void Update()
    {
        Player.instance.LoadPlayer();
        DisplayScore();

        if (AnimatedScript.instance.animated.isPlaying && !on_drag)
        {
            slider.value = (float)AnimatedScript.instance.animated.frame / (float)AnimatedScript.instance.animated.frameCount;
        }

    }

    public void DisplayScore()
    {
        playerScore.text = Player.instance.playerTotalScore.ToString();
    }

    public void OnClickHomeButton()
    {
        SceneController.instance.mainMenu();
    }

    public void NounAnim()
    {
        

        if (Player.instance.NounUnlock == false)
        {
            Animated.SetActive(true);

            AnimatedScript.instance.noun();

            float videoTime = (float)AnimatedScript.instance.animated.length;

            if (Animated.activeSelf)
            {
                AudioManagerScript.instance.PauseSoundMusic();
            }
        }

    }

    public void EndNoun()
    {
        AudioManagerScript.instance.UnPauseSoundMusic();

        Animated.SetActive(false);

        AnimatedScript.instance.animated.Stop();

        AnimatedTrigger.instance.IsNounFinished = true;
    }

    public void PronounAnim()
    {
        if (Player.instance.PronounUnlock == false)
        {
            Animated.SetActive(true);

            AnimatedScript.instance.pronoun();

            float videoTime = (float)AnimatedScript.instance.animated.length;

            if (Animated.activeSelf)
            {
                AudioManagerScript.instance.PauseSoundMusic();
            }
        }
    }

    public void EndPronoun()
    {
        AudioManagerScript.instance.UnPauseSoundMusic();

        Animated.SetActive(false);

        AnimatedScript.instance.animated.Stop();

        AnimatedTrigger.instance.IsPronounFinished = true;
    }
    public void VerbAnim()
    {
        if (Player.instance.VerbUnlock == false)
        {
            Animated.SetActive(true);

            AnimatedScript.instance.verb();

            float videoTime = (float)AnimatedScript.instance.animated.length;

            if (Animated.activeSelf)
            {
                AudioManagerScript.instance.PauseSoundMusic();
            }
        }

    }

    public void EndVerb()
    {
        AudioManagerScript.instance.UnPauseSoundMusic();

        Animated.SetActive(false);

        AnimatedScript.instance.animated.Stop();

        AnimatedTrigger.instance.IsVerbFinished = true;
    }

    public void AdjectiveAnim()
    {
        if (Player.instance.AdjectiveUnlock == false)
        {
            Animated.SetActive(true);

            AnimatedScript.instance.adjective();

            float videoTime = (float)AnimatedScript.instance.animated.length;

            if (Animated.activeSelf)
            {
                AudioManagerScript.instance.PauseSoundMusic();
            }
        }

    }

    public void EndAdjective()
    {
        AudioManagerScript.instance.UnPauseSoundMusic();

        Animated.SetActive(false);

        AnimatedScript.instance.animated.Stop();

        AnimatedTrigger.instance.IsAdjectiveFinished = true;
    }

    public void AdverbAnim()
    {
        if (Player.instance.AdverbUnlock == false)
        {
            Animated.SetActive(true);

            AnimatedScript.instance.adverb();

            float videoTime = (float)AnimatedScript.instance.animated.length;

            if (Animated.activeSelf)
            {
                AudioManagerScript.instance.PauseSoundMusic();
            }
        }

    }

    public void EndAdverb()
    {
        AudioManagerScript.instance.UnPauseSoundMusic();

        Animated.SetActive(false);

        AnimatedScript.instance.animated.Stop();

        AnimatedTrigger.instance.IsNounFinished = true;
    }

    public void EndAnim()
    {
        switch(Player.instance.CurrentPlace)
        {
            case 0:
                AudioManagerScript.instance.UnPauseSoundMusic();

                Animated.SetActive(false);

                AnimatedScript.instance.animated.Stop();

                Player.instance.NounUnlock = true;

                Player.instance.SavePlayer();
                break;
            case 1:
                AudioManagerScript.instance.UnPauseSoundMusic();

                Animated.SetActive(false);

                AnimatedScript.instance.animated.Stop();

                Player.instance.PronounUnlock = true;

                Player.instance.SavePlayer();
                break;
            case 2:
                AudioManagerScript.instance.UnPauseSoundMusic();

                Animated.SetActive(false);

                AnimatedScript.instance.animated.Stop();

                Player.instance.VerbUnlock = true;

                Player.instance.SavePlayer();
                break;
            case 3:
                AudioManagerScript.instance.UnPauseSoundMusic();

                Animated.SetActive(false);

                AnimatedScript.instance.animated.Stop();

                Player.instance.AdjectiveUnlock = true;

                Player.instance.SavePlayer();
                break;
            case 4:
                AudioManagerScript.instance.UnPauseSoundMusic();

                Animated.SetActive(false);

                AnimatedScript.instance.animated.Stop();

                Player.instance.AdverbUnlock = true;

                Player.instance.SavePlayer();
                break;
        }
    }
    public void ButtonSound()
    {
        AudioManagerScript.instance.ClickButtons();
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

    public void ChangeHandle(int CurrentPlace)
    {
        switch (CurrentPlace)
        {
            case 0:
                Handle.sprite = Handles[0];
                break;
            case 1:
                Handle.sprite = Handles[1];
                break;
            case 2:
                Handle.sprite = Handles[2];
                break;
            case 3:
                Handle.sprite = Handles[3];
                break;
            case 4:
                Handle.sprite = Handles[4];
                break;
        }
    }
}
