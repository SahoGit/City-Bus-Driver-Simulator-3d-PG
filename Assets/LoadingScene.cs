using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScene : MonoBehaviour
{
    public Image fillImage; // Unity Inspector mein Image drag-and-drop karein
    public Text progressText; // Unity Inspector mein Text drag-and-drop karein
    public Transform circularImage; // Unity Inspector mein Circular Image (Transform) drag-and-drop karein
    public float rotationSpeed = 100f; // Speed of circular image rotation

    private float fillTime = 3f; // Time in seconds to go from 0 to 1
    private float fillSpeed; // Speed at which the fill will change
    private float currentFill = 0f;
    public string SceneName = "Next Load Scene name";
    void Start()
    {
        fillSpeed = 1f / fillTime; // Calculate fill speed
        currentFill = 0f;
        UpdateFill();
    }

    void Update()
    {
        if (currentFill < 1f)
        {
            currentFill += fillSpeed * Time.deltaTime;
            UpdateFill();
        }
        else
        {
            // Scene loading complete


            SceneManager.LoadScene(SceneName);
        }

        // Rotate circular image based on text percentage
        float rotationAngle = currentFill * 360f;
        circularImage.rotation = Quaternion.Euler(0f, 0f, rotationAngle);


    }

    void UpdateFill()
    {
      //  fillImage.fillAmount = currentFill; // Update the fill of the image
       // int percentage = Mathf.RoundToInt(currentFill * 100); // Calculate percentage
       // progressText.text = percentage + "%"; // Update the text with the percentage



        currentFill = Mathf.Clamp01(currentFill); // Ensure currentFill is within the [0, 1] range
        fillImage.fillAmount = currentFill; // Update the fill of the image
        int percentage = Mathf.RoundToInt(currentFill * 100); // Calculate percentage
        progressText.text = percentage + "%";
    }


}
