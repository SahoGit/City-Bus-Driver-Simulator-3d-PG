using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpecs : MonoBehaviour
{
    [Range(0, 1)]
    public float handling;

    [Range(0, 1)]
    public float brakes;

    [Range(0, 1)]
    public float power;

    [Range(0, 1)]
    public float maxSpeed;
}
