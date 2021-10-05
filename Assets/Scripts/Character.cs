using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int health;
    protected int shield;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        int remainingDamage = damage - shield;
        shield = shield - damage;
        if (shield <= 0)
        {
            shield = 0;
            health = health - remainingDamage;
        }
        if (health <= 0)
        {
            health = 0;
            isDead = true;
        }
       
    }
}
