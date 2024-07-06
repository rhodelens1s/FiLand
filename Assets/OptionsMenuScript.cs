using UnityEngine;
using UnityEngine.UI;


public class OptionsMenuScript : MonoBehaviour
{
    public Sprite[] Music, Sound;

    void Awake()
    {
        Player.instance.LoadPlayer();
    }

    void Update()
    {
        if (Player.instance.musicOn == 0)
        {
            var MusicSprite = GameObject.Find("musicBtn").GetComponentInChildren<Image>();

            MusicSprite.sprite = Music[0];
        }
        else if (Player.instance.musicOn == 1)
        {
            var MusicSprite = GameObject.Find("musicBtn").GetComponentInChildren<Image>();

            MusicSprite.sprite = Music[1];
        }

        if (Player.instance.soundOn == 0)
        {
            var SoundSprite = GameObject.Find("soundBtn").GetComponentInChildren<Image>();

            SoundSprite.sprite = Sound[0];
        }
        else if (Player.instance.soundOn == 1)
        {
            var SoundSprite = GameObject.Find("soundBtn").GetComponentInChildren<Image>();

            SoundSprite.sprite = Sound[1];
        }
    }

    public void OnMusicBtnCLick()
    {
        if (Player.instance.musicOn == 0)
        {
            Player.instance.musicOn = 1;
            Player.instance.SavePlayer();
        }
        else
        {
            Player.instance.musicOn = 0;
            Player.instance.SavePlayer();
        }

    }

    public void OnSoundBtnCLick()
    {
        if (Player.instance.soundOn == 0)
        {
            Player.instance.soundOn = 1;
            Player.instance.SavePlayer();
        }
        else
        {
            Player.instance.soundOn = 0;
            Player.instance.SavePlayer();
        }
    }
}
