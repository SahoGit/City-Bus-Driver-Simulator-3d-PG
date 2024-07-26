using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;
    public float height = 0.5f;
    public float rotationSpeed = 5.0f;
    public Transform initialPosition;
    public float lerpSpeed = 2.0f;

    private float angle = 0.0f;
    bool canRotate = false;
    private void OnEnable()
    {
        canRotate = true;
        // Lerp the camera position to the initial position
        //StartCoroutine(LerpCameraPosition(initialPosition.position, lerpSpeed));
        //transform.LookAt(target.position);
    }

    private IEnumerator LerpCameraPosition(Vector3 targetPosition, float speed)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        canRotate = true;
    }

    private void LateUpdate()
    {
        if (canRotate)
        {
            // Calculate the camera's position
            angle += rotationSpeed * Time.deltaTime;
            Vector3 position = target.position - Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;
            position.y += height;

            // Rotate the camera around the target
            transform.RotateAround(target.position, Vector3.up, angle);

            // Set the camera's position and make it look at the target
            transform.position = position;
            transform.LookAt(target.position);
        }
        
    }
}




