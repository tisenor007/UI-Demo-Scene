using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public int sightRange;
    public Image healthBar;
    public GameObject canvas;
    public Transform camera;
    public bool freeForAll;
    public int speed;

    private Vector3 direction;
    private float originSize;
    private RaycastHit sightHit;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        maxShield = shield;
        XPAbsorbed = false;
        isDead = false;
        originSize = healthBar.rectTransform.sizeDelta.x;
        direction = new Vector3(-1, 0, 0);
        if (gun != null)
        {
            gunScript = gun.GetComponent<Gun>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true && XPAbsorbed == true)
        {
            this.gameObject.SetActive(false);
            health = 0;
            shield = 0;
        }
        else
        {
            shield = 0;
            //Goomba AI
            transform.Translate(direction * speed * Time.deltaTime);

            healthBar.rectTransform.sizeDelta = new Vector2(originSize * health / maxHealth, healthBar.rectTransform.sizeDelta.y);
            canvas.transform.LookAt(canvas.transform.position + camera.forward);
            if (gun != null && gunScript != null)
            {
                if (Physics.Raycast(this.transform.position, this.transform.forward, out sightHit, sightRange))
                {
                    if (freeForAll == true)
                    {
                        if (sightHit.transform.gameObject.tag == "Player" || sightHit.transform.gameObject.tag == "Enemy")//&& sightHit.transform.gameObject.tag != this.gameObject.tag)
                        {
                            gunScript.Shoot();
                        }
                    }
                    else if (freeForAll == false)
                    {
                        if (sightHit.transform.gameObject.tag == "Player" && sightHit.transform.gameObject.tag != this.gameObject.tag)
                        {
                            gunScript.Shoot();
                        }
                    }
                }
            }
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            targetScript = target.GetComponent<Player>();
        }
        if (other.gameObject.tag == "KillBox")
        {
            this.isDead = true;
        }
        if (other.gameObject.tag == "AmmoBox" && getIsDead() == false)
        {
            if (gunScript != null)
            {
                gunScript.AddAmmo(20);
            }
        }
        if (other.gameObject.tag == "FirstAid" && getIsDead() == false)
        {
            Heal(25);
        }
        if (other.gameObject.tag == "ShieldRepair" && getIsDead() == false)
        {
            RegenShield(20);
        }
        if (other.gameObject.tag == "Enemy" && freeForAll == true)
        {
            target = other.gameObject;
            targetScript = target.GetComponent<Player>();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        direction *= -1;

        if (other.gameObject.tag == "Player" && getIsDead() == false)
        {
            targetScript.TakeDamage(this.MeleeAttackDamage);
        }
        if (other.gameObject.tag == "Enemy" && freeForAll == true && getIsDead() == false)
        {
            targetScript.TakeDamage(this.MeleeAttackDamage);
        }
    }
}
