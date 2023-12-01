using System.Collections;
using UnityEngine;

public class ShotTimeTrigger : MonoBehaviour{

    public int value;
    public bool offBeat = false;
    bool beatPerformed = false;

    private float timer = 0f;
    private int delayAmount = 1;
    private int lastShot = 0;


    void Start() {
        //gameObject.GetComponent<Renderer>().material.color =  new Color(255,255,255);
        
    }

    void Update() {
        timer += Time.deltaTime;
        if (timer > delayAmount) {
            timer = 0;
            lastShot++;
        }

        if (lastShot > 15) {
            int r = Random.Range(0, 2);
            int tempValue = value;
            if (r == 0) {
                tempValue = (tempValue+1)%8;
            } else {
                tempValue = (tempValue-1)%8;
            }
            if(tempValue < 0) {
                tempValue = tempValue *-1;
            }
            value = tempValue;
        }

        //bool pause = gameObject.GetComponent<PauseGame>().isPaused;

        if ((AudioPeer._frequencyBand[value]) > AudioPeer.sensitivity && offBeat == AudioPeer._offBeat && !beatPerformed) {
            gameObject.GetComponent<PlayerShoot>().Shoot();
            lastShot = 0;
            beatPerformed = true;
        }else{
            //gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            beatPerformed = false;
        }
    }

}
