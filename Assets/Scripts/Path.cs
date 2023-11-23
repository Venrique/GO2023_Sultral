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
    // Start is called before the first frame update
    void Awake()
{   
   /*  finished = false;
    List<GameObject> list = new List<GameObject>();
    if(points.Length == 0){
        int randomNumber = Random.Range(0, 3);
        if(randomNumber == 0){
            list.Add(GameObject.Find("GameObject"));
            list.Add(GameObject.Find("GameObject (1)"));
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }else if(randomNumber >=1 && randomNumber <= 2){
            list.Add(GameObject.Find("GameObject (" +randomNumber + ")"));
            list.Add(GameObject.Find("GameObject (" +(randomNumber+1) + ")"));
            if(randomNumber == 2){
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            }
        }else{
            list.Add(GameObject.Find("GameObject (" +randomNumber + ")"));
            list.Add(GameObject.Find("GameObject"));
            transform.rotation = Quaternion.Euler(0f, 0f, 180f); 
            
        }
    
        points = list.ToArray();
    }

    transform.position = points[pointsIndex].transform.position; */
}
    void Start()
    {   
        transform.position = points[pointsIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(pointsIndex);
        Debug.Log(points.Length);
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
