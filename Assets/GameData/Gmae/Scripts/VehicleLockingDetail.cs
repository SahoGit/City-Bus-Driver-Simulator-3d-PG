using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleLockingDetail : MonoBehaviour
{
    public string vehName;
    public int vehPrice;
    public bool unlock;
    private string status;

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (!(PlayerPrefs.HasKey(vehName)))
        {
            if (unlock)
            {
                PlayerPrefs.SetString(vehName, true.ToString());
            }
            else
            {
                PlayerPrefs.SetString(vehName, false.ToString());
            }
        }
        status = PlayerPrefs.GetString(vehName);
        unlock = bool.Parse(status);
        //switch (status)
        //{
        //    case "True":
        //        unlock = true;
        //        break;
        //    case "False":
        //        unlock = false;
        //        break;
        //}
    }
}