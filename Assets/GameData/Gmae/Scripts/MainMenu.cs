using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.DefaultInputActions;
//using ToastPlugin;

public class MainMenu : MonoBehaviour
{
    public LoadingHandler loadingHandler;
    [Header("Menus")]
    public GameObject Main;
    public GameObject Garage;
    [Header("Vehicles")]
    [SerializeField] private GameObject[] Buses;
    private static int busIndex = 0;
    [Header("Buttons")]
    [SerializeField] private Button preVehicle;
    [SerializeField] private Button nextVehicle;
    [Header("Camera")]
    public GameObject MainMenuCamera;
    public GameObject GarageCamera;
    public Transform camInitPos;

    [Header("Specification")]
    [SerializeField] private Slider handlingFill;
    [SerializeField] private Slider brakesFill;
    [SerializeField] private Slider powerFill;
    [SerializeField] private Slider maxSpeedFill;
    VehicleSpecs specs;
    Coroutine fillSpecs;
    
    [Header("Purchasing")]
    public Button selectBtn;
    public Button buyBtn;
    public Text priceText;
    bool vehLockStatus;
    string vehName;
    int vehPrice;
    [Header("MoneyManager")]
    public Text currency;
    int currentCash;
    [Header("Panels")]
    public GameObject rewardPnl;
    public GameObject SettingPanel;
    public GameObject PrivacyPolicyPanel;
    public GameObject ExitPanel;

