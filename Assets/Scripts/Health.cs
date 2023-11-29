using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    [SerializeField] public GameObject flashSprite;

    void Awake(){
        if(maxHealth == 0){
            maxHealth =100;
            currentHealth = maxHealth;
        }else{
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
        StartCoroutine(flash());

        currentHealth -= damage;
        if(currentHealth <=0){
            Destroy(gameObject);
        }
    }

    IEnumerator flash()
    {
        GetComponent<SpriteMask>().sprite = GetComponent<SpriteRenderer>().sprite;
        flashSprite.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        flashSprite.SetActive(false);
    }
}
