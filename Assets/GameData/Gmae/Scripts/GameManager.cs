using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.ShaderData;

[System.Serializable]
public class LevelSubParts
{
    public GameObject[] partNum;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public RCC_Camera rcc_Camera;
    public GameObject rcc_Canvas;
    public GameObject[] AllBuses;
    public GameObject[] AllLevels;
    public LevelSubParts[] _levelSubParts;
    public Transform[] spawnPoint;
    public GameObject LevelCompletePanel;
    public GameObject LevelFailedPanel;
    public GameObject LevelPausePanel;
    public Text CollisionText;

    public int PartIndex = 0;
    public int actBusInd = 0;
    int actLvlInd = 0;
    public float startTime = 150.0f;
    public Text timerText;

    private float timeRemaining;
    private bool timerIsRunning;
    public bool passengerDrop;
    public bool passengerDropFinish;
    public GameObject NextBtn;
    public Text levelComplete;
    private void Awake()
    {
        Instance = this;
        //GameConstants.slctdLvl =3;
      //  GlobalAudioPlayer.PlayMusic("BackgroundMusicForGamePlay");
        actBusInd = GameConstants.slctdBus;
        actLvlInd = GameConstants.slctdLvl;
    }
    private void Start()
    {
        Invoke("BannerShow", 2f);
        AdsManager.Instance.HideMREC();
        timeRemaining = startTime;
        timerIsRunning = true;
        ActivateBus(actBusInd);
        ActivateLevel(actLvlInd);
        rcc_Canvas.gameObject.SetActive(false);
    }
     public void PlayButtonClickSound()
    {
        GlobalAudioPlayer.PlaySFX("ButtonClick");

    }

    public void BannerShow()
    {
        AdsManager.Instance.ShowBanner();
    }
    public void EngineStart_Sound()
    {
        GlobalAudioPlayer.PlaySFX("engineStart");
        Invoke("GamePlaySound",1.5f);

    }
    public void GamePlaySound(){
         GlobalAudioPlayer.PlayMusic("BackgroundMusicForGamePlay");
    }
      public void Button_HornClicked()
    {
       
       GlobalAudioPlayer.PlaySFX("horn");

    }
    private void Update()
    {
        if (timerIsRunning)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();
            if (timeRemaining <= 0)
            {
                LevelFailed();
                timerIsRunning = false;
            }
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }



    void ActivateBus(int busNo)
    {
        for (int i = 0; i < AllBuses.Length; i++)
        {
            if (i == busNo)
            {
                AllBuses[i].SetActive(true);
                AllBuses[i].transform.SetPositionAndRotation(spawnPoint[actLvlInd].position, spawnPoint[actLvlInd].rotation);
                rcc_Camera.cameraTarget.playerVehicle = AllBuses[busNo].GetComponent<RCC_CarControllerV3>();
                AllBuses[busNo].GetComponent<RCC_CarControllerV3>();
            }
            else
                AllBuses[i].SetActive(false);
        }
    }
    void ActivateLevel(int lvlNo)
    {
        for (int i = 0; i < AllLevels.Length; i++)
        {
            if (i == lvlNo)
                AllLevels[i].SetActive(true);
            else
                Destroy(AllLevels[i]);
        }
    }
    public void NextBus()
    {
        AllBuses[actBusInd].SetActive(false);
        actBusInd++;
        if (actBusInd >= AllBuses.Length)
        {
            actBusInd = 0;
        }
        AllBuses[actBusInd].SetActive(true);
        AllBuses[actBusInd].transform.position = spawnPoint[actBusInd].position;
        AllBuses[actBusInd].transform.rotation = spawnPoint[actBusInd].rotation;
    }

    public void LevelComplete()
    {
       /* Transform parentTransform = transform.Find("Bus");
        Transform childTransform = parentTransform.Find("ChildObjectName");*/
        // AllBuses[actBusInd].GetComponentInChildren<>().enable = false;

        GameObject navi= AllBuses[actBusInd].transform.Find("All Audio Sources").gameObject;
        navi.gameObject.SetActive(false);
        LevelCompletePanel.SetActive(true);
        AdsManager.Instance.ShowInterstitial("Ad show on level complete");
        Rewrad_levelcomplete();
        // if (GameConstants.slctdLvl ==4)
        // {
        //     NextBtn.SetActive(false);
        // }
        if (GameConstants.slctdLvl >= PlayerPrefs.GetInt(PlayerPrefsHandler.unlockLevels))
        {
            PlayerPrefs.SetInt(PlayerPrefsHandler.unlockLevels, PlayerPrefs.GetInt(PlayerPrefsHandler.unlockLevels) + 1);
        }
        Time.timeScale = 0;
    }
    private int currentCash;

