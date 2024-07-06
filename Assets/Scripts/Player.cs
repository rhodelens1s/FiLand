using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance {get; set;}
    [Header("Player Profile")]
    public string playerName = string.Empty;
    public int currentChar = 0;
    [Header("Player Data")]
    public int playerTotalScore = 0;
    [Header("Game Data")]
    public int placeUnlocked = 1;
    public int lvlUnlocked = 1;
    [Header("Noun Valley")]
    public int Lvl1Score = 0;
    public int Lvl2Score = 0;
    public int Lvl3Score = 0;
    public int Lvl4Score = 0;
    public int Lvl5Score = 0;
    public int Lvl6Score = 0;
    public int Lvl7Score = 0;
    public int Lvl8Score = 0;
    public int Lvl9Score = 0;
    public int Lvl10Score = 0;
    public int NounValleyTotalScore = 0;
    [Header("Pronoun Island")]
    public int Lvl11Score = 0;
    public int Lvl12Score = 0;
    public int Lvl13Score = 0;
    public int Lvl14Score = 0;
    public int Lvl15Score = 0;
    public int Lvl16Score = 0;
    public int Lvl17Score = 0;
    public int Lvl18Score = 0;
    public int Lvl19Score = 0;
    public int Lvl20Score = 0;
    public int PronounIslandTotalScore = 0;
    [Header("Verb Town")]
    public int Lvl21Score = 0;
    public int Lvl22Score = 0;
    public int Lvl23Score = 0;
    public int Lvl24Score = 0;
    public int Lvl25Score = 0;
    public int Lvl26Score = 0;
    public int Lvl27Score = 0;
    public int Lvl28Score = 0;
    public int Lvl29Score = 0;
    public int Lvl30Score = 0;
    public int VerbTownTotalScore = 0;
    [Header("Adjective Palace")]
    public int Lvl31Score = 0;
    public int Lvl32Score = 0;
    public int Lvl33Score = 0;
    public int Lvl34Score = 0;
    public int Lvl35Score = 0;
    public int Lvl36Score = 0;
    public int Lvl37Score = 0;
    public int Lvl38Score = 0;
    public int Lvl39Score = 0;
    public int Lvl40Score = 0;
    public int AdjectivePalaceTotalScore = 0;
    [Header("Adverb Park")]
    public int Lvl41Score = 0;
    public int Lvl42Score = 0;
    public int Lvl43Score = 0;
    public int Lvl44Score = 0;
    public int Lvl45Score = 0;
    public int Lvl46Score = 0;
    public int Lvl47Score = 0;
    public int Lvl48Score = 0;
    public int Lvl49Score = 0;
    public int Lvl50Score = 0;
    public int AdverbParkTotalScore = 0;
    [Header("Boat Position")]
    public int CurrentPlace = 0;
    [Header("Level")]
    public int currentLvl = 1;
    public int highestLvl = 1;
    [Header("SettingsPrefs")]
    public int musicOn;
    public int soundOn;
    [Header("VideoUnlock")]
    public bool IntroUnlock = false;
    public bool NounUnlock = false;
    public bool PronounUnlock = false;
    public bool VerbUnlock = false;
    public bool AdverbUnlock = false;
    public bool AdjectiveUnlock = false;


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
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        playerName = data.playerName;
        currentChar = data.currentChar;
        playerTotalScore = data.playerTotalScore;
        placeUnlocked = data.placeUnlocked;
        lvlUnlocked = data.lvlUnlocked;
        Lvl1Score = data.Lvl1Score;
        Lvl2Score = data.Lvl2Score;
        Lvl3Score = data.Lvl3Score;
        Lvl4Score = data.Lvl4Score;
        Lvl5Score = data.Lvl5Score;
        Lvl6Score = data.Lvl6Score;
        Lvl7Score = data.Lvl7Score;
        Lvl8Score = data.Lvl8Score;
        Lvl9Score = data.Lvl9Score;
        Lvl10Score = data.Lvl10Score;
        NounValleyTotalScore = data.NounValleyTotalScore;
        Lvl11Score = data.Lvl11Score;
        Lvl12Score = data.Lvl12Score;
        Lvl13Score = data.Lvl13Score;
        Lvl14Score = data.Lvl14Score;
        Lvl15Score = data.Lvl15Score;
        Lvl16Score = data.Lvl16Score;
        Lvl17Score = data.Lvl17Score;
        Lvl18Score = data.Lvl18Score;
        Lvl19Score = data.Lvl19Score;
        Lvl20Score = data.Lvl20Score;
        PronounIslandTotalScore = data.PronounIslandTotalScore;
        Lvl21Score = data.Lvl21Score;
        Lvl22Score = data.Lvl22Score;
        Lvl23Score = data.Lvl23Score;
        Lvl24Score = data.Lvl24Score;
        Lvl25Score = data.Lvl25Score;
        Lvl26Score = data.Lvl26Score;
        Lvl27Score = data.Lvl27Score;
        Lvl28Score = data.Lvl28Score;
        Lvl29Score = data.Lvl29Score;
        Lvl30Score = data.Lvl30Score;
        VerbTownTotalScore = data.VerbTownTotalScore;
        Lvl31Score = data.Lvl31Score;
        Lvl32Score = data.Lvl32Score;
        Lvl33Score = data.Lvl33Score;
        Lvl34Score = data.Lvl34Score;
        Lvl35Score = data.Lvl35Score;
        Lvl36Score = data.Lvl36Score;
        Lvl37Score = data.Lvl37Score;
        Lvl38Score = data.Lvl38Score;
        Lvl39Score = data.Lvl39Score;
        Lvl40Score = data.Lvl40Score;
        AdjectivePalaceTotalScore = data.AdjectivePalaceTotalScore;
        Lvl41Score = data.Lvl41Score;
        Lvl42Score = data.Lvl42Score;
        Lvl43Score = data.Lvl43Score;
        Lvl44Score = data.Lvl44Score;
        Lvl45Score = data.Lvl45Score;
        Lvl46Score = data.Lvl46Score;
        Lvl47Score = data.Lvl47Score;
        Lvl48Score = data.Lvl48Score;
        Lvl49Score = data.Lvl49Score;
        Lvl50Score = data.Lvl50Score;
        AdverbParkTotalScore = data.AdverbParkTotalScore;
        CurrentPlace = data.CurrentPlace;
        currentLvl = data.currentLvl;
        highestLvl = data.highestLvl;
        musicOn = data.musicOn;
        soundOn = data.soundOn;
        IntroUnlock = data.IntroUnlock;
        NounUnlock = data.NounUnlock;
        PronounUnlock = data.PronounUnlock;
        VerbUnlock = data.VerbUnlock;
        AdverbUnlock = data.AdverbUnlock;
        AdjectiveUnlock = data.AdjectiveUnlock;
        
}
}
