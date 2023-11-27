using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Path : MonoBehaviour
{
    [SerializeField] public GameObject[] points;
    [SerializeField] public float moveSpeed;
    private int pointsIndex;
    private bool finished;

    void Awake()
    {   
       
    }

    void Start()
    {   
        if(points[0].name == "Point0" ){
            transform.rotation = Quaternion.Euler(0,0, 0f);
       }

       if(points[0].name == "Point1" ){
            transform.rotation = Quaternion.Euler(0,0, -90f);
       }

       if(points[0].name == "Point2" ){
        transform.rotation = Quaternion.Euler(0,0, -180f);
       }

       if(points[0].name == "Point3" ){
        transform.rotation = Quaternion.Euler(0,0, -270f);
       }
        transform.position = points[pointsIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(pointsIndex <= points.Length -1 && finished == false){

            transform.position = Vector2.MoveTowards(transform.position, points[pointsIndex].transform.position, moveSpeed *Time.deltaTime);
        
            if(transform.position == points[pointsIndex].transform.position){
                pointsIndex +=1;
            }
            if(pointsIndex == points.Length){
                    //pointsIndex = 0;
                    finished = true;
                    pointsIndex -= 2;
            }     
        }

        if(finished == true){

            if (pointsIndex == 0){
                finished = false;
            }

            transform.position = Vector2.MoveTowards(transform.position, points[pointsIndex].transform.position, moveSpeed *Time.deltaTime);
        
            if(transform.position == points[pointsIndex].transform.position){
                pointsIndex -=1;
            }
            if(pointsIndex == points.Length){
                    pointsIndex -= 1;
            }  
        }
    }
}
