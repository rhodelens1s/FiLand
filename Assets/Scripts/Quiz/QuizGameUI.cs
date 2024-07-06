using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizGameUI : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private QuizManager quizManager;             
    [SerializeField] private Text scoreText;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private Text questionInfoText;                 
    [SerializeField] private List<Button> MultipleChoiceOptions;                 
    [SerializeField] private List<Button> TrueOrFalseOptions;
    [SerializeField] private List<Button> MultipleChoiceLAOptions;
    [SerializeField] private GameObject MultipleChoicePanel, TrueOrFalsePanel, IdentificaationPanel, QPicture, VoiceToggle, PictureToggle, MultipleChoiceV2Panel, MCV2Options;
    [SerializeField] private InputField IdentificationAnswer;
    [SerializeField] private Button IdentificationBtn;
    [SerializeField] private Button LongAnswerOptionsBtn;
    [SerializeField] private List<Image> LifeImage;
    [SerializeField] private GameObject QuestionPanel;
    [SerializeField] public GameObject WinPanel, LosePanel, RewardPanel;
    [SerializeField] public List<Sprite> Level;

    [SerializeField] private Image questionImg, Picture;
    [SerializeField] private AudioSource questionAudio;
    [SerializeField] public Slider slider;
    [SerializeField] public List<Sprite> QuizBG;
    [SerializeField] public Image[] Background;
    [SerializeField] public Image ProgressIcon;
    [SerializeField] public List<Sprite> NounIcon;
    [SerializeField] public List<Sprite> PronounIcon;
    [SerializeField] public List<Sprite> VerbIcon;
    [SerializeField] public List<Sprite> AdjectiveIcon;
    [SerializeField] public List<Sprite> AdverbIcon;
    [SerializeField] public List<Sprite> Reward;
    [SerializeField] public Image RewardImg;

    [SerializeField] public Text WinScore;

    [SerializeField] Animator Heart3;
    [SerializeField] Animator Heart2;
    [SerializeField] Animator Heart1;