    private void Awake()
    {
        GlobalAudioPlayer.PlayMusic("BackgroundMusicForMainMenu");
    }
    private void Start()
    {
        OpenMenu();
        
        
        AdsManager.Instance.ShowBanner();
        currentCash = PlayerPrefs.GetInt(PlayerPrefsHandler.money);
        currency.text = currentCash.ToString();


        
         // Load the saved coin count
    currentCash = PlayerPrefs.GetInt("Money", 0);

    // Update your UI or perform other actions related to the main menu
    currency.text = currentCash.ToString();

        Debug.Log("Coins loaded in main menu: " + currentCash);
       // UpdateCash();
    }
    public void UpdateCash()
    {
        currentCash = PlayerPrefs.GetInt("Coins", currentCash);
        currency.text = currentCash.ToString();

        //currentCash = PlayerPrefs.GetInt("Coins");
        //currentCash += 500;
       // PlayerPrefs.SetInt("Coins", currentCash);

       // currency.text = currentCash.ToString();
    
    }
    public void OpenMenu()
    {
        Main.SetActive(true); MainMenuCamera.SetActive(true);
        Garage.SetActive(false); GarageCamera.SetActive(false);

    }
    public void PlayButtonClickSound()
    {
        GlobalAudioPlayer.PlaySFX("ButtonClick");

    }
    int playerCoins;
    public void FreeCash_RewradVideo()
    {
        AdsManager.Instance.ShowRewarded(() =>
        {
        currentCash = PlayerPrefs.GetInt("Coins");
       // currentCash = PlayerPrefs.GetInt("Money");
        currentCash += 500;
        PlayerPrefs.SetInt("Coins", currentCash);
       // PlayerPrefs.SetInt("Money", currentCash);

        currency.text = currentCash.ToString();
        }, "WATCH AD GET 500 COINS");
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



    }
    public void OpenGarage()
    {
        Main.SetActive(false); MainMenuCamera.SetActive(false);
        Garage.SetActive(true); GarageCamera.SetActive(true); 
        SelectBus(busIndex);
    }
    IEnumerator FillSpecs(float handling, float brakes, float power, float maxSpeed, float fillDuration)
    {
        float timer = 0f;
         while (timer < fillDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / fillDuration);
            handlingFill.value = Mathf.Lerp(handlingFill.value, handling, t);
            brakesFill.value = Mathf.Lerp(brakesFill.value, brakes, t);
            powerFill.value = Mathf.Lerp(powerFill.value, power, t);
            maxSpeedFill.value = Mathf.Lerp(maxSpeedFill.value, maxSpeed, t);
            yield return null;
        }
    }
    void DisableAllBuses()
    {
        foreach (GameObject truck in Buses)
        {
            truck.SetActive(false);
        }
    }
    void ActivateMenuBus(int index)
    {
        for (int i = 0; i < Buses.Length; i++)
        {
            if (i == index)
                Buses[i].SetActive(true);
            else
                Buses[i].SetActive(false);
        }
    }
    public void NextVehicle()
    {
        busIndex++;
        if (busIndex == Buses.Length - 1)
        {
            nextVehicle.interactable = false;
            preVehicle.interactable = true;
        }
        else
        {
            nextVehicle.interactable = true;
            preVehicle.interactable = true;
        }
        ClickSound();
        SelectBus(busIndex);
    }
    public void PreVehicle()
    {
        busIndex--;
        if (busIndex < 0)
        {
            busIndex = Buses.Length - 1;
            nextVehicle.interactable = true;
            preVehicle.interactable = false;
        }
        else
        {
            nextVehicle.interactable = true;
            preVehicle.interactable = true;
        }
        ClickSound();
        SelectBus(busIndex);
    }
    public void SelectBus(int index)
    {
        if (index == 0)
        {
            nextVehicle.interactable = true;
            preVehicle.interactable = false;
        }
        for (int i = 0; i < Buses.Length; i++)
        {
            if (i == index)
            {
                Buses[i].SetActive(true);
                if (fillSpecs != null)
                    StopCoroutine(fillSpecs);
                specs = Buses[i].GetComponent<VehicleSpecs>();

                fillSpecs = StartCoroutine(FillSpecs(specs.handling, specs.brakes, specs.power, specs.maxSpeed, 4f));
                // Camera Settings
                //Buses[i].transform.position = new Vector3(Buses[i].transform.position.x, -4f, Buses[i].transform.position.z);
                //Buses[i].transform.rotation = Quaternion.identity;
                busIndex = index;
                // Purchasing
                vehLockStatus = Buses[i].GetComponent<VehicleLockingDetail>().unlock;
                vehPrice = Buses[i].GetComponent<VehicleLockingDetail>().vehPrice;
                if (vehLockStatus || vehPrice == 0)
                {
                    buyBtn.gameObject.SetActive(false);
                    selectBtn.gameObject.SetActive(true);
                    //currentCash -=vehPrice;
                }
                else
                {
                    buyBtn.gameObject.SetActive(true);
                    vehPrice = Buses[i].GetComponent<VehicleLockingDetail>().vehPrice;
                    priceText.text = vehPrice.ToString();
                    selectBtn.gameObject.SetActive(false);
                }
            }
            else
            {
                Buses[i].SetActive(false);
            }
            Buses[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            Buses[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
    public void RewardCoins(int rewardAmount)
    {
        MoneyManager.Instance.AddMoney(rewardAmount);
        currentCash = PlayerPrefs.GetInt(PlayerPrefsHandler.money);
        currency.text = currentCash.ToString();
    }

    public void OpenPanel(GameObject panelToOpen)
    {
        panelToOpen.SetActive(true);
        Debug.Log("Open Panel "   +  panelToOpen);
    }

    //Game Close Button
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Game Close"  + Application.platform);
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://sites.google.com/view/pioneergamerz/privacy-policy");
        Debug.Log("Privacy Policy Clicked");
    }

    public void RateUs()
    {
        //Application.OpenURL("market://details?id=com.pg.bus.simulator.driving.modern.city.coach.bussimulatorid" + Application.identifier);
       Application.OpenURL("https://play.google.com/store/apps/details?id=com.pg.bus.simulator.driving.modern.city.coach.bussimulatorid");
        Debug.Log("RateUs Clicked");
    }

    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Pioneer+Gamerz");
        Debug.Log("More Games Clicked");
    }

    public void BackButton(GameObject parent)
    {
        parent.gameObject.SetActive(false);
        Debug.Log("Close Panel " +  parent);
    }

    public void onClickSettings()
    {
        SettingPanel.gameObject.SetActive(true);
    }

    public void OnBusSelect()
    {
        DisableAllBuses();
        GameConstants.slctdBus = busIndex;
        loadingHandler.LoadScene(AllScenes.LevelSelection);
    }
    
    public void BuyTruck()
    {
        vehName = Buses[busIndex].GetComponent<VehicleLockingDetail>().vehName;
        vehPrice = Buses[busIndex].GetComponent<VehicleLockingDetail>().vehPrice;
        if (currentCash >= vehPrice)
        {
            PlayerPrefs.SetString(vehName, "true");
            Buses[busIndex].GetComponent<VehicleLockingDetail>().unlock = true;
            SelectBus(busIndex);
            MoneyManager.Instance.SubtractMoney(vehPrice);
           // currentCash -=vehPrice;
            currentCash = PlayerPrefs.GetInt(PlayerPrefsHandler.money);
            currency.text = currentCash.ToString();
        }
        else
        {
            rewardPnl.SetActive(true);
        }
        ClickSound();
    }
    private void OnDisable()
    {
        busIndex = 0;
    }
    public void UnlockAllBuses()
    {
        PlayerPrefs.SetInt(PlayerPrefsHandler.unlockTrucks, 1);
    }
    public void ClickSound()
    {
        //SoundBase._instance.PlayOne(SoundBase._instance.Click, 0.5f);
    }
}