
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeCharacterUI : MonoBehaviour
{
    public GameObject boyPrefab;
    public GameObject girlPrefab;

    [SerializeField] Animator boy;
    [SerializeField] Animator girl;

    public Transform prevCharacter;
    public Transform selectedCharacter;
    private void Start()
    {
        var c = GameManager.instance.characters[0];
        Button button = boyPrefab.GetComponent<Button>();

        button.onClick.AddListener(() => {
            Debug.Log("boy");
            //boy = GameObject.Find("Icon").GetComponentInChildren<Animator>();
            //girl = GameObject.Find("Icon2").GetComponentInChildren<Animator>();
            Player.instance.currentChar = 0;
            //boy.Play("BoyWaving");
            //girl.Play("New State");


        });

        Image image = boyPrefab.GetComponentInChildren<Image>();
        image.sprite = c.CharacterSelect;


        {

            var c2 = GameManager.instance.characters[1];
            Button button2 = girlPrefab.GetComponent<Button>();

            button2.onClick.AddListener(() => {
                Debug.Log("girl");
                //boy = GameObject.Find("Icon").GetComponentInChildren<Animator>();
                //girl = GameObject.Find("Icon2").GetComponentInChildren<Animator>();
                //GameManager.instance.SetCharacter(c2);
                Player.instance.currentChar = 1;
                //girl.Play("GirlWaving");
                //boy.Play("CharacterSelectionAnim");


            });

            Image image2 = girlPrefab.GetComponentInChildren<Image>();
            image.sprite = c.CharacterSelect;


            {

            }
        }
    }

    private void Update()
    {


        if (selectedCharacter != null)
        {
            //selectedCharacter.localScale = Vector3.Lerp(selectedCharacter.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 10);
        }

        if (prevCharacter != null)
        {
            //prevCharacter.localScale = Vector3.Lerp(prevCharacter.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10);
        }
    }

    private void Awake()
    {
        Player.instance.LoadPlayer();
    }

    public void SaveButton()
    {
            Player.instance.SavePlayer();
            //SceneManager.LoadScene("Main Menu");
            //ProfileEditPanel.SetActive(true);
    }

}
