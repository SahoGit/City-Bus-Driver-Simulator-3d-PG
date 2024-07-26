using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParkingManager : MonoBehaviour
{
    [Header("Loading Handler")]
    public LoadingHandler loadingHandler;
    [HideInInspector]
    [Header("Manager")]
    private ParkingLevelManager lvlManager;
    [Header("Levels")]
    public GameObject[] LevelPrefabs;
    public Transform levelParent;
    private GameObject activeLevel;
    [Header("Camera & Vehicles")]
    public RCC_Camera rcc_Camera;
    public GameObject[] AllBuses;
    public Transform spawnPoint;
    int actBusInd = 0;
    int actLvlInd = 0;
    public int dummyLevelNo;
    [Header("UI")]
    public GameObject CompletePanel;
    public GameObject FailPanel;
    public GameObject PausePanel;
    public GameObject Loading;
    private void Awake()
    {
        GlobalAudioPlayer.PlayMusic("BackgroundMusicForGamePlay");
        actBusInd = GameConstants.slctdBus;
        actBusInd = GameConstants.slctdBus;
        actLvlInd = GameConstants.parkingLevel;
    }
    public void PlayButtonClickSound()
    {
        GlobalAudioPlayer.PlaySFX("ButtonClick");

    }
    private void Start()
    {
        Invoke("BannerShow", 2f);
        AdsManager.Instance.HideMREC();
        Time.timeScale = 1;
        ActivateLevel(actLvlInd);
        Debug.Log("check level number:"+actLvlInd);
        if (lvlManager.haveCutscene)
        {
            rcc_Camera.gameObject.SetActive(false);
            lvlManager.cutScene.SetActive(true);
            StartCoroutine(WaitForAnimation(lvlManager.anim));
        }
        else
        {
            ActivateBus(actBusInd);
        }
    }
    public void BannerShow()
    {
        AdsManager.Instance.ShowBanner();
    }
    private IEnumerator WaitForAnimation(Animator anim)
    {
        while (true)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.normalizedTime >= 1f)
            {
                // Animation has completed
                // Add your desired logic here
                yield return new WaitForSeconds(1f);
                lvlManager.cutScene.SetActive(false);
                ActivateBus(actBusInd);
                yield break; // Exit the coroutine
            }

            yield return null; // Wait for the next frame
        }
    }
    void ActivateBus(int busNo)
    {
        rcc_Camera.gameObject.SetActive(true);
        for (int i = 0; i < AllBuses.Length; i++)
        {
            if (i == busNo)
            {
                AllBuses[i].SetActive(true);
                AllBuses[i].transform.SetPositionAndRotation(lvlManager.startPos.position, lvlManager.startPos.rotation);
                rcc_Camera.cameraTarget.playerVehicle = AllBuses[busNo].GetComponent<RCC_CarControllerV3>();
            }
            else
                AllBuses[i].SetActive(false);
        }
    }
    void ActivateLevel(int lvlNo)
    {
        activeLevel = Instantiate(LevelPrefabs[actLvlInd]);
        activeLevel.transform.SetParent(levelParent);
        lvlManager = activeLevel.GetComponent<ParkingLevelManager>();
    }
    public void ButtonPause()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(true);
    }
    public void OpenPanel(AllPanels Panel)
    {
        CompletePanel.SetActive(false);
        FailPanel.SetActive(false);
        PausePanel.SetActive(false);
        switch (Panel)
        {
            case AllPanels.Complete:
                CompletePanel.SetActive(true);
                AdsManager.Instance.ShowInterstitial("Ad show on level complete");
                Rewrad_levelcomplete();
                AdsManager.Instance.ShowBanner();
                 if (GameConstants.parkingLevel >= PlayerPrefs.GetInt(PlayerPrefsHandler.unlockTrucks))
               {
              PlayerPrefs.SetInt(PlayerPrefsHandler.unlockTrucks, PlayerPrefs.GetInt(PlayerPrefsHandler.unlockTrucks) + 1);
              }
                GlobalAudioPlayer.PlayMusic("levelComplete");
                break;
            case AllPanels.Fail:
                FailPanel.SetActive(true);
                AdsManager.Instance.ShowInterstitial("Ad show on level failed");
                AdsManager.Instance.ShowBanner();
                GlobalAudioPlayer.PlayMusic("levelFailed");
                break;
            case AllPanels.Pause:
               AdsManager.Instance.ShowInterstitial("Ad show on screen pause");
               AdsManager.Instance.ShowBanner();
                PausePanel.SetActive(true);
                AudioListener.pause = true;
                Time.timeScale = 0;
                break;
        }
    }
    public void Home()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        loadingHandler.LoadScene(AllScenes.MainMenu);
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

   
    levelComplete.text = currentCash.ToString();
    }
     public GameObject Button_Reward;
     public Text levelComplete;
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

    public void Next()
    {
       /* Time.timeScale = 1;
        AudioListener.pause = false;
        loadingHandler.LoadScene(AllScenes.LevelSelection);
        LevelSelectionmy.Instance.CompleteLevel1(GameConstants.parkingLevel);*/

     int totalLevels = 9; // Total number of levels

    if (GameConstants.parkingLevel < totalLevels)
    {
        // If current level is not the last level, increment the level and load next level
        GameConstants.parkingLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        LevelSelectionmy.Instance.CompleteLevel1(GameConstants.parkingLevel);
        Debug.Log("levels next calling");

    }
    else
    {
        // If current level is the last level, load the level selection scene
        SceneManager.LoadScene("LevelSelection");
        Debug.Log("check scne name calling");
    }

        // Time.timeScale = 1;
        // GameConstants.parkingLevel += 1;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // LevelSelectionmy.Instance.CompleteLevel(GameConstants.parkingLevel);
    }
    public void Replay()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        loadingHandler.LoadScene(AllScenes.ParkingMode);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        PausePanel.SetActive(false);
    }
}
public enum AllPanels
{
    Complete, Fail, Pause
}