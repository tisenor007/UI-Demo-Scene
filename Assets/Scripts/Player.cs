using System.Collections;
using System.Collections.Generic;
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
    public int attackDamage;

    private int XP;

    private GameObject currentEnemyAttacking;
    private Enemy enemyScript;
    // Start is called before the first frame update
    void Start()
    {
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
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            gameOverCanvas.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            currentEnemyAttacking = other.gameObject;
            enemyScript = currentEnemyAttacking.GetComponent<Enemy>();
        }
        if (other.gameObject.tag == "KillBox")
        {
            this.isDead = true;
        }
        if (other.gameObject.tag == "Water")
        {
            StopCoroutine(FadeImage(false));
            waterSplash.color = new Color(1, 1, 1, 1);
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
        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage(enemyScript.attackDamage);
            if (enemyScript.isDead == true && enemyScript.XPAbsorbed == false)
            {
                XP = XP + 100;
                enemyScript.XPAbsorbed = true;
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
