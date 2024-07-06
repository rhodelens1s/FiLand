using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    public GameObject boyPrefab;
    public GameObject girlPrefab;

    [SerializeField] Animator boy;
    [SerializeField] Animator girl;

    public Transform prevCharacter;
    public Transform selectedCharacter;

    private void Start()
    {
        GameObject option = Instantiate(boyPrefab, transform);
        var c = GameManager.instance.characters[0];
        Button button = option.GetComponent<Button>();

            button.onClick.AddListener(() => {
                boy = GameObject.Find("Icon").GetComponentInChildren<Animator>();
                girl = GameObject.Find("Icon2").GetComponentInChildren<Animator>();
                GameManager.instance.SetCharacter(c);
                if (selectedCharacter != null)
                {
                    prevCharacter = selectedCharacter;
                }

                selectedCharacter = option.transform;

                boy.Play("BoyWaving");
                girl.Play("New State");

                ButtonSound();

            });

            Image image = option.GetComponentInChildren<Image>();
            image.sprite = c.CharacterSelect;


        {


            GameObject option2 = Instantiate(girlPrefab, transform);
            var c2 = GameManager.instance.characters[1];
            Button button2 = option2.GetComponent<Button>();

            button2.onClick.AddListener(() => {
                boy = GameObject.Find("Icon").GetComponentInChildren<Animator>();
                girl = GameObject.Find("Icon2").GetComponentInChildren<Animator>();
                GameManager.instance.SetCharacter(c2);
                if (selectedCharacter != null)
                {
                    prevCharacter = selectedCharacter;
                }

                selectedCharacter = option2.transform;
                girl.Play("GirlWaving");
                boy.Play("CharacterSelectionAnim");

                ButtonSound();

            });

            Image image2 = option.GetComponentInChildren<Image>();
            image.sprite = c.CharacterSelect;


            {

            }
        }
    }

    private void Update()
    {

        try
        {
            if (selectedCharacter != null)
            {
                selectedCharacter.localScale = Vector3.Lerp(selectedCharacter.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 10);
            }

            if (prevCharacter != null)
            {
                prevCharacter.localScale = Vector3.Lerp(prevCharacter.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10);
            }

            if (selectedCharacter == prevCharacter)
            {
                selectedCharacter.localScale = Vector3.Lerp(selectedCharacter.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 10);
            }
        }catch (Exception)
        {

        }
    }

    public void ButtonSound()
    {
        AudioManagerScript.instance.ClickButtons();
    }
}
