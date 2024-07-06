using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript instance;

    [Header("AudioSource")]
    public AudioSource MusicSFX;
    public AudioSource SoundSFX;
    [Header("AudioClip")]
    public AudioClip CharacterSelectionBGM;
    public AudioClip MainMenuBGM;
    [Header("AnswerSound")]
    public AudioClip CorrectAnswer;
    public AudioClip WrongAnswer;
    [Header("TransitionCloud")]
    public AudioClip Cloud;
    [Header("GameOverSFX")]
    public AudioClip WinSFX;
    public AudioClip LoseSFX;
    [Header("Button")]
    public AudioClip ClickButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }

        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path))
        {
            Player.instance.LoadPlayer();
        }
    }

    private void Update()
    {

        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path))
        {
            ToggleMusic();
            ToggleSound();
        }

    }

    public void ToggleMusic()
    {
        if (Player.instance.musicOn == 0)
        {
            MusicSFX.mute = false;
        }
        else
        {
            MusicSFX.mute = true;
        }
    }

    public void ToggleSound()
    {
        if (Player.instance.soundOn == 0)
        {
            SoundSFX.mute = false;
        }
        else
        {
            SoundSFX.mute = true;
        }
    }
    public void CharacterSelectionMusic()
    {
        MusicSFX.clip = CharacterSelectionBGM;
        MusicSFX.Play();
    }

    public void MainMenuMusic()
    {
        MusicSFX.clip = MainMenuBGM;
        MusicSFX.Play();
    }

    public void CorrectAns()
    {
        SoundSFX.clip = CorrectAnswer;
        SoundSFX.Play();
    }
    public void WrongAns()
    {
        SoundSFX.clip = WrongAnswer;
        SoundSFX.Play();
    }
    public void Clouds()
    {
        SoundSFX.clip = Cloud;
        SoundSFX.Play();
    }

    public void GameOverWin()
    {
        SoundSFX.clip = WinSFX;
        SoundSFX.Play();
    }

    public void GameOverLose()
    {
        SoundSFX.clip = LoseSFX;
        SoundSFX.Play();
    }

    public void ClickButtons()
    {
        SoundSFX.clip = ClickButton;
        SoundSFX.Play();
    }

    public void PauseSoundMusic()
    {
        MusicSFX.Pause();
    }

    public void UnPauseSoundMusic()
    {
        MusicSFX.UnPause();
    }
}
