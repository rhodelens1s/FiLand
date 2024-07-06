
using UnityEngine;
using UnityEngine.Video;

public class AnimatedScript : MonoBehaviour
{
    public static AnimatedScript instance;

    public VideoPlayer animated;
    public VideoClip Intro, Noun, Pronoun, Verb, Adjective, Adverb;

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
    }


    public void intro()
    {
        animated.clip = Intro;
        animated.Play();
    }

    public void noun()
    {
        animated.clip = Noun;
        animated.Play();
    }

    public void pronoun()
    {
        animated.clip = Pronoun;
        animated.Play();
    }

    public void verb()
    {
        animated.clip = Verb;
        animated.Play();
    }

    public void adjective()
    {
        animated.clip = Adjective;
        animated.Play();
    }

    public void adverb()
    {
        animated.clip = Adverb;
        animated.Play();
    }
    private void ClearRenderTexture()
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = animated.targetTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
    }
}
