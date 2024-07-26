using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BusParkingHandler : MonoBehaviour
{
    private RCC_CarControllerV3 rcc_controller;
    private Rigidbody rb;
    [Header("Manager")]
    public ParkingManager ParkingManager_;
    [Header("Healh")]
    [SerializeField] private bool useHealth;
    [SerializeField] private float busHealth = 100f;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Image healthBarFill;
    private float busMaxHealth = 100f; 
    private float busCurrHealth = 100f;
    private void Awake()
    {
        rcc_controller = this.GetComponent<RCC_CarControllerV3>();
        rb = this.GetComponent<Rigidbody>(); rb.isKinematic = false;
    }
    private void Start()
    {
        busHealth = busCurrHealth = busMaxHealth;
        if (useHealth)
        {
            healthBar.SetActive(true);
            healthBarFill.fillAmount = (busHealth / 100f);
        }
        else
        {
            healthBar.SetActive(false);
            healthBarFill.fillAmount = 0;
        }
    }
    void StopController()
    {
        rcc_controller.speed = 0;
        rcc_controller.brakeInput = 1;
        rcc_controller.handbrakeInput = 1;
        rb.velocity = new Vector3(0,0,0);
        rb.angularVelocity = new Vector3(0,0,0);
        rb.isKinematic = true;
        rcc_controller.KillEngine();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Hurdle"))
        {
            StopController();
            StartCoroutine(WaitAndPerform(0.25f, ()=>
            {
                ParkingManager_.OpenPanel(AllPanels.Fail);
            }));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            StopController();
            StartCoroutine(WaitAndPerform(0.25f, () =>
            {
                ParkingManager_.OpenPanel(AllPanels.Complete);
            }));
        }
    }
    IEnumerator WaitAndPerform(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        if (action != null)
            action();
    }
    void CheckPlayerHealth()
    {
        float randomNo = UnityEngine.Random.Range(10f, 20f);
        busCurrHealth -= randomNo;
        //if(busCurrHealth<=0)

    }
}
