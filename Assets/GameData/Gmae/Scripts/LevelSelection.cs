using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [Header("LoadingHandler")]
    public LoadingHandler loadingHandler;
    [Header("Panels")]
    [SerializeField] private GameObject Modes;
    [SerializeField] private GameObject Levels;
    [SerializeField] private GameObject CareerLvlSel;
    [SerializeField] private GameObject ParkingLvlSel;
    [Header("Levels")]
    [SerializeField] private LevelsData[] CareerLvls;
    [SerializeField] private LevelsData[] ParkingLvls;
    [Header("MoneyManager")]
    public Text currency;
    int currentCash;
      private string lastSelectedModeKey = "LastSelectedMode";
    void Start()
    {

        // Load the last selected mode from PlayerPrefs
//string lastSelectedMode = PlayerPrefs.GetString(lastSelectedModeKey, "Career"); // Default to Career mode if not found
      //  OpenLevels(lastSelectedMode);
        GlobalAudioPlayer.PlayMusic("BackgroundMusicForMainMenu");
        AdsManager.Instance.ShowBanner();
        currentCash = PlayerPrefs.GetInt(PlayerPrefsHandler.money);
        currency.text = currentCash.ToString();
        
        OpenModes();
       /* int unlockUpToLevel = 5; // Change this value to the level you want to unlock up to

        for (int i = 1; i <= unlockUpToLevel; i++)
        {
            UnlockCareerLevel(i);
        }

        // Unlock Parking mode levels up to a certain point (e.g., unlock the first 5 levels)
        // Change unlockUpToLevel for Parking mode as needed
        for (int i = 1; i <= unlockUpToLevel; i++)
        {
            UnlockParkingLevel(i);
        }*/
    }
    public GameObject RewardPanel;
    public void Button_WatchReward()
    {
        AdsManager.Instance.ShowRewarded(() =>
        {
            currentCash = PlayerPrefs.GetInt("Coins");
            currentCash += 500;
            PlayerPrefs.SetInt("Coins", currentCash);

            currency.text = currentCash.ToString();
            RewardPanel.gameObject.SetActive(false);
        }, "WATCH AD GET 500 COINS");

// AdsManager.Instance.ShowRewarded(() =>
//         {
           
//         }, "");

    }
    public void PlayButtonClickSound()
    {
        GlobalAudioPlayer.PlaySFX("ButtonClick");

    }
    public void OpenModes()
    {
         // Disable mode glow images initially
      //  careerModeGlow.SetActive(false);
       // parkingModeGlow.SetActive(false);
        Modes.SetActive(true); Levels.SetActive(false); CareerLvlSel.SetActive(false); ParkingLvlSel.SetActive(false);
    }
      [Header("Mode Glow Images")]
    public GameObject careerModeGlow;
    public GameObject parkingModeGlow;

  
    public void OpenLevels(string type)
    {
        Modes.SetActive(false);
        Levels.SetActive(true);

        // Determine which mode was selected and enable its glow image
        switch (type)
        {
            case "Career":
                CareerLvlSel.SetActive(true);
                ParkingLvlSel.SetActive(false);
                // Enable Career mode glow image
                careerModeGlow.SetActive(true);
                parkingModeGlow.SetActive(false); // Disable Parking mode glow image
                break;
            case "Parking":
                CareerLvlSel.SetActive(false);
                ParkingLvlSel.SetActive(true);
                // Enable Parking mode glow image
                careerModeGlow.SetActive(false); // Disable Career mode glow image
                parkingModeGlow.SetActive(true);
                break;
        }

        // Store the last selected mode in PlayerPrefs
     //   PlayerPrefs.SetString(lastSelectedModeKey, type);
    //    PlayerPrefs.Save(); // Save changes to PlayerPrefs
        // Modes.SetActive(false);
        // Levels.SetActive(true);
        // switch (type)
        // {
        //     case "Career":
        //         CareerLvlSel.SetActive(true);
        //         ParkingLvlSel.SetActive(false);
        //         break;
        //     case "Parking":
        //         CareerLvlSel.SetActive(false);
        //         ParkingLvlSel.SetActive(true);
        //         break;
        // }
    }
    public void CareerLevels(int lvlNo)
    {
        GameConstants.slctdLvl = lvlNo;
     foreach (LevelsData levelData in CareerLvls)
    {
        levelData.LvlGlowImg.gameObject.SetActive(false);
    }
    
// if (lvlNo > 0 && lvlNo <= CareerLvls.Length)
//         {
//             LevelsData levelData = CareerLvls[lvlNo - 1];
//             // Check if the level is locked
//             if (levelData.LvlLockImg.gameObject.activeSelf)
//             {
//                 // Call the method to watch the rewarded video ad
//                 WatchRewardedAdToUnlockLevel(lvlNo);
//             }
//             else
//             {
//                 // If the level is already unlocked, just highlight it
//                 levelData.LvlGlowImg.gameObject.SetActive(true);
//             }
//         }



    //Turn on glow for the selected level
    if (lvlNo >= 0 && lvlNo < CareerLvls.Length)
    {
        LevelsData selectedLevel = CareerLvls[lvlNo];
        selectedLevel.LvlGlowImg.gameObject.SetActive(true);
        selectedCareerLevelIndex = lvlNo; // Store the selected level index

        // Reset glow for level 1 if levelIndex is 0
        if (lvlNo == 0)
        {
            LevelsData firstLevel = CareerLvls[0];
            firstLevel.LvlGlowImg.gameObject.SetActive(true);
        }
    }
// Turn off glow for all levels
    // foreach (LevelsData levelData in CareerLvls)
    // {
    //     levelData.LvlGlowImg.gameObject.SetActive(false);
    // }

    // // Turn on glow for the selected level
    // if (lvlNo >= 0 && lvlNo < CareerLvls.Length)
    // {
    //     LevelsData selectedLevel = CareerLvls[lvlNo];
    //     selectedLevel.LvlGlowImg.gameObject.SetActive(true);
    //     selectedCareerLevelIndex = lvlNo; // Store the selected level index
    // }



        

          // Turn off glow for all levels
    // foreach (LevelsData levelData in CareerLvls)
    // {
    //     levelData.LvlGlowImg.gameObject.SetActive(false);
    // }

    // // Turn on glow for the selected level
    // if (lvlNo > 0 && lvlNo <= CareerLvls.Length)
    // {
    //     LevelsData selectedLevel = CareerLvls[lvlNo - 1];
    //     selectedLevel.LvlGlowImg.gameObject.SetActive(true);
    // }

    // Proceed to loading the gameplay
    //loadingHandler.LoadScene(AllScenes.GamePlay);
       // loadingHandler.LoadScene(AllScenes.GamePlay);

    }
   public void WatchRewardedAdToUnlockLevel(int levelNumber)
    {
        // Show the rewarded video ad
       AdsManager.Instance.ShowRewarded(() =>
    {
        // This code executes after the rewarded ad is watched
        // Unlock the level
        UnlockCareerLevel(levelNumber);
    }, "Unlock Level Reward");
    }
   public void ParkignMode_WatchRewardedAdToUnlockLevel(int levelNumber)
    {
        // Show the rewarded video ad
       AdsManager.Instance.ShowRewarded(() =>
    {
        // This code executes after the rewarded ad is watched
        // Unlock the level
        UnlockParkingLevel(levelNumber);
    }, "Unlock Level Reward");
    }
    public void ParkingLevels(int lvlNo)
    {
        GameConstants.parkingLevel = lvlNo;
        //loadingHandler.LoadScene(AllScenes.ParkingMode);


     foreach (LevelsData levelData in ParkingLvls)
    {
        levelData.LvlGlowImg.gameObject.SetActive(false);
    }

    // Turn on glow for the selected level
    if (lvlNo >= 0 && lvlNo < ParkingLvls.Length)
    {
        LevelsData selectedLevel = ParkingLvls[lvlNo];
        selectedLevel.LvlGlowImg.gameObject.SetActive(true);
        selectedCareerLevelIndex = lvlNo; // Store the selected level index

        // Reset glow for level 1 if levelIndex is 0
        if (lvlNo == 0)
        {
            LevelsData firstLevel = ParkingLvls[0];
            firstLevel.LvlGlowImg.gameObject.SetActive(true);
        }
    }

    }
    private int selectedCareerLevelIndex = -1;
    public void LoadGameplay()
    {
        loadingHandler.LoadScene(AllScenes.GamePlay);
    //     loadingHandler.LoadScene(AllScenes.GamePlay);
    //     if (selectedCareerLevelIndex != -1)
    // {
    //     GameConstants.slctdLvl = selectedCareerLevelIndex + 1; // Adjust index to start from 1
    //     loadingHandler.LoadScene(AllScenes.GamePlay);
    // }
    // else
    // {
    //     Debug.Log("No level selected!"); // Handle case where no level is selected
    // }
    }
    public void LoadParkingGameplay()
    {
        loadingHandler.LoadScene(AllScenes.ParkingMode);
    }
    public void BackToMain()
    {
        loadingHandler.LoadScene(AllScenes.MainMenu);
    }

    public void OpenPanel(GameObject panelToOpen)
    {
        panelToOpen.SetActive(true);
        Debug.Log("Open Panel " + panelToOpen);
    }

    public void ClosePanel(GameObject panelToClose)
    {
        panelToClose.gameObject.SetActive(false);
        Debug.Log("Close Panel " + panelToClose);
    }
    /*public void UnlockLevel(string mode, int levelNumber)
    {
        LevelsData[] levelDataArray = mode == "Career" ? CareerLvls : ParkingLvls;
        if (levelNumber > 0 && levelNumber <= levelDataArray.Length)
        {
            LevelsData levelData = levelDataArray[levelNumber - 1];
            levelData.LvlLockImg.gameObject.SetActive(false);
        }
    }*/
    public void UnlockCareerLevel(int levelNumber)
    {
        if (levelNumber > 0 && levelNumber <= CareerLvls.Length)
        {
            LevelsData levelData = CareerLvls[levelNumber - 1];
            levelData.LvlLockImg.gameObject.SetActive(false);
            levelData.LvlBtn.interactable = true;
            
        }
    }

    public void UnlockParkingLevel(int levelNumber)
    {
        if (levelNumber > 0 && levelNumber <= ParkingLvls.Length)
        {
            LevelsData levelData = ParkingLvls[levelNumber - 1];
            levelData.LvlLockImg.gameObject.SetActive(false);
            levelData.LvlBtn.interactable = true;

        }
    }

    public string playStoreURL = "https://play.google.com/store/apps/details?id=com.pg.dinorobotwar.transformationgame"; // Replace this with your game's Play Store URL

    public void Buuton_GameLink()
    {
        Application.OpenURL(playStoreURL);
    }

    [System.Serializable]
    public class LevelsData
    {
        public Button LvlBtn;
        public Image LvlGlowImg;
        public Image LvlLockImg;
        public Image LvlClearImg;
    }
}