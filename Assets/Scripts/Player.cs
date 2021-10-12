using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public Text healthtxt;
    public Text shieldtxt;
    public Text XPtxt;
    public GameObject gameOverCanvas;
    public Rigidbody rb;
    public Image waterSplash;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        maxShield = shield;
        XPAbsorbed = false;
        gunScript = gun.GetComponent<Gun>();
        XP = 0;
        shield = 50;
        health = 100;
        isDead = false;
        waterSplash.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        healthtxt.text = health.ToString();
        shieldtxt.text = shield.ToString();
        XPtxt.text = XP.ToString();
        if (isDead == true)
        {
            gameOverCanvas.SetActive(true);
            health = 0;
            shield = 0;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            gameOverCanvas.SetActive(false);
            if (Input.GetButtonDown("Fire1"))
            {
                gunScript.Shoot();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                gunScript.setIsFiring(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            StopCoroutine(FadeImage(false));
            waterSplash.color = new Color(1, 1, 1, 1);
        }
        if (other.gameObject.tag == "Enemy" && getIsDead() == false)
        {
            target = other.gameObject;
            targetScript = target.GetComponent<Enemy>();
        }
        if (other.gameObject.tag == "KillBox")
        {
            this.isDead = true;
        }
        if (other.gameObject.tag == "AmmoBox" && getIsDead() == false)
        {
            gunScript.AddAmmo(20);
        }
        if (other.gameObject.tag == "FirstAid" && getIsDead() == false)
        {
            Heal(25);
        }
        if (other.gameObject.tag == "ShieldRepair" && getIsDead() == false)
        {
            RegenShield(20);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            StartCoroutine(FadeImage(true));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" && gunScript.getIsFiring() == false && getIsDead() == false)
        {
            targetScript.TakeDamage(this.MeleeAttackDamage);
            if (targetScript.getIsDead() == true && targetScript.getXPAbsorbed() == false)
            {
                addXP(100);
                targetScript.setXPAbsorbed(true);
            }
        }
        
    }

    IEnumerator FadeImage(bool fadeaway)
    {
        if (fadeaway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                waterSplash.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
