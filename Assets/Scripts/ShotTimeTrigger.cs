using System.Collections;
using UnityEngine;

public class ShotTimeTrigger : MonoBehaviour{

    public int value;
    public bool offBeat = false;
    bool beatPerformed = false;

    void Start() {
        //gameObject.GetComponent<Renderer>().material.color =  new Color(255,255,255);
        
    }

    void Update() {
        if ((AudioPeer._frequencyBand[value]) > 0.25 && offBeat == AudioPeer._offBeat && !beatPerformed) {
            gameObject.GetComponent<PlayerShoot>().Shoot();
            beatPerformed = true;
        }else{
            //gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            beatPerformed = false;
        }
    }

}
