using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banner_Active : MonoBehaviour
{
    private void OnEnable()
    {
        AdsManager.Instance.ShowBanner();
        AdsManager.Instance.ShowMREC();
    }
    private void OnDisable()
    {
        //AdsManager.Instance.HideBanner();
        AdsManager.Instance.HideMREC();
    }
}
