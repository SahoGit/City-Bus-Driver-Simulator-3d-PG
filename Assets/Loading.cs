using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    private void OnEnable()
    {
        AdsManager.Instance.ShowBanner();
        AdsManager.Instance.ShowMREC();
        Invoke("AdsCall", 0.5f);
    }
    public void AdsCall()
    {
        AdsManager.Instance.ShowInterstitial("ad on loading screen");
    }
    private void OnDisable()
    {
        //AdsManager.Instance.HideBanner();
       AdsManager.Instance.HideMREC();
    }
}
