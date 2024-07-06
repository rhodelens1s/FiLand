using UnityEngine;
using UnityEngine.UI;

public class ChangeCharacter : MonoBehaviour
{
    public Button boyPrefab;
    public Button girlPrefab;

    public Transform prevCharacter;
    public Transform selectedCharacter;

    void Start()
    {
        
    }

    void Awake()
    {
       
    }

    public void Boy()
    {
        var c = GameManager.instance.characters[1];

        GameManager.instance.SetCharacter(c);

        /*if (selectedCharacter != null)
        {
            prevCharacter = selectedCharacter;
        }

        selectedCharacter = boyPrefab.transform;

        Image image = boyPrefab.GetComponentInChildren<Image>();
        image.sprite = c.CharacterSelect;*/
    }

    public void Girl()
    {
        var c2 = GameManager.instance.characters[1];

        GameManager.instance.SetCharacter(c2);

        /*if (selectedCharacter != null)
        {
            prevCharacter = selectedCharacter;
        }

        selectedCharacter = girlPrefab.transform;

        Image image = girlPrefab.GetComponentInChildren<Image>();
        image.sprite = c2.CharacterSelect;*/
    }

    public void Save()
    {
        Player.instance.SavePlayer();
    }
}
