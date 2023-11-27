using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    // Start is called before the first frame update

    
    void Awake(){
        if(maxHealth == 0){
            maxHealth =100;
            currentHealth = maxHealth;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        if(currentHealth <=0){
            Destroy(this);
        }
    }
}
