using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int MeleeAttackDamage;
    public int shield;
    public int health;
    protected bool isDead;
    protected bool XPAbsorbed;
    protected GameObject target;
    protected Character targetScript;
    protected int maxHealth;
    protected int maxShield;
    protected int XP;
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
    public void Heal(int hp)
    {
        health = health + hp;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }
    public void RegenShield(int sp)
    {
        shield = shield + sp;
        if (shield >= maxShield)
        {
            shield = maxShield;
        }
    }
    public void addXP(int xp)
    {
        XP = XP + xp;
    }
    public bool getIsDead()
    {
        return isDead;
    }
    public void setIsDead(bool deathStatus)
    {
        isDead = deathStatus;
    }
    public bool getXPAbsorbed()
    {
        return XPAbsorbed;
    }
    public void setXPAbsorbed(bool xpStatus)
    {
        XPAbsorbed = xpStatus;
    }
}
