//using Sirenix.OdinInspector;
using UnityEngine;

public class GarageCameraController : MonoBehaviour
{
    public Transform target;
    public float speed = 10;
    public float distance = 7;
    public float smoothSpeed = 10;
    public float zoomDistance = 5;
    public float zoomSpeed = 10;
    public Vector2 ClampYAxis = new Vector2(-15, 60);
    public Vector3 LookAtOffset = new Vector3(0, 0.75f, 0f);
    //public Garage garage;
    float initDist;
    float x, y;
    Quaternion initRot;
    Vector3 finalLookAt;


    private float angle = 0.0f;
    private bool isOrbiting = true;

    [SerializeField] private TouchField touhes;
    private void Start()
    {
        SetInitialValues();
    }

    void LateUpdate()
    {
        //if (MainMenuHandler.Instance.SelectionIsClosed) return;

        if (touhes.Pressed)
        {
            isOrbiting = false;
            x += Input.GetAxis("Mouse X") * speed / 100;
            y -= Input.GetAxis("Mouse Y") * speed / 100;
            y = Mathf.Clamp(y, ClampYAxis.x, ClampYAxis.y);

            finalLookAt = Vector3.Lerp(finalLookAt, target.position + LookAtOffset, Time.deltaTime * zoomSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(y, x, 0), smoothSpeed * Time.deltaTime);
            transform.position = transform.rotation * new Vector3(0, 0, -distance) + finalLookAt;

            distance = Mathf.Lerp(distance, zoomDistance, Time.deltaTime * 3);
            //if (!PlayerPrefs.HasKey(PlayerPrefsHandler.Hand360guide))
            //{
            //    PlayerPrefs.SetInt(PlayerPrefsHandler.Hand360guide, 100);
            //    garage.GuideAnimObject.SetActive(false);
            //}
            //MainMenuHandler.Instance.SetGarageUIAlpha(false, 0, zoomSpeed * 2);
        }
        else
        {
            isOrbiting = true;
            x = initRot.eulerAngles.y;
            y = initRot.eulerAngles.x;

            finalLookAt = Vector3.Lerp(finalLookAt, target.position, Time.deltaTime * 3);
            transform.rotation = Quaternion.Slerp(transform.rotation, initRot, smoothSpeed * Time.deltaTime);
            transform.position = transform.rotation * new Vector3(0, 0, -distance) + finalLookAt;

            distance = Mathf.Lerp(distance, initDist, Time.deltaTime * zoomSpeed);
            //MainMenuHandler.Instance.SetGarageUIAlpha(true, 1, zoomSpeed * 2);
        }
        if (isOrbiting)
        {
            angle += speed * Time.deltaTime;
            Vector3 position = target.position - Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;
            position.y += LookAtOffset.y;

            // Rotate the camera around the target
            transform.RotateAround(target.position, Vector3.up, angle);

            // Set the camera's position and make it look at the target
            transform.position = position;
            transform.LookAt(target.position);
        }
        transform.LookAt(finalLookAt);
    }
    public void SetInitialValues()
    {
        x = initRot.eulerAngles.y;
        y = initRot.eulerAngles.x;

        initDist = distance;
        initRot = transform.rotation;
        finalLookAt = target.position;
    }
}