#pragma warning restore 649
    private Question question;  
    private bool answered = false;  

    private void Awake()
    {
        Player.instance.LoadPlayer();
    }

    private void Update()
    {
        scoreText.text = quizManager.currentScore.ToString();
        DisplayWinScore();

    }
    private void Start()
    {

        Levels(Player.instance.currentLvl);
        
        for (int i = 0; i < MultipleChoiceOptions.Count; i++)
        {
            Button localBtn = MultipleChoiceOptions[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        for (int i = 0; i < TrueOrFalseOptions.Count; i++)
        {
            Button localBtn = TrueOrFalseOptions[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        for (int i = 0; i < MultipleChoiceLAOptions.Count; i++)
        {
            Button localBtn = MultipleChoiceLAOptions[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        IdentificationBtn.onClick.AddListener(() => OnClick(IdentificationBtn));

        quizManager.StartGame(Player.instance.currentLvl);

        QuizBGLvl(Player.instance.currentLvl);

    }

    public void SetQuestion(Question question)
    {
        
        this.question = question;
        

        List<string> MultipleChoiceOptionsRandom = ShuffleList.ShuffleListItems<string>(question.MultipleChoiceOptions);
        List<string> TrueOrFalseOptionsRandom = ShuffleList.ShuffleListItems<string>(question.TrueOrFalseOptions);
        List<string> MultipleChoiceV2OptionsRandom = ShuffleList.ShuffleListItems<string>(question.MultipleChoiceV2Options);

        switch (question.questionType)
        {
            case QuestionType.MultipleChoice:
                QuestionPanel.SetActive(true);
                MultipleChoicePanel.SetActive(true);
                TrueOrFalsePanel.SetActive(false);
                IdentificaationPanel.SetActive(false);
                VoiceToggle.SetActive(true);
                PictureToggle.SetActive(false);
                MultipleChoiceV2Panel.SetActive(false);
                MCV2Options.SetActive(false);

                for (int i = 0; i < MultipleChoiceOptions.Count; i++)
                {

                    MultipleChoiceOptions[i].GetComponentInChildren<Text>().text = MultipleChoiceOptionsRandom[i];
                    MultipleChoiceOptions[i].name = MultipleChoiceOptionsRandom[i];    
                    MultipleChoiceOptions[i].image.color = Color.white;     
                }
                break;
            case QuestionType.TrueOrFalse:
                QuestionPanel.SetActive(true);
                MultipleChoicePanel.SetActive(false);
                TrueOrFalsePanel.SetActive(true);
                IdentificaationPanel.SetActive(false);
                VoiceToggle.SetActive(true);
                PictureToggle.SetActive(false);
                MultipleChoiceV2Panel.SetActive(false);
                MCV2Options.SetActive(false);
                for (int i = 0; i < TrueOrFalseOptions.Count; i++)
                {
                    
                    TrueOrFalseOptions[i].GetComponentInChildren<Text>().text = TrueOrFalseOptionsRandom[i];
                    TrueOrFalseOptions[i].name = TrueOrFalseOptionsRandom[i]; ;   
                    TrueOrFalseOptions[i].image.color = Color.white;     
                }
                break;
            case QuestionType.Identification:
                QuestionPanel.SetActive(true);
                MultipleChoicePanel.SetActive(false);
                TrueOrFalsePanel.SetActive(false);
                IdentificaationPanel.SetActive(true);
                VoiceToggle.SetActive(true);
                PictureToggle.SetActive(false);
                MultipleChoiceV2Panel.SetActive(false);
                MCV2Options.SetActive(false);
                break;
            case QuestionType.TanongNaMayPicture:
                QuestionPanel.SetActive(true);
                MultipleChoicePanel.SetActive(true);
                TrueOrFalsePanel.SetActive(false);
                IdentificaationPanel.SetActive(false);
                VoiceToggle.SetActive(true);
                PictureToggle.SetActive(true);
                MultipleChoiceV2Panel.SetActive(false);
                MCV2Options.SetActive(false);

                for (int i = 0; i < MultipleChoiceOptions.Count; i++)
                {
                    
                    MultipleChoiceOptions[i].GetComponentInChildren<Text>().text = MultipleChoiceOptionsRandom[i];
                    MultipleChoiceOptions[i].name = MultipleChoiceOptionsRandom[i];    
                    MultipleChoiceOptions[i].image.color = Color.white;    
                }
                break;
            case QuestionType.MultipleChoiceV2:
                QuestionPanel.SetActive(true);
                MultipleChoicePanel.SetActive(false);
                TrueOrFalsePanel.SetActive(false);
                IdentificaationPanel.SetActive(false);
                VoiceToggle.SetActive(true);
                PictureToggle.SetActive(false);
                MultipleChoiceV2Panel.SetActive(true);
                MCV2Options.SetActive(false);

                for (int i = 0; i < MultipleChoiceOptions.Count; i++)
                {

                    MultipleChoiceLAOptions[i].GetComponentInChildren<Text>().text = MultipleChoiceV2OptionsRandom[i];
                    MultipleChoiceLAOptions[i].name = MultipleChoiceV2OptionsRandom[i];
                    MultipleChoiceLAOptions[i].image.color = Color.white;
                }
                break;
        }

        questionInfoText.text = question.questionInfo;  
        questionImg.sprite = question.questionImage;
        Picture.sprite = question.Picture;

        QuestionAudioToggle();

        answered = false;

    }

    public void ReduceLife(int remainingLife)
    {
        switch(remainingLife)
        {
            case 0:
                Heart3.Play("Reduce Life 3");
                break;
            case 1:
                Heart2.Play("Reduce Life 2");
                break;
            case 2:
                Heart1.Play("Reduce Life 1");
                break;
        }
    }

    public void ChangeProgressIcon(int currentPlace, int questionNumber)
    {
        if (currentPlace == 0) 
        {
            switch (questionNumber)
            {
                case 0:
                    ProgressIcon.sprite = NounIcon[0];
                    break;
                case 1:
                    ProgressIcon.sprite = NounIcon[1];
                    break;
                case 2:
                    ProgressIcon.sprite = NounIcon[2];
                    break;
                case 3:
                    ProgressIcon.sprite = NounIcon[3];
                    break;
                case 4:
                    ProgressIcon.sprite = NounIcon[4];
                    break;
            }
        }
        else if (currentPlace == 1)
        {
            switch (questionNumber)
            {
                case 0:
                    ProgressIcon.sprite = PronounIcon[0];
                    break;
                case 1:
                    ProgressIcon.sprite = PronounIcon[1];
                    break;
                case 2:
                    ProgressIcon.sprite = PronounIcon[2];
                    break;
                case 3:
                    ProgressIcon.sprite = PronounIcon[3];
                    break;
                case 4:
                    ProgressIcon.sprite = PronounIcon[4];
                    break;
            }
        }
        else if (currentPlace == 2)
        {
            switch (questionNumber)
            {
                case 0:
                    ProgressIcon.sprite = VerbIcon[0];
                    break;
                case 1:
                    ProgressIcon.sprite = VerbIcon[1];
                    break;
                case 2:
                    ProgressIcon.sprite = VerbIcon[2];
                    break;
                case 3:
                    ProgressIcon.sprite = VerbIcon[3];
                    break;
                case 4:
                    ProgressIcon.sprite = VerbIcon[4];
                    break;
            }
        }
        else if (currentPlace == 3)
        {
            switch (questionNumber)
            {
                case 0:
                    ProgressIcon.sprite = AdjectiveIcon[0];
                    break;
                case 1:
                    ProgressIcon.sprite = AdjectiveIcon[1];
                    break;
                case 2:
                    ProgressIcon.sprite = AdjectiveIcon[2];
                    break;
                case 3:
                    ProgressIcon.sprite = AdjectiveIcon[3];
                    break;
                case 4:
                    ProgressIcon.sprite = AdjectiveIcon[4];
                    break;
            }
        }
        else if (currentPlace == 4)
        {
            switch (questionNumber)
            {
                case 0:
                    ProgressIcon.sprite = AdverbIcon[0];
                    break;
                case 1:
                    ProgressIcon.sprite = AdverbIcon[1];
                    break;
                case 2:
                    ProgressIcon.sprite = AdverbIcon[2];
                    break;
                case 3:
                    ProgressIcon.sprite = AdverbIcon[3];
                    break;
                case 4:
                    ProgressIcon.sprite = AdverbIcon[4];
                    break;
            }
        }
    }

    public void Rewards(int Place)
    {
        switch (Place)
        {
            case 0:
                RewardImg.sprite = Reward[0];
                break;
            case 1:
                RewardImg.sprite = Reward[1];
                break;
            case 2:
                RewardImg.sprite = Reward[2];
                break;
            case 3:
                RewardImg.sprite = Reward[3];
                break;
            case 4:
                RewardImg.sprite = Reward[4];
                break;
        }
    }

    void OnClick(Button btn)
    {
        questionAudio.Stop();
        if (quizManager.GameStatus == GameStatus.PLAYING)
        {
            if (question.questionType == QuestionType.MultipleChoice || question.questionType == QuestionType.TrueOrFalse || question.questionType == QuestionType.TanongNaMayPicture || question.questionType == QuestionType.MultipleChoiceV2)
            {
                if (!answered)
                {
                    answered = true;

                    bool val = quizManager.Answer(btn.name);
 
                    if (val)
                    {
                        btn.image.color = Color.green;
                        QuestionPanel.SetActive(false);
                        IdentificaationPanel.SetActive(false);
                        TrueOrFalsePanel.SetActive(false);
                        MultipleChoicePanel.SetActive(false);
                        VoiceToggle.SetActive(false);
                        PictureToggle.SetActive(false);
                        MultipleChoiceV2Panel.SetActive(false);
                        MCV2Options.SetActive(false);
                    }
                    else
                    {
                        btn.image.color = Color.red;
                        QuestionPanel.SetActive(false);
                        IdentificaationPanel.SetActive(false);
                        TrueOrFalsePanel.SetActive(false);
                        MultipleChoicePanel.SetActive(false);
                        VoiceToggle.SetActive(false);
                        PictureToggle.SetActive(false);
                        MultipleChoiceV2Panel.SetActive(false);
                        MCV2Options.SetActive(false);

                    }
                }
            }
            else
            {
                if (!answered)
                {
                   
                    answered = true;
                   
                    bool val = quizManager.Answer(IdentificationAnswer.text.ToLower());
                    


                    if (val)
                    {

                        IdentificationAnswer.text = "";
                        QuestionPanel.SetActive(false);
                        IdentificaationPanel.SetActive(false);
                        TrueOrFalsePanel.SetActive(false);
                        MultipleChoicePanel.SetActive(false);
                        VoiceToggle.SetActive(false);
                        PictureToggle.SetActive(false);
                        MultipleChoiceV2Panel.SetActive(false);
                        MCV2Options.SetActive(false);
                    }

                    else
                    {
                        IdentificationAnswer.text = "";
                        QuestionPanel.SetActive(false);
                        IdentificaationPanel.SetActive(false);
                        TrueOrFalsePanel.SetActive(false);
                        MultipleChoicePanel.SetActive(false);
                        VoiceToggle.SetActive(false);
                        PictureToggle.SetActive(false);
                        MultipleChoiceV2Panel.SetActive(false);
                        MCV2Options.SetActive(false);

                    }
                }
            }
        }
    }


    public void OnClickAnswerBtn()
    {
        string Answer = IdentificationAnswer.text;

        if (quizManager.GameStatus == GameStatus.PLAYING)
        {

            if (!answered)
            {

                answered = true;

                bool val = quizManager.Answer(Answer);


                if (val)
                {
                
                }
                else
                {
                 

                }
            }
        }
    }

    public void Levels(int Levels)
    {
        var currentLvl = GameObject.Find("Level").GetComponentInChildren<Image>();
        currentLvl.sprite = Level[Levels];
    }
    
    public void QuestionAudioToggle()
    {
        questionAudio.clip = question.QuestionVoiceOver;
        questionAudio.Play();
    }

    public void XBtn()
    {
        QPicture.SetActive(false);
    }

    public void XBtn2()
    {
        MCV2Options.SetActive(false);
    }
    public void OnClickPicture()
    {
        QPicture.SetActive(true);
    }

    void QuizBGLvl(int LevelIndex)
    {
        int PlayerLvl = LevelIndex;
        Background[0].GetComponent<Image>().sprite = QuizBG[PlayerLvl];
        Background[1].GetComponent<Image>().sprite = QuizBG[PlayerLvl];
    }

   public void DisplayWinScore()
    {
        WinScore.text = quizManager.currentScore.ToString();
    }
    IEnumerator BlinkImg(Image img)
    {
        for (int i = 0; i < 2; i++)
        {
            img.color = Color.white;
            yield return new WaitForSeconds(2);
            yield return new WaitForSeconds(2);
        }
    }

    public void OnClickMCV2Options()
    {
        MCV2Options.SetActive(true);
        AudioManagerScript.instance.ClickButtons();
    }
    
}