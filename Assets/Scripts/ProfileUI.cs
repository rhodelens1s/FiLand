using UnityEngine.UI;
using UnityEngine;

public class ProfileUI : MonoBehaviour
{
    public GameObject profilePrefab;

    public InputField inputField;

    public Transform prevCharacter;
    public Transform selectedCharacter;

    private void Start()
    {
        Player.instance.LoadPlayer();
        foreach (Character c in GameManager.instance.characters)
        {
            GameObject profileicon = Instantiate(profilePrefab, transform);
            Button button2 = profileicon.GetComponent<Button>();

            button2.onClick.AddListener(() => {
                GameManager.instance.SetCharacter(c);
                if (selectedCharacter != null)
                {
                    prevCharacter = selectedCharacter;
                }

                selectedCharacter = profileicon.transform;

            });

            Image image = profileicon.GetComponentInChildren<Image>();
            image.sprite = c.icon;

        }

        inputField.GetComponent<InputField>().text = Player.instance.playerName;
    }

    private void Update()
    {
        if (selectedCharacter != null)
        {
            selectedCharacter.localScale = Vector3.Lerp(selectedCharacter.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 10);
        }

        if (prevCharacter != null)
        {
            prevCharacter.localScale = Vector3.Lerp(prevCharacter.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10);
        }
    }
}
