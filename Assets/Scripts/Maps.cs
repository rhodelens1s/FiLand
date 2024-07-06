using UnityEngine;

public class Maps : MonoBehaviour
{

    public static Maps instance;
    void Start()
    {
        Player.instance.LoadPlayer(); 
    }


    public void NounValley()
    {
        SceneController.instance.Quiz();
    }

    public void PronounIsland()
    {
        SceneController.instance.PronounIsland();
    }

}
