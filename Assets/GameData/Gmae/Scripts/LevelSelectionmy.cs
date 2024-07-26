using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionmy : MonoBehaviour
{
    public static LevelSelectionmy Instance;
    public Button[] careerLevelButtons; // Array of career mode level buttons
    public Button[] parkingLevelButtons; // Array of parking mode level buttons
    public Image[] lockImagesCareerMode; // Array of lock images
    public Image[] lockImagesParkingMode; // Array of lock images
    public  int unlockedCareerLevels = 1; // Initially, only the first career mode level is unlocked
    private int unlockedParkingLevels = 1; // Initially, only the first parking mode level is unlocked
    private void Awake()
    {
        Instance = this;
        //GameConstants.slctdLvl =3;
        //actBusInd = GameConstants.slctdBus;
       // actLvlInd = GameConstants.slctdLvl;
    }
    private void Start()
    {
        // Load unlocked level data from PlayerPrefs for Career and Parking modes
      // 
     unlockedCareerLevels = PlayerPrefs.GetInt("UnlockedCareerLevels", 1);
        unlockedParkingLevels = PlayerPrefs.GetInt("UnlockedParkingLevels", 1);
         Debug.Log("Loaded UnlockedCareerLevels: " + unlockedCareerLevels);
        Debug.Log("Loaded UnlockedParkingLevels: " + unlockedParkingLevels);
        // Set button interactivity and lock images for Career mode levels based on unlocked levels
        for (int i = 0; i < careerLevelButtons.Length; i++)
        {
            careerLevelButtons[i].interactable = (i < unlockedCareerLevels);
            lockImagesCareerMode[i].gameObject.SetActive(i >= unlockedCareerLevels);
        }

        // Set button interactivity and lock images for Parking mode levels based on unlocked levels
        for (int i = 0; i < parkingLevelButtons.Length; i++)
        {
            parkingLevelButtons[i].interactable = (i < unlockedParkingLevels);
            lockImagesParkingMode[i].gameObject.SetActive(i >= unlockedParkingLevels);
            //lockImagesParkingMode[i + parkingLevelButtons.Length].gameObject.SetActive(i >= unlockedParkingLevels);
        }
    }

    public void LoadCareerLevel(int level)
    {
        if (level <= unlockedCareerLevels)
        {
            // Load the selected Career mode level scene
            SceneManager.LoadScene("CareerLevel" + level);
        }
    }

    public void LoadParkingLevel(int level)
    {
        if (level <= unlockedParkingLevels)
        {
            // Load the selected Parking mode level scene
            SceneManager.LoadScene("ParkingLevel" + level);
        }
    }

    public void CompleteLevel(int level)
    {
        if (level == unlockedCareerLevels)
        {
            // Unlock the next Career mode level
            unlockedCareerLevels++;
            PlayerPrefs.SetInt("UnlockedCareerLevels", unlockedCareerLevels);
            //PlayerPrefs.Save();

          
            //Start(); // Reload the level selection UI to reflect the changes
        }
        else if (level == unlockedParkingLevels)
        {
            // Unlock the next Parking mode level
            unlockedParkingLevels++;
            PlayerPrefs.SetInt("UnlockedParkingLevels", unlockedParkingLevels);
          //  PlayerPrefs.Save();
           // Start(); // Reload the level selection UI to reflect the changes
        }
    }
    public void CompleteLevel1(int level)
    {
        if (level == unlockedParkingLevels)
        {
            // Unlock the next Parking mode level
            unlockedParkingLevels++;
            PlayerPrefs.SetInt("UnlockedParkingLevels", unlockedParkingLevels);
            //PlayerPrefs.Save();
          //  Start(); // Reload the level selection UI to reflect the change
        }
        
    }

}