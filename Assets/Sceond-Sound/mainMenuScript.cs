using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
//using UnityEditor;
//using UnityEditor.Experimental.AssetImporters;
public class mainMenuScript : MonoBehaviour {
	public Text coins;
	public GameObject Loading;
	public Image fillBar;
	public GameObject settingsPanel;
	public Image soundImg;
	public Image musicImg;
	public Sprite on;
	public Sprite off;
	static string cashPref = "Cash";
	static string soundPref = "Sounds";
	static string musicPref = "Music";
	void Awake(){
		Time.timeScale=1f;
		Loading.SetActive (false);
		// Initial Deposit
		if (!PlayerPrefs.HasKey(cashPref))
        {
			PlayerPrefs.SetInt(cashPref, 250);
        }
		coins.text = PlayerPrefs.GetInt(cashPref).ToString();
	}

	public void RewardCoins(int amount)
    {
		int oldCash = PlayerPrefs.GetInt(cashPref);
		int newCash = oldCash + amount;
		PlayerPrefs.SetInt(cashPref, newCash);
		coins.text = PlayerPrefs.GetInt(cashPref).ToString();
	}
    public void Button_CoinsRewarded()
    {
       // AdsManager.Instance.ShowRewarded(() =>
       // {
		//	RewardCoins(500);
        ///   // counter++;
            //RewardCounter.text = counter.ToString();
       // }, "Counter Incremented");
    }
    void Start()
	{
        //AssignAdIds_CB.instance.ShowBanner();
       // AdsManager.Instance.ShowBanner();
       // AdsManager.Instance.ShowMREC();
        SoundManager.PlaySound(SoundManager.NameOfSounds.MainMenu);
	}
    public void play()
    {
		SoundManager.PlaySound (SoundManager.NameOfSounds.Button);
		Loading.SetActive (true);
		StartCoroutine(LoadLevel(0.1f));
    }
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("Escape button pressed and game is quit ");
			//AssignAdIds_CB.instance.CallInterstitialAd(Adspref.JustStatic);
			Application.Quit();
		}
	}
	public void ButtonClick()
    {
		SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
	}
	public void Trucks(){
		SoundManager.PlaySound (SoundManager.NameOfSounds.Button);
		Loading.SetActive (true);
	//	GameConstant.isTruckButtonClicked = true;
		SceneManager.LoadScene ("TruckSelection");
	}

	IEnumerator LoadLevel( float delay ){
		yield return new WaitForSeconds(delay);
        //AssignAdIds_CB.instance.CallInterstitialAd(Adspref.JustStatic);
       // AdsManager.Instance.ShowInterstitial("Click On play Button");
        yield return new WaitForSeconds(delay * 2);
		//GameConstant.isTruckButtonClicked = false;
		StartCoroutine(LoadSceneAsync("LevelSelection"));
	}
	public void Policy()
	{
		SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
		Application.OpenURL("https://bestone-games.webnode.com/privacy-policy/");
	}
	public void Rate()
	{
		SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.braingames.european.city.airplane.simulator");
	}
	public void OnExitConfirm()
    {
		ButtonClick();
		Application.Quit();
    }
	IEnumerator ShowAdAfterDelay(float delay)
    {
		yield return new WaitForSeconds(delay);
		//AssignAdIds_CB.instance.CallInterstitialAd(Adspref.JustStatic);

    }
	public void ShowAd()
    {
		StartCoroutine(ShowAdAfterDelay(0.25f));
	}
	IEnumerator LoadSceneAsync(string sceneName)
    {
		AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
		while (!async.isDone)
        {
			fillBar.fillAmount = async.progress;
			yield return null;
		}
	}
	public void OpenSettings()
	{
		SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
		settingsPanel.SetActive(true);
		if (PlayerPrefs.GetFloat(soundPref) == 1)
		{
			soundImg.sprite = on;
		}
		else
        {
			soundImg.sprite = off;
        }
		if (PlayerPrefs.GetFloat(musicPref) == 1)
		{
			musicImg.sprite = on;
		}
		else
		{
			musicImg.sprite = off;
		}
	}
	public void Sounds()
	{
		SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
		if (PlayerPrefs.GetFloat(soundPref) == 1)
        {
			PlayerPrefs.SetFloat(soundPref, 0);
			soundImg.sprite = off;
		}
        else
		{
			PlayerPrefs.SetFloat(soundPref, 1);
			soundImg.sprite = on;
		}
		SoundManager.Instance.SoundSettings(PlayerPrefs.GetFloat(soundPref));
	}
	public void Music()
	{
		SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
		if (PlayerPrefs.GetFloat(musicPref) == 1)
		{
			PlayerPrefs.SetFloat(musicPref, 0);
			musicImg.sprite = off;
		}
		else
		{
			PlayerPrefs.SetFloat(musicPref, 1);
			musicImg.sprite = on;
		}
		SoundManager.Instance.MusicSettings(PlayerPrefs.GetFloat(musicPref));
	}
}

