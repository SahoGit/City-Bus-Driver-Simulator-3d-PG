using DG.Tweening;
using SWS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Passengers
{
    public splineMove[] Passenger;
    public Animator[] passAnimator;
    public Transform[] sitPoint;
    public float CutSceneDisableCamera;
}


public class BusController : MonoBehaviour
{

    public Passengers[] passengers;
    public PathManager[] PathManagers;
    public DOTweenAnimation door;
    public DOTweenAnimation door2;
    public GameObject BusCamera;
    public Rigidbody myRig;
    public Transform ExitPoint;
    public bool HaveTwoDoors;
    public LayerMask layerMask;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndPoint"))
        {
            other.gameObject.SetActive(false);
            transform.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
            transform.rotation = other.transform.rotation;
            switch (GameConstants.slctdLvl)
            {
                case 0:
                    for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                    {
                        passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                        passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                    }
                    break;
                case 1:
                    for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                    {
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.position = ExitPoint.position;
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = ExitPoint.rotation;
                        passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                        passengers[GameConstants.slctdLvl].Passenger[i].Reverse();
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                    }
                    break;
                case 2:
                //  for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                //     {
                //         passengers[GameConstants.slctdLvl].Passenger[i].transform.position = ExitPoint.position;
                //         passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = ExitPoint.rotation;
                //         passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                //         passengers[GameConstants.slctdLvl].Passenger[i].Reverse();
                //         passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                //         passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                //     }
                    if (!GameManager.Instance.passengerDrop)
                    {
                        for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                        {
                            passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                            passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                        }
                    }
                    else
                    {
                        if (HaveTwoDoors)
                        {
                            door2.DOPlayForward();
                            door.DOPlayForward();
                        }
                        else
                        {
                            door.DOPlayForward();
                        }
                        for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                        {
                            passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.position = ExitPoint.position;
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = ExitPoint.rotation;
                            passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                            passengers[GameConstants.slctdLvl].Passenger[i].Reverse();
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                        }
                        StartCoroutine(DelayLvlComp());
                    }
                    break;
                case 3:
                    if (!GameManager.Instance.passengerDropFinish)
                    {
                        if (!GameManager.Instance.passengerDrop)
                        {
                            for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                            {
                                passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                                passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                            }
                        }
                        else if (GameManager.Instance.passengerDrop)
                        {

                            for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                            {
                                passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                                passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].Passenger[i].pathContainer.waypoints[0].transform.position;
                                passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].Passenger[i].pathContainer.waypoints[0].transform.rotation;
                                passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                                passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                        {
                            passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.position = ExitPoint.position;
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = ExitPoint.rotation;
                            passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                            passengers[GameConstants.slctdLvl].Passenger[i].Reverse();
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                        }
                        StartCoroutine(DelayLvlComp());
                    }
                    break;
                case 4:
                    if (!GameManager.Instance.passengerDropFinish)
                    {
                        if (!GameManager.Instance.passengerDrop)
                        {
                            for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                            {
                                passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                                passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                            }
                        }
                        else if (GameManager.Instance.passengerDrop)
                        {

                            for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                            {
                                passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                                passengers[GameConstants.slctdLvl].Passenger[i].transform.position = ExitPoint.position;
                                passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = ExitPoint.rotation;
                                passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                        {
                            passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.position = ExitPoint.position;
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = ExitPoint.rotation;
                            passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                            passengers[GameConstants.slctdLvl].Passenger[i].Reverse();
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                        }
                        StartCoroutine(DelayLvlComp());
                    }
                    break;

                //  case 5:
                //     for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                //     {
                //         passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                //         passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                //         passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                //         passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                //     }
                //     break;
                case 5:
                    if (!GameManager.Instance.passengerDrop)
                    {
                        for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                        {
                            passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                            passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                        }
                    }
                    else
                    {
                        if (HaveTwoDoors)
                        {
                            door2.DOPlayForward();
                            door.DOPlayForward();
                        }
                        else
                        {
                            door.DOPlayForward();
                        }
                        for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                        {
                            passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.position = ExitPoint.position;
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = ExitPoint.rotation;
                            passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                            passengers[GameConstants.slctdLvl].Passenger[i].Reverse();
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                        }
                        StartCoroutine(DelayLvlComp());
                    }
                    break;
                case 6:
                    if (!GameManager.Instance.passengerDropFinish)
                    {
                        if (!GameManager.Instance.passengerDrop)
                        {
                            for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                            {
                                passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                                passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                            }
                        }
                        else if (GameManager.Instance.passengerDrop)
                        {

                            for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                            {
                                passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                                passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].Passenger[i].pathContainer.waypoints[0].transform.position;
                                passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].Passenger[i].pathContainer.waypoints[0].transform.rotation;
                                passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                                passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                        {
                            passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.position = ExitPoint.position;
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = ExitPoint.rotation;
                            passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                            passengers[GameConstants.slctdLvl].Passenger[i].Reverse();
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                        }
                        StartCoroutine(DelayLvlComp());
                    }
                    break;
                case 7:
                    if (!GameManager.Instance.passengerDropFinish)
                    {
                        if (!GameManager.Instance.passengerDrop)
                        {
                            for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                            {
                                passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                                passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                            }
                        }
                        else if (GameManager.Instance.passengerDrop)
                        {

                            for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                            {
                                passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                                passengers[GameConstants.slctdLvl].Passenger[i].transform.position = ExitPoint.position;
                                passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = ExitPoint.rotation;
                                passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                        {
                            passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.position = ExitPoint.position;
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = ExitPoint.rotation;
                            passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                            passengers[GameConstants.slctdLvl].Passenger[i].Reverse();
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                        }
                        StartCoroutine(DelayLvlComp());
                    }
                    break;
                case 8:
                    if (!GameManager.Instance.passengerDrop)
                    {
                        for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                        {
                            passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                            passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                        }
                    }
                    else
                    {
                        if (HaveTwoDoors)
                        {
                            door2.DOPlayForward();
                            door.DOPlayForward();
                        }
                        else
                        {
                            door.DOPlayForward();
                        }
                        for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                        {
                            passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.position = ExitPoint.position;
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = ExitPoint.rotation;
                            passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                            passengers[GameConstants.slctdLvl].Passenger[i].Reverse();
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                        }
                        StartCoroutine(DelayLvlComp());
                    }
                    break;
                case 9:
                    if (!GameManager.Instance.passengerDropFinish)
                    {
                        if (!GameManager.Instance.passengerDrop)
                        {
                            for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                            {
                                passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                                passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                            }
                        }
                        else if (GameManager.Instance.passengerDrop)
                        {

                            for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                            {
                                passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                                passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].Passenger[i].pathContainer.waypoints[0].transform.position;
                                passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].Passenger[i].pathContainer.waypoints[0].transform.rotation;
                                passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                                passengers[GameConstants.slctdLvl].Passenger[i].StartMove();
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                                passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                        {
                            passengers[GameConstants.slctdLvl].Passenger[i].gameObject.SetActive(true);
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.position = ExitPoint.position;
                            passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = ExitPoint.rotation;
                            passengers[GameConstants.slctdLvl].Passenger[i].pathContainer = PathManagers[i];
                            passengers[GameConstants.slctdLvl].Passenger[i].Reverse();
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", false);
                            passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", true);
                        }
                        StartCoroutine(DelayLvlComp());
                    }
                    break;
                default:
                    break;
            }
            if (HaveTwoDoors)
            {
                door2.DOPlayForward();
                door.DOPlayForward();
            }
            else
            {
                door.DOPlayForward();
            }
            myRig.isKinematic = true;
            //myRig.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, myRig.gameObject.transform.position.y, other.gameObject.transform.position.y);
            //myRig.gameObject.transform.rotation = Quaternion.Euler(other.gameObject.transform.rotation.x, other.gameObject.transform.rotation.y, other.gameObject.transform.rotation.y);
            //if (HaveTwoDoors)
            //{
            //    door2.DOPlay();
            //    door.DOPlay();
            //}
            //else
            //{
            //    door.DOPlay();

            //}
            GameManager.Instance.rcc_Camera.gameObject.SetActive(false);
            GameManager.Instance.rcc_Canvas.SetActive(false);
            BusCamera.SetActive(true);
            StartCoroutine(DisableCamera());
        }
    }
    IEnumerator DelayLvlComp()
    {
        yield return new WaitForSeconds(passengers[GameConstants.slctdLvl].CutSceneDisableCamera);
        GameManager.Instance.LevelComplete();
    }

    IEnumerator DisableCamera()
    {
        yield return new WaitForSeconds(passengers[GameConstants.slctdLvl].CutSceneDisableCamera);
        switch (GameConstants.slctdLvl)
        {
            case 0:
                GameManager.Instance.LevelComplete();
                break;
            case 1:
                GameManager.Instance.LevelComplete();
                break;
            case 2:
                GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                GameManager.Instance.PartIndex++;
                GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);

                myRig.isKinematic = false;
                for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                {
                    passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                    passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                }
                if (HaveTwoDoors)
                {
                    door2.DOPlayBackwards();
                    door.DOPlayBackwards();
                }
                else
                {
                    door.DOPlayBackwards();
                }
                BusCamera.SetActive(false);
                GameManager.Instance.passengerDrop = true;
                break;
            case 3:

                myRig.isKinematic = false;
                if (!GameManager.Instance.passengerDrop)
                {
                    GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                    GameManager.Instance.PartIndex++;
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);
                    for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                    {
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                    }
                    if (HaveTwoDoors)
                    {
                        door2.DOPlayBackwards();
                        door.DOPlayBackwards();
                    }
                    else
                    {
                        door.DOPlayBackwards();
                    }
                    BusCamera.SetActive(false);
                    GameManager.Instance.passengerDrop = true;
                }
                else
                {
                    GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                    GameManager.Instance.PartIndex++;
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);
                    for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                    {
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                    }
                    if (HaveTwoDoors)
                    {
                        door2.DOPlayBackwards();
                        door.DOPlayBackwards();
                    }
                    else
                    {
                        door.DOPlayBackwards();
                    }
                    GameManager.Instance.passengerDropFinish = true;
                    BusCamera.SetActive(false);
                }
                break;
            case 4:

                myRig.isKinematic = false;
                if (!GameManager.Instance.passengerDrop)
                {
                    GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                    GameManager.Instance.PartIndex++;
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);
                    //for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                    //{
                    //    passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                    //    passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                    //    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                    //    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                    //}
                    if (HaveTwoDoors)
                    {
                        door2.DOPlayBackwards();
                        door.DOPlayBackwards();
                    }
                    else
                    {
                        door.DOPlayBackwards();
                    }
                    BusCamera.SetActive(false);
                    GameManager.Instance.passengerDrop = true;
                }
                else
                {
                    GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                    GameManager.Instance.PartIndex++;
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);
                    //for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                    //{
                    //    passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                    //    passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                    //    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                    //    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                    //}
                    if (HaveTwoDoors)
                    {
                        door2.DOPlayBackwards();
                        door.DOPlayBackwards();
                    }
                    else
                    {
                        door.DOPlayBackwards();
                    }
                    GameManager.Instance.passengerDropFinish = true;
                    BusCamera.SetActive(false);
                }
                break;
            case 5:
                GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                GameManager.Instance.PartIndex++;
                GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);

                myRig.isKinematic = false;
                for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                {
                    passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                    passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                }
                if (HaveTwoDoors)
                {
                    door2.DOPlayBackwards();
                    door.DOPlayBackwards();
                }
                else
                {
                    door.DOPlayBackwards();
                }
                BusCamera.SetActive(false);
                GameManager.Instance.passengerDrop = true;
                break;
            case 6:

                myRig.isKinematic = false;
                if (!GameManager.Instance.passengerDrop)
                {
                    GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                    GameManager.Instance.PartIndex++;
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);
                    for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                    {
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                    }
                    if (HaveTwoDoors)
                    {
                        door2.DOPlayBackwards();
                        door.DOPlayBackwards();
                    }
                    else
                    {
                        door.DOPlayBackwards();
                    }
                    BusCamera.SetActive(false);
                    GameManager.Instance.passengerDrop = true;
                }
                else
                {
                    GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                    GameManager.Instance.PartIndex++;
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);
                    for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                    {
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                    }
                    if (HaveTwoDoors)
                    {
                        door2.DOPlayBackwards();
                        door.DOPlayBackwards();
                    }
                    else
                    {
                        door.DOPlayBackwards();
                    }
                    GameManager.Instance.passengerDropFinish = true;
                    BusCamera.SetActive(false);
                }
                break;
            case 7:

                myRig.isKinematic = false;
                if (!GameManager.Instance.passengerDrop)
                {
                    GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                    GameManager.Instance.PartIndex++;
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);
                    //for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                    //{
                    //    passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                    //    passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                    //    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                    //    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                    //}
                    if (HaveTwoDoors)
                    {
                        door2.DOPlayBackwards();
                        door.DOPlayBackwards();
                    }
                    else
                    {
                        door.DOPlayBackwards();
                    }
                    BusCamera.SetActive(false);
                    GameManager.Instance.passengerDrop = true;
                }
                else
                {
                    GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                    GameManager.Instance.PartIndex++;
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);
                    //for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                    //{
                    //    passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                    //    passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                    //    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                    //    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                    //}
                    if (HaveTwoDoors)
                    {
                        door2.DOPlayBackwards();
                        door.DOPlayBackwards();
                    }
                    else
                    {
                        door.DOPlayBackwards();
                    }
                    GameManager.Instance.passengerDropFinish = true;
                    BusCamera.SetActive(false);
                }
                break;
            case 8:
                GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                GameManager.Instance.PartIndex++;
                GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);

                myRig.isKinematic = false;
                for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                {
                    passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                    passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                    passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                }
                if (HaveTwoDoors)
                {
                    door2.DOPlayBackwards();
                    door.DOPlayBackwards();
                }
                else
                {
                    door.DOPlayBackwards();
                }
                BusCamera.SetActive(false);
                GameManager.Instance.passengerDrop = true;
                break;
            case 9:

                myRig.isKinematic = false;
                if (!GameManager.Instance.passengerDrop)
                { 
                    GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                    GameManager.Instance.PartIndex++;
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);
                    for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                    {
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                    }
                    if (HaveTwoDoors)
                    {
                        door2.DOPlayBackwards();
                        door.DOPlayBackwards();
                    }
                    else
                    {
                        door.DOPlayBackwards();
                    }
                    BusCamera.SetActive(false);
                    GameManager.Instance.passengerDrop = true;
                }
                else
                {
                    GameManager.Instance.rcc_Camera.gameObject.SetActive(true);
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(false);
                    GameManager.Instance.PartIndex++;
                    GameManager.Instance._levelSubParts[GameConstants.slctdLvl].partNum[GameManager.Instance.PartIndex].SetActive(true);
                    for (int i = 0; i < passengers[GameConstants.slctdLvl].Passenger.Length; i++)
                    {
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.position = passengers[GameConstants.slctdLvl].sitPoint[i].position;
                        passengers[GameConstants.slctdLvl].Passenger[i].transform.rotation = passengers[GameConstants.slctdLvl].sitPoint[i].rotation;
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("Walk", false);
                        passengers[GameConstants.slctdLvl].passAnimator[i].SetBool("sit", true);
                    }
                    if (HaveTwoDoors)
                    {
                        door2.DOPlayBackwards();
                        door.DOPlayBackwards();
                    }
                    else
                    {
                        door.DOPlayBackwards();
                    }
                    GameManager.Instance.passengerDropFinish = true;
                    BusCamera.SetActive(false);
                }
                break;
                // case 5:
                // GameManager.Instance.LevelComplete();
                // break;
            default:
                break;
        }

    }
}
