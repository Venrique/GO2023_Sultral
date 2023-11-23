using System.Collections;
using UnityEngine;

public class ShotTimeTrigger : MonoBehaviour{

    public int value;
    public bool offBeat = false;
    bool beatPerformed = false;

    void Start() {
        gameObject.GetComponent<Renderer>().material.color =  new Color(255,255,255);
    }

    void Update() {
        //a to the power of b
        //Debug.Log(Mathf.Pow(2, 3));
        print(value+": "+AudioPeer._frequencyBand[value]);
        if ((AudioPeer._frequencyBand[value]) > 0.10 && offBeat == AudioPeer._offBeat && !beatPerformed) {
            gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            beatPerformed = true;
        }else{
            gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            beatPerformed = false;
        }
    }

}
