using UnityEngine;

public class AnimatedTrigger : MonoBehaviour
{
    public static AnimatedTrigger instance;

    public bool IsIntroFinished;
    public bool IsNounFinished;
    public bool IsPronounFinished;
    public bool IsVerbFinished;
    public bool IsAdjectiveFinished;
    public bool IsAdverbFinished;

    public void Awake()
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
}