     public void Rewrad_levelcomplete()
    {  
    currentCash = PlayerPrefs.GetInt("Money", 0);

    // Add 500 coins to the current count
    currentCash += 250;

    // Save the updated coin count
    PlayerPrefs.SetInt("Money", currentCash);
    PlayerPrefs.Save();

    // Update your UI or perform other actions related to the level completion
    levelComplete.text = currentCash.ToString();

    // Load the main menu scene
   // SceneManager.LoadScene("MainMenu");









    //     currentCash = 500;

    // // Save the updated coin count
    // PlayerPrefs.SetInt("Money", currentCash);

    // // Update your UI or perform other actions related to the level completion
    // levelComplete.text = currentCash.ToString();
    // PlayerPrefs.Save();

    //        currentCash = PlayerPrefs.GetInt("Coins", 0) + 500;

    // // Save the updated coin count
    //     PlayerPrefs.SetInt("Coins", currentCash);
    //     PlayerPrefs.Save();

    // // Update your UI or perform other actions related to the level completion
    //     levelComplete.text = currentCash.ToString();
     
            // currentCash = PlayerPrefs.GetInt("Coins");
            // currentCash += 500;
            // PlayerPrefs.SetInt("Coins", currentCash);

            // levelComplete.text = currentCash.ToString();
           
       



    }
    public GameObject Button_Reward;
    public void Button_WatchReward()
    {
        AdsManager.Instance.ShowRewarded(() =>
        {
            currentCash = PlayerPrefs.GetInt("Money");
            currentCash *= 2;
            PlayerPrefs.SetInt("Money", currentCash);

            levelComplete.text = currentCash.ToString();
            Button_Reward.gameObject.SetActive(false);
        }, "WATCH AD GET 2x COINS");



    }
    public void LevelFailed()
    {
        LevelFailedPanel.SetActive(true);
        AdsManager.Instance.ShowInterstitial("Ad show on level failed");
        AdsManager.Instance.ShowBanner();
        
        GlobalAudioPlayer.PlayMusic("levelFailed");
        Time.timeScale = 0;
    }
    public void LevelPause()
    {
       AdsManager.Instance.ShowInterstitial("Ad show on level pause");
       AdsManager.Instance.ShowBanner();
        LevelPausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        LevelPausePanel.SetActive(false);
    }
    public void NextLevel()
    {
        // Time.timeScale = 1;
        // GameConstants.slctdLvl += 1;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // LevelSelectionmy.Instance.CompleteLevel(GameConstants.slctdLvl);
      
       Time.timeScale = 1;
        int totalLevels = 9; // Total number of levels

        if (GameConstants.slctdLvl < totalLevels)
        {
            // If current level is not the last level, load the next level
            GameConstants.slctdLvl += 1;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            LevelSelectionmy.Instance.CompleteLevel(GameConstants.slctdLvl);
             //PlayerPrefs.Save();
            //  Debug.Log("Loaded UnlockedCareerLevels: " + //unlockedCareerLevels);
            //PlayerPrefs.GetInt("UnlockedCareerLevels", 0);
        }
        else
        {
            // If current level is the last level, load the mode selection scene
            SceneManager.LoadScene("LevelSelection");
        }
      
       
        //levelSelectionScript.UnlockCareerLevel(levelNumber);

    }
   // int UnlockCareerLevel;
   /* public void UnlockNextLevel(string mode, int currentLevel)
    {
      // int maxLevel = (mode == "Career") ? CareerLvls.Length : ParkingLvls.Length;

        // Check if the current level is not the last level in the mode
      //  if (currentLevel < maxLevel)
      //  {
       //     int nextLevel = currentLevel + 1;
        //    if (mode == "Career")
         //   {
         //      // UnlockCareerLevel(nextLevel);
           // }
          //  else if (mode == "Parking")
          /  {
                //UnlockParkingLevel(nextLevel);
            }
        }
    }*/
    public void HomeLevel() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
