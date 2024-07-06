using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryScript : MonoBehaviour
{
    public Image Handle;
    public List<Sprite> Handles;
    public GameObject Animated;
    public List<GameObject> VideoLock;
    public List<Button> VideoButton;

    public Slider slider;

    bool on_drag = false;

    private void Start()
    {
        if(Player.instance.IntroUnlock == true)
        {
            VideoLock[0].SetActive(false);
            VideoButton[0].interactable = true;
        }

        if (Player.instance.NounUnlock == true)
        {
            VideoLock[1].SetActive(false);
            VideoButton[1].interactable = true;
        }

        if (Player.instance.PronounUnlock == true)
        {
            VideoLock[2].SetActive(false);
            VideoButton[2].interactable = true;
        }

        if (Player.instance.VerbUnlock == true)
        {
            VideoLock[3].SetActive(false);
            VideoButton[3].interactable = true;
        }

        if (Player.instance.AdjectiveUnlock == true)
        {
            VideoLock[4].SetActive(false);
            VideoButton[4].interactable = true;
        }

        if (Player.instance.AdverbUnlock == true)
        {
            VideoLock[5].SetActive(false);
            VideoButton[5].interactable = true;
        }

    }

    void Update()
    {
        if (AnimatedScript.instance.animated.isPlaying && !on_drag)
        {
            slider.value = (float)AnimatedScript.instance.animated.frame / (float)AnimatedScript.instance.animated.frameCount;
        }
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

    public void PlayAnimation(int ButtonIndex)
    {
        switch (ButtonIndex)
        {
            case 0:
                Handle.sprite = Handles[0];
                IntroAnim();
                break;
            case 1:
                Handle.sprite = Handles[1];
                NounAnim();
                break;
            case 2:
                Handle.sprite = Handles[2];
                PronounAnim();
                break;
            case 3:
                Handle.sprite = Handles[3];
                VerbAnim();
                break;
            case 4:
                Handle.sprite = Handles[4];
                AdjectiveAnim();
                break;
            case 5:
                Handle.sprite = Handles[5];
                AdverbAnim();
                break;
        }
    }

    public void IntroAnim()
    {
        Animated.SetActive(true);

        AnimatedScript.instance.intro();

        float videoTime = (float)AnimatedScript.instance.animated.length;

        if (Animated.activeSelf)
        {
            AudioManagerScript.instance.PauseSoundMusic();
        }

    }

    public void EndIntro()
    {
        AudioManagerScript.instance.UnPauseSoundMusic();

        Animated.SetActive(false);

        AnimatedScript.instance.animated.Stop();

        AnimatedTrigger.instance.IsNounFinished = true;
    }

    public void NounAnim()
    {
        Animated.SetActive(true);

        AnimatedScript.instance.noun();

        float videoTime = (float)AnimatedScript.instance.animated.length;

        if (Animated.activeSelf)
        {
            AudioManagerScript.instance.PauseSoundMusic();
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
        Animated.SetActive(true);

        AnimatedScript.instance.pronoun();

        float videoTime = (float)AnimatedScript.instance.animated.length;

        if (Animated.activeSelf)
        {
            AudioManagerScript.instance.PauseSoundMusic();
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
        Animated.SetActive(true);

        AnimatedScript.instance.verb();

        float videoTime = (float)AnimatedScript.instance.animated.length;

        if (Animated.activeSelf)
        {
            AudioManagerScript.instance.PauseSoundMusic();
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
        Animated.SetActive(true);

        AnimatedScript.instance.adjective();

        float videoTime = (float)AnimatedScript.instance.animated.length;

        if (Animated.activeSelf)
        {
            AudioManagerScript.instance.PauseSoundMusic();
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
        Animated.SetActive(true);

        AnimatedScript.instance.adverb();

        float videoTime = (float)AnimatedScript.instance.animated.length;

        if (Animated.activeSelf)
        {
            AudioManagerScript.instance.PauseSoundMusic();
        }
    }

    public void EndAdverb()
    {
        AudioManagerScript.instance.UnPauseSoundMusic();

        Animated.SetActive(false);

        AnimatedScript.instance.animated.Stop();

        AnimatedTrigger.instance.IsNounFinished = true;
    }

    public void EndAnim(int ButtonIndex_)
    {
        switch (ButtonIndex_)
        {
            case 0:
                AudioManagerScript.instance.UnPauseSoundMusic();

                Animated.SetActive(false);

                AnimatedScript.instance.animated.Stop();
                break;
            case 1:
                AudioManagerScript.instance.UnPauseSoundMusic();

                Animated.SetActive(false);

                AnimatedScript.instance.animated.Stop();
                break;
            case 2:
                AudioManagerScript.instance.UnPauseSoundMusic();

                Animated.SetActive(false);

                AnimatedScript.instance.animated.Stop();
                break;
            case 3:
                AudioManagerScript.instance.UnPauseSoundMusic();

                Animated.SetActive(false);

                AnimatedScript.instance.animated.Stop();
                break;
            case 4:
                AudioManagerScript.instance.UnPauseSoundMusic();

                Animated.SetActive(false);

                AnimatedScript.instance.animated.Stop();
                break;
            case 5:
                AudioManagerScript.instance.UnPauseSoundMusic();

                Animated.SetActive(false);

                AnimatedScript.instance.animated.Stop();
                break;

        }
    }
    public void MainMenuButton()
    {
        SceneController.instance.mainMenu();
    }
    public void ButtonSound()
    {
        AudioManagerScript.instance.ClickButtons();
    }
}
