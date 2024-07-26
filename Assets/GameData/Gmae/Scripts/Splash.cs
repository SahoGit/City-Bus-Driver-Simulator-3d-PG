using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    public GameObject PPPanel;
    public LoadingHandler loadingHandler;
    // Use this for initialization
    void Start()
    {
        loadingHandler.LoadScene(AllScenes.MainMenu);
    }
    public void ShowPolicy()
    {
        if (PlayerPrefs.GetInt("PrivacyPolicy", 0) == 1)
        {
            PPPanel.SetActive(false);
            AcceptOurPrivacyPolicy();
        }
        else
        {
            PPPanel.SetActive(true);
        }
    }
    public void AcceptOurPrivacyPolicy()
    {
        PlayerPrefs.SetInt("PrivacyPolicy", 1);
        PPPanel.SetActive(false);
        loadingHandler.LoadScene(AllScenes.MainMenu);
    }
}
