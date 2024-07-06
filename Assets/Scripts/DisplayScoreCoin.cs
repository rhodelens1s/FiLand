
using UnityEngine;
using UnityEngine.UI;

public class DisplayScoreCoin : MonoBehaviour
{
    public Text playerScoreText;
    void Start()
    {
        Player.instance.LoadPlayer();
        displayTexts();
    }

    
    public void displayTexts()
    {
        playerScoreText.text = Player.instance.playerTotalScore.ToString();
    }
}
