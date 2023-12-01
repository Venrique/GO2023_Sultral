using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public int healthRegen=10;
    // Start is called before the first frame update
    public HealthBar healthBar;

    private float timer = 0f;
    private int delayAmount = 1;
    
    public GameOverScreen GameOverScreen;
    void Awake(){
        if(maxHealth == 0){
            maxHealth =100;
            currentHealth = maxHealth;
        }else{
            currentHealth = maxHealth;
        }
        healthBar.SetMaxHealth(maxHealth);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delayAmount) {
            timer = 0;
            currentHealth+= (currentHealth + healthRegen <= maxHealth) ? healthRegen : maxHealth - currentHealth;
            healthBar.SetHealth(currentHealth);
        }   
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <=0){
            Destroy(gameObject);
            GameOver();
        }
    }

    public void GameOver(){
        GameOverScreen.Setup();
    }
}
