using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Camera.main.backgroundColor = new Color(AudioPeer._frequencyBand[1]/2,0, 0);
        
        
    }
}
