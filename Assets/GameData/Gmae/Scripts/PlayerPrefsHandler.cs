using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsHandler
{
    // Preloading Dependencies
    public static string isFirstTime = "FirstTime";
    public static string tutorial = "Tutorial";
    // Settings
    public static string controlType = "ControlType";
    public static string sound = "Sound";
    public static string music = "Music";
    public static string vibration = "Vibration";

    // Character Wizard
    public static string userName = "UserName";
    public static string skin = "Skin";
    public static string hair = "Hair";
    public static string shirtType = "ShirtType"; // Casual & Tees
    public static string c_Shirt = "CasualShirt";
    public static string t_Shirt = "TShirt";
    public static string pant = "Pant";
    public static string boot = "Boot";

    // Garage
    public static string Howo371HP = "Howo371HP";
    public static string Hand360guide = "Hand360guide";

    // Fuel -->> threshold=50;
    public static string fuel = "Fuel";

    public static string money = "Money";

    // In App Purchases
    public static string unlockLevels = "UnlockedCareerLevels";
 //   public static string unlockLevels = "UnlockLevels";
    public static string unlockTrucks = "UnlockedParkingLevels";
//public static string unlockTrucks = "UnlockTrucks";
    // Welcome Back
    public static string loggedOutTime = "LoggedOutTime";

}