using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public Image healthBar;
    public GameObject canvas;
    public Transform camera;

    public int speed;

    private Vector3 direction;
    private float originSize;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        maxShield = shield;
        XPAbsorbed = false;
        isDead = false;
        originSize = healthBar.rectTransform.sizeDelta.x;
        direction = new Vector3(-1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true && XPAbsorbed == true)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            //Goomba AI
            transform.Translate(direction * speed * Time.deltaTime);

            healthBar.rectTransform.sizeDelta = new Vector2(originSize * health / maxHealth, healthBar.rectTransform.sizeDelta.y);
            canvas.transform.LookAt(canvas.transform.position + camera.forward);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            targetScript = target.GetComponent<Player>();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        direction *= -1;

        if (other.gameObject.tag == "Player")
        {
            targetScript.TakeDamage(this.MeleeAttackDamage);
        }
    }
}
