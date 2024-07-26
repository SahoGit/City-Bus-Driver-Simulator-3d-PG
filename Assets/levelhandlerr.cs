using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelhandlerr : MonoBehaviour
{
    // Start is called before the first frame update
    int level;
    void Start()
    {
        GlobalAudioPlayer.PlayMusic("levelComplete");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
