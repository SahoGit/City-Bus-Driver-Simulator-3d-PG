using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5.0f;
    public float delayTime = 2.0f;
    public Vector3[] randomPositions;

    private Animator anim;
    private int currentWaypointIndex = 0;
    private bool isMoving = false;
    private bool isSitting = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveToWaypoint();
        }
        else if (isSitting)
        {
            StartCoroutine(SitAnimation());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ParkingPoint")
        {
            isMoving = true;
            isSitting = false;
        }
        else if (other.tag == "EndPoint")
        {
            isMoving = false;
            isSitting = false;
            currentWaypointIndex = 0;
            StartCoroutine(MoveToRandomPosition());
        }
    }

    private void MoveToWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position == targetPosition)
            {
                currentWaypointIndex++;
                isSitting = true;
            }
        }
        else
        {
            isMoving = false;
            currentWaypointIndex = 0;
        }
    }

    private IEnumerator SitAnimation()
    {
        anim.SetTrigger("sit");
        yield return new WaitForSeconds(delayTime);
        isSitting = false;
    }

    private IEnumerator MoveToRandomPosition()
    {
        yield return new WaitForSeconds(delayTime);

        Vector3 targetPosition = randomPositions[Random.Range(0, randomPositions.Length)];
        float step = speed * Time.deltaTime;

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            yield return null;
        }

        isMoving = false;
    }
}
