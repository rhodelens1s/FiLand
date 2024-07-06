using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
#pragma warning disable 649

    
    [SerializeField] private QuizGameUI quizGameUI;
    
    [SerializeField] private List<QuizDataScriptable> quizDataList;
    [SerializeField] private float timeInSeconds;
#pragma warning restore 649

    
    private List<Question> questions;
    
    private Question selectedQuestion = new Question();
    public int livesRemaining;
    public int currentScore;
    private QuizDataScriptable dataScriptable;

    private int questionLimit = 5;

    
    [SerializeField] private Animator Character;
    [SerializeField] private Animator Background;

    [SerializeField] private GameObject QuestionImagePanel;
    [SerializeField] private GameObject MultipleChoicePanel;
    [SerializeField] private GameObject TrueOrFalsePanel;
    [SerializeField] private GameObject IdentificationPanel;


    private GameStatus gameStatus = GameStatus.NEXT;

    public GameStatus GameStatus { get { return gameStatus; } }

    public List<QuizDataScriptable> QuizData { get => quizDataList; }

    private void Awake()
    {
        Player.instance.LoadPlayer();
    }

    public void StartGame(int categoryIndex)
    {
        livesRemaining = 3;
        
        questions = new List<Question>();
        dataScriptable = quizDataList[categoryIndex];
        questions.AddRange(dataScriptable.questions);
        
        SelectQuestion();
        gameStatus = GameStatus.PLAYING;
        

        if (Player.instance.currentChar == 0)
        {
            Character.Play("Answer(Boy)");
        }
        else
        {
            Character.Play("Answer(Girl)");
        }
        
    }
    private void SelectQuestion()
    {
        if (questions.Count > 0)
        {
            if (questionLimit <= 0)
            {
                GameEnd("win");
                gameStatus = GameStatus.WIN;
            }
            else
            {
                int val = UnityEngine.Random.Range(0, questions.Count);
                selectedQuestion = questions[val];
                quizGameUI.SetQuestion(selectedQuestion);
                questions.RemoveAt(val);
                Progress(questionLimit - 1);
                questionLimit--;
            }
        }
        else if (questionLimit == 0 && livesRemaining > 0)
        {
            GameEnd("win");
            gameStatus = GameStatus.WIN;
            Debug.Log(gameStatus);
            Debug.Log((Player.instance.currentLvl) + 1 % (Player.instance.currentLvl + 1));
        }

        quizGameUI.ChangeProgressIcon(Player.instance.CurrentPlace, questionLimit);
    }

    private void RepeatCurrentQuestion()
    {
        quizGameUI.SetQuestion(selectedQuestion);
    }

    private void Update()
    {
        if (gameStatus == GameStatus.WIN)
        {
            switch (Player.instance.currentChar)
            {
                case 0:
                    Character.Play("Win(Boy)");
                    break;
                case 1:
                    Character.Play("Win(Girl)");
                    break;
            }
            
        }
    }
    IEnumerator CorrectAnswer()
    {
        switch (Player.instance.currentChar)
        {
            case 0:
                Character.Play("CorrectAnswer(Boy)");
                Background.Play("CharacterWalking");
                yield return new WaitForSeconds(3);
                Character.Play("Answer(Boy)");
                Background.Play("Answer");
                break;
            case 1:
                Character.Play("CorrectAnswer(Girl)");
                Background.Play("CharacterWalking");
                yield return new WaitForSeconds(3);
                Character.Play("Answer(Girl)");
                Background.Play("Answer");
                break;
        }

    }

    IEnumerator WrongAnswer()
    {
        switch (Player.instance.currentChar)
        {
            case 0:
                Character.Play("WrongAnswer(Boy)");
                Background.Play("CharacterWalking");
                yield return new WaitForSeconds(3);
                Character.Play("Answer(Boy)");
                Background.Play("Answer");
                break;
            case 1:
                Character.Play("WrongAnswer(Girl)");
                Background.Play("CharacterWalking");
                yield return new WaitForSeconds(3);
                Character.Play("Answer(Girl)");
                Background.Play("Answer");
                break;
        }
    }

    IEnumerator GameWin()
    {
        yield return new WaitForSeconds(0);
        quizGameUI.WinPanel.SetActive(true);
    }

    IEnumerator WinReward()
    {
        quizGameUI.Rewards(Player.instance.CurrentPlace);
        yield return new WaitForSeconds(0);
        quizGameUI.RewardPanel.SetActive(true);
    }
    public bool Answer(string selectedOption)
    {
       
        bool correct = false;
 
                if (selectedQuestion.correctAns == selectedOption)
                {
                    
                    correct = true;
                    currentScore += 50;
                    AudioManagerScript.instance.CorrectAns();
                    Debug.Log("Correct!");
                    if(questionLimit > 0)
                    {
                        StartCoroutine(CorrectAnswer());
                    }
                    Invoke("SelectQuestion", 3f);

                }
                else
                {

                    livesRemaining--;
                    quizGameUI.ReduceLife(livesRemaining);
                    AudioManagerScript.instance.WrongAns();
                    Debug.Log("Wrong!");
                    if(livesRemaining > 0 && questionLimit > 0)
                    {
                        StartCoroutine(WrongAnswer());
                    }
                    Invoke("SelectQuestion", 3f);

                    if (livesRemaining == 0)
                    {
                        questions.Clear();
                        GameEnd("lose");

                    }
                }
      
        return correct;
    }

    private void UnlockNewLevel()
    {
        if (Player.instance.currentLvl + 1 == Player.instance.lvlUnlocked)
        {
            Player.instance.lvlUnlocked++;
            Player.instance.SavePlayer();
            UnlockNewPlace();
        }
    }

    private void UnlockNewPlace()
    {
        if (Player.instance.lvlUnlocked % 10 == 0 + 1)
        {
            Player.instance.placeUnlocked++;
            Player.instance.SavePlayer();
        }
    }

    private void GameEnd(string status)
    {
        if (status == "win")
        {
            switch (Player.instance.CurrentPlace)
            {
                case 0:
                    if (Player.instance.currentLvl != 9)
                    {
                        gameStatus = GameStatus.WIN;

                        StartCoroutine(GameWin());

                        AudioManagerScript.instance.GameOverWin();
                    }
                    else
                    {
                        gameStatus = GameStatus.WIN;

                        StartCoroutine(WinReward());

                        AudioManagerScript.instance.GameOverWin();
                    }
                    break;
                case 1:
                    if (Player.instance.currentLvl != 19)
                    {
                        gameStatus = GameStatus.WIN;

                        StartCoroutine(GameWin());

                        AudioManagerScript.instance.GameOverWin();
                    }
                    else
                    {
                        gameStatus = GameStatus.WIN;

                        StartCoroutine(WinReward());

                        AudioManagerScript.instance.GameOverWin();
                    }
                    break;
                case 2:
                    if (Player.instance.currentLvl != 29)
                    {
                        gameStatus = GameStatus.WIN;

                        StartCoroutine(GameWin());

                        AudioManagerScript.instance.GameOverWin();
                    }
                    else
                    {
                        gameStatus = GameStatus.WIN;

                        StartCoroutine(WinReward());

                        AudioManagerScript.instance.GameOverWin();
                    }
                    break;
                case 3:
                    if (Player.instance.currentLvl != 39)
                    {
                        gameStatus = GameStatus.WIN;

                        StartCoroutine(GameWin());

                        AudioManagerScript.instance.GameOverWin();
                    }
                    else
                    {
                        gameStatus = GameStatus.WIN;

                        StartCoroutine(WinReward());

                        AudioManagerScript.instance.GameOverWin();
                    }
                    break;
                case 4:
                    if (Player.instance.currentLvl != 49)
                    {
                        gameStatus = GameStatus.WIN;

                        StartCoroutine(GameWin());

                        AudioManagerScript.instance.GameOverWin();
                    }
                    else
                    {
                        gameStatus = GameStatus.WIN;

                        StartCoroutine(WinReward());

                        AudioManagerScript.instance.GameOverWin();
                    }
                    break;
            }
            
        }
      
        else if (status == "lose")
        {
            gameStatus= GameStatus.LOSE;

            quizGameUI.LosePanel.SetActive(true);

            AudioManagerScript.instance.GameOverLose();

        }
    }

    public void Back()
    {
        SceneController.instance.playGame();

        switch (Player.instance.currentLvl)
        {
            case 0:
                if (currentScore > Player.instance.Lvl1Score)
                {
                    Player.instance.Lvl1Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 1:
                if (currentScore > Player.instance.Lvl2Score)
                {
                    Player.instance.Lvl2Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 2:
                if (currentScore > Player.instance.Lvl3Score)
                {
                    Player.instance.Lvl3Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 3:
                if (currentScore > Player.instance.Lvl4Score)
                {
                    Player.instance.Lvl4Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 4:
                if (currentScore > Player.instance.Lvl5Score)
                {
                    Player.instance.Lvl5Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 5:
                if (currentScore > Player.instance.Lvl6Score)
                {
                    Player.instance.Lvl6Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 6:
                if (currentScore > Player.instance.Lvl7Score)
                {
                    Player.instance.Lvl7Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 7:
                if (currentScore > Player.instance.Lvl8Score)
                {
                    Player.instance.Lvl8Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 8:
                if (currentScore > Player.instance.Lvl9Score)
                {
                    Player.instance.Lvl9Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 9:
                if (currentScore > Player.instance.Lvl10Score)
                {
                    Player.instance.Lvl10Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                            Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 10:
                if (currentScore > Player.instance.Lvl11Score)
                {
                    Player.instance.Lvl11Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 11:
                if (currentScore > Player.instance.Lvl12Score)
                {
                    Player.instance.Lvl12Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 12:
                if (currentScore > Player.instance.Lvl13Score)
                {
                    Player.instance.Lvl13Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 13:
                if (currentScore > Player.instance.Lvl14Score)
                {
                    Player.instance.Lvl14Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 14:
                if (currentScore > Player.instance.Lvl15Score)
                {
                    Player.instance.Lvl15Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 15:
                if (currentScore > Player.instance.Lvl16Score)
                {
                    Player.instance.Lvl16Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 16:
                if (currentScore > Player.instance.Lvl17Score)
                {
                    Player.instance.Lvl17Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 17:
                if (currentScore > Player.instance.Lvl18Score)
                {
                    Player.instance.Lvl18Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 18:
                if (currentScore > Player.instance.Lvl19Score)
                {
                    Player.instance.Lvl19Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 19:
                if (currentScore > Player.instance.Lvl20Score)
                {
                    Player.instance.Lvl20Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                            Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 20:
                if (currentScore > Player.instance.Lvl21Score)
                {
                    Player.instance.Lvl21Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 21:
                if (currentScore > Player.instance.Lvl22Score)
                {
                    Player.instance.Lvl22Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 22:
                if (currentScore > Player.instance.Lvl23Score)
                {
                    Player.instance.Lvl23Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 23:
                if (currentScore > Player.instance.Lvl24Score)
                {
                    Player.instance.Lvl24Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 24:
                if (currentScore > Player.instance.Lvl25Score)
                {
                    Player.instance.Lvl25Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 25:
                if (currentScore > Player.instance.Lvl26Score)
                {
                    Player.instance.Lvl26Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 26:
                if (currentScore > Player.instance.Lvl27Score)
                {
                    Player.instance.Lvl27Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 27:
                if (currentScore > Player.instance.Lvl28Score)
                {
                    Player.instance.Lvl28Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 28:
                if (currentScore > Player.instance.Lvl29Score)
                {
                    Player.instance.Lvl29Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                            Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 29:
                if (currentScore > Player.instance.Lvl30Score)
                {
                    Player.instance.Lvl30Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 30:
                if (currentScore > Player.instance.Lvl31Score)
                {
                    Player.instance.Lvl31Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 31:
                if (currentScore > Player.instance.Lvl32Score)
                {
                    Player.instance.Lvl32Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 32:
                if (currentScore > Player.instance.Lvl33Score)
                {
                    Player.instance.Lvl33Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 33:
                if (currentScore > Player.instance.Lvl34Score)
                {
                    Player.instance.Lvl34Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 34:
                if (currentScore > Player.instance.Lvl35Score)
                {
                    Player.instance.Lvl35Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 35:
                if (currentScore > Player.instance.Lvl36Score)
                {
                    Player.instance.Lvl36Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 36:
                if (currentScore > Player.instance.Lvl37Score)
                {
                    Player.instance.Lvl37Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 37:
                if (currentScore > Player.instance.Lvl38Score)
                {
                    Player.instance.Lvl38Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 38:
                if (currentScore > Player.instance.Lvl39Score)
                {
                    Player.instance.Lvl39Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                            Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 39:
                if (currentScore > Player.instance.Lvl40Score)
                {
                    Player.instance.Lvl40Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 40:
                if (currentScore > Player.instance.Lvl41Score)
                {
                    Player.instance.Lvl41Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 41:
                if (currentScore > Player.instance.Lvl42Score)
                {
                    Player.instance.Lvl42Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 42:
                if (currentScore > Player.instance.Lvl43Score)
                {
                    Player.instance.Lvl43Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 43:
                if (currentScore > Player.instance.Lvl44Score)
                {
                    Player.instance.Lvl44Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 44:
                if (currentScore > Player.instance.Lvl45Score)
                {
                    Player.instance.Lvl45Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 45:
                if (currentScore > Player.instance.Lvl46Score)
                {
                    Player.instance.Lvl46Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 46:
                if (currentScore > Player.instance.Lvl47Score)
                {
                    Player.instance.Lvl47Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 47:
                if (currentScore > Player.instance.Lvl48Score)
                {
                    Player.instance.Lvl48Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 48:
                if (currentScore > Player.instance.Lvl49Score)
                {
                    Player.instance.Lvl49Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;
            case 49:
                if (currentScore > Player.instance.Lvl50Score)
                {
                    Player.instance.Lvl50Score = currentScore;
                }
                switch (GameStatus)
                {
                    case GameStatus.WIN:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        //UnlockNewLevel();

                        SceneController.instance.playGame();
                        break;
                    case GameStatus.LOSE:

                        Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                            Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                        Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                            Player.instance.AdverbParkTotalScore;

                        Player.instance.SavePlayer();

                        SceneController.instance.playGame();
                        break;
                }
                break;

        }

    }

    public void Retry()
    {
        
        SceneController.instance.Quiz();

    }

    public void Next()
    {
        switch (Player.instance.currentLvl)
        {
            case 0:
                if (currentScore > Player.instance.Lvl1Score)
                {
                    Player.instance.Lvl1Score = currentScore;
                }
                Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                    Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 1:
                if (currentScore > Player.instance.Lvl2Score)
                {
                    Player.instance.Lvl2Score = currentScore;
                }
                Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                   Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 2:
                if (currentScore > Player.instance.Lvl3Score)
                {
                    Player.instance.Lvl3Score = currentScore;
                }
                Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                   Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 3:
                if (currentScore > Player.instance.Lvl4Score)
                {
                    Player.instance.Lvl4Score = currentScore;
                }
                Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                   Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 4:
                if (currentScore > Player.instance.Lvl5Score)
                {
                    Player.instance.Lvl5Score = currentScore;
                }
                Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                   Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 5:
                if (currentScore > Player.instance.Lvl6Score)
                {
                    Player.instance.Lvl6Score = currentScore;
                }
                Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                   Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 6:
                if (currentScore > Player.instance.Lvl7Score)
                {
                    Player.instance.Lvl7Score = currentScore;
                }
                Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                   Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 7:
                if (currentScore > Player.instance.Lvl8Score)
                {
                    Player.instance.Lvl8Score = currentScore;
                }
                Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                   Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 8:
                if (currentScore > Player.instance.Lvl9Score)
                {
                    Player.instance.Lvl9Score = currentScore;
                }
                Player.instance.NounValleyTotalScore = Player.instance.Lvl1Score + Player.instance.Lvl2Score + Player.instance.Lvl3Score + Player.instance.Lvl4Score + Player.instance.Lvl5Score + Player.instance.Lvl6Score +
                   Player.instance.Lvl7Score + Player.instance.Lvl8Score + Player.instance.Lvl9Score + Player.instance.Lvl10Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 9:
                if (currentScore > Player.instance.Lvl10Score)
                {
                    Player.instance.Lvl10Score = currentScore;
                }
                Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                     Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 10:
                if (currentScore > Player.instance.Lvl11Score)
                {
                    Player.instance.Lvl11Score = currentScore;
                }
                Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                     Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 11:
                if (currentScore > Player.instance.Lvl12Score)
                {
                    Player.instance.Lvl12Score = currentScore;
                }
                Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                     Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 12:
                if (currentScore > Player.instance.Lvl13Score)
                {
                    Player.instance.Lvl13Score = currentScore;
                }
                Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                     Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 13:
                if (currentScore > Player.instance.Lvl14Score)
                {
                    Player.instance.Lvl14Score = currentScore;
                }
                Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                     Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 14:
                if (currentScore > Player.instance.Lvl15Score)
                {
                    Player.instance.Lvl15Score = currentScore;
                }
                Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                     Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 15:
                if (currentScore > Player.instance.Lvl16Score)
                {
                    Player.instance.Lvl16Score = currentScore;
                }
                Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                     Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 16:
                if (currentScore > Player.instance.Lvl17Score)
                {
                    Player.instance.Lvl17Score = currentScore;
                }
                Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                     Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 17:
                if (currentScore > Player.instance.Lvl18Score)
                {
                    Player.instance.Lvl18Score = currentScore;
                }
                Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                     Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 18:
                if (currentScore > Player.instance.Lvl19Score)
                {
                    Player.instance.Lvl19Score = currentScore;
                }
                Player.instance.PronounIslandTotalScore = Player.instance.Lvl11Score + Player.instance.Lvl12Score + Player.instance.Lvl13Score + Player.instance.Lvl14Score + Player.instance.Lvl15Score + Player.instance.Lvl16Score +
                     Player.instance.Lvl17Score + Player.instance.Lvl18Score + Player.instance.Lvl19Score + Player.instance.Lvl20Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 19:
                if (currentScore > Player.instance.Lvl20Score)
                {
                    Player.instance.Lvl20Score = currentScore;
                }
                Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                    Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 20:
                if (currentScore > Player.instance.Lvl21Score)
                {
                    Player.instance.Lvl21Score = currentScore;
                }
                Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                    Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 21:
                if (currentScore > Player.instance.Lvl22Score)
                {
                    Player.instance.Lvl22Score = currentScore;
                }
                Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                    Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 22:
                if (currentScore > Player.instance.Lvl23Score)
                {
                    Player.instance.Lvl23Score = currentScore;
                }
                Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                    Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 23:
                if (currentScore > Player.instance.Lvl24Score)
                {
                    Player.instance.Lvl24Score = currentScore;
                }
                Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                    Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 24:
                if (currentScore > Player.instance.Lvl25Score)
                {
                    Player.instance.Lvl25Score = currentScore;
                }
                Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                    Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 25:
                if (currentScore > Player.instance.Lvl26Score)
                {
                    Player.instance.Lvl26Score = currentScore;
                }
                Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                    Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 26:
                if (currentScore > Player.instance.Lvl27Score)
                {
                    Player.instance.Lvl27Score = currentScore;
                }
                Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                    Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 27:
                if (currentScore > Player.instance.Lvl28Score)
                {
                    Player.instance.Lvl28Score = currentScore;
                }
                Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                    Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 28:
                if (currentScore > Player.instance.Lvl29Score)
                {
                    Player.instance.Lvl29Score = currentScore;
                }
                Player.instance.VerbTownTotalScore = Player.instance.Lvl21Score + Player.instance.Lvl22Score + Player.instance.Lvl23Score + Player.instance.Lvl24Score + Player.instance.Lvl25Score + Player.instance.Lvl26Score +
                    Player.instance.Lvl27Score + Player.instance.Lvl28Score + Player.instance.Lvl29Score + Player.instance.Lvl30Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 29:
                if (currentScore > Player.instance.Lvl30Score)
                {
                    Player.instance.Lvl30Score = currentScore;
                }
                Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                    Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 30:
                if (currentScore > Player.instance.Lvl31Score)
                {
                    Player.instance.Lvl31Score = currentScore;
                }
                Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                    Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 31:
                if (currentScore > Player.instance.Lvl32Score)
                {
                    Player.instance.Lvl32Score = currentScore;
                }
                Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                    Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 32:
                if (currentScore > Player.instance.Lvl33Score)
                {
                    Player.instance.Lvl33Score = currentScore;
                }
                Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                    Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 33:
                if (currentScore > Player.instance.Lvl34Score)
                {
                    Player.instance.Lvl34Score = currentScore;
                }
                Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                    Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 34:
                if (currentScore > Player.instance.Lvl35Score)
                {
                    Player.instance.Lvl35Score = currentScore;
                }
                Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                    Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 35:
                if (currentScore > Player.instance.Lvl36Score)
                {
                    Player.instance.Lvl36Score = currentScore;
                }
                Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                    Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 36:
                if (currentScore > Player.instance.Lvl37Score)
                {
                    Player.instance.Lvl37Score = currentScore;
                }
                Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                    Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 37:
                if (currentScore > Player.instance.Lvl38Score)
                {
                    Player.instance.Lvl38Score = currentScore;
                }
                Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                    Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 38:
                if (currentScore > Player.instance.Lvl39Score)
                {
                    Player.instance.Lvl39Score = currentScore;
                }
                Player.instance.AdjectivePalaceTotalScore = Player.instance.Lvl31Score + Player.instance.Lvl32Score + Player.instance.Lvl33Score + Player.instance.Lvl34Score + Player.instance.Lvl35Score + Player.instance.Lvl36Score +
                    Player.instance.Lvl37Score + Player.instance.Lvl38Score + Player.instance.Lvl39Score + Player.instance.Lvl40Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 39:
                if (currentScore > Player.instance.Lvl40Score)
                {
                    Player.instance.Lvl40Score = currentScore;
                }
                Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                    Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 40:
                if (currentScore > Player.instance.Lvl41Score)
                {
                    Player.instance.Lvl41Score = currentScore;
                }
                Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                    Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 41:
                if (currentScore > Player.instance.Lvl42Score)
                {
                    Player.instance.Lvl42Score = currentScore;
                }
                Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                    Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 42:
                if (currentScore > Player.instance.Lvl43Score)
                {
                    Player.instance.Lvl43Score = currentScore;
                }
                Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                    Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 43:
                if (currentScore > Player.instance.Lvl44Score)
                {
                    Player.instance.Lvl44Score = currentScore;
                }
                Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                    Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 44:
                if (currentScore > Player.instance.Lvl45Score)
                {
                    Player.instance.Lvl45Score = currentScore;
                }
                Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                    Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 45:
                if (currentScore > Player.instance.Lvl46Score)
                {
                    Player.instance.Lvl46Score = currentScore;
                }
                Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                    Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 46:
                if (currentScore > Player.instance.Lvl47Score)
                {
                    Player.instance.Lvl47Score = currentScore;
                }
                Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                    Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 47:
                if (currentScore > Player.instance.Lvl48Score)
                {
                    Player.instance.Lvl48Score = currentScore;
                }
                Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                    Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 48:
                if (currentScore > Player.instance.Lvl49Score)
                {
                    Player.instance.Lvl49Score = currentScore;
                }
                Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                    Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
            case 49:
                if (currentScore > Player.instance.Lvl50Score)
                {
                    Player.instance.Lvl50Score = currentScore;
                }
                Player.instance.AdverbParkTotalScore = Player.instance.Lvl41Score + Player.instance.Lvl42Score + Player.instance.Lvl43Score + Player.instance.Lvl44Score + Player.instance.Lvl45Score + Player.instance.Lvl46Score +
                    Player.instance.Lvl47Score + Player.instance.Lvl48Score + Player.instance.Lvl49Score + Player.instance.Lvl50Score;

                Player.instance.playerTotalScore = Player.instance.NounValleyTotalScore + Player.instance.PronounIslandTotalScore + Player.instance.VerbTownTotalScore + Player.instance.AdjectivePalaceTotalScore +
                    Player.instance.AdverbParkTotalScore;

                Player.instance.highestLvl++;

                UnlockNewLevel();

                Player.instance.currentLvl++;

                Player.instance.SavePlayer();

                SceneController.instance.Quiz();
                break;
        }
        
    }

    public void Progress(int value)
    {
        quizGameUI.slider.value = value;
    }

    public void ButtonSound()
    {
        AudioManagerScript.instance.ClickButtons();
    }
}

[System.Serializable]
public class Question
{
    public string questionInfo;
    public Sprite questionImage;
    public AudioClip QuestionVoiceOver;
    public QuestionType questionType;
    public Sprite Picture;
    public List<string> MultipleChoiceOptions;
    public List<string> TrueOrFalseOptions;
    public List<string> MultipleChoiceV2Options;
    public string correctAns;
}

[System.Serializable]
public enum QuestionType
{
    MultipleChoice,
    TrueOrFalse,
    Identification,
    TanongNaMayPicture,
    MultipleChoiceV2
}

[SerializeField]
public enum GameStatus
{
    PLAYING,
    WIN,
    LOSE,
    NEXT
}