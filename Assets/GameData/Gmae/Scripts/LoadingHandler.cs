using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingHandler : MonoBehaviour
{
    public GameObject LoadingPanel;
    public Image loadingImg;
    [Range(1, 15)] public float loadDuration = 5f;

    private void Start()
    {
        LoadingPanel.SetActive(false);
    }
    public void LoadScene(AllScenes sceneName)
    {
        StartCoroutine(LoadRoutine(sceneName));
    }
    IEnumerator LoadRoutine(AllScenes scene)
    {
        loadingImg.fillAmount = 0f;
        LoadingPanel.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene.ToString());
        //operation.allowSceneActivation = false;
        float loadProgress = 0f;

        bool timeElapsed = false;
        while (loadProgress < 1)
        {
            loadProgress += Time.deltaTime / loadDuration;
            loadProgress = Mathf.Clamp01(loadProgress);
            loadingImg.fillAmount = loadProgress;

            if (loadProgress > .95f)
            {
                if (!timeElapsed)
                {
                    timeElapsed = true;
                    //if (!ShaderVariant.isWarmedUp)
                    //    ShaderVariant.WarmUp();
                }
            }

            //progressText.text = ((int)(loadProgress * 100)).ToString() + "%";
            yield return null;
        }
        //AdsManager.Instance.HideBanner();
        //AdsManager.Instance.HideMREC();

        //AssignAdIds_CB.instance.HideBanner();
        //BigBannerHandler.instance.HideBigBanner();

        operation.allowSceneActivation = true;
    }
}
public enum AllScenes
{
    MainMenu, LevelSelection, GamePlay, ParkingMode
}