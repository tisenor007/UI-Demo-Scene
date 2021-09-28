using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Image healthBar;
    public GameObject canvas;
    public Transform camera;

    public int speed;
    private Vector3 direction;

    private float health = 100;
    private float maxHealth;
    private float originSize;
    // Start is called before the first frame update
    void Start()
    {
        originSize = healthBar.rectTransform.sizeDelta.x;
        maxHealth = health;
        direction = new Vector3(-1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Goomba AI
        transform.Translate(direction * speed * Time.deltaTime);

        healthBar.rectTransform.sizeDelta = new Vector2(originSize * health / maxHealth, healthBar.rectTransform.sizeDelta.y);
        canvas.transform.LookAt(canvas.transform.position + camera.forward);

    }

    private void TakeDamage(float damage)
    {
        if (health <= 0)
        {
            health = 0;
            this.gameObject.SetActive(false);
        }
        else
        {
            health = health - damage;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        direction *= -1;

        if (other.gameObject.tag == "Player")
        {
            TakeDamage(5);
        }
    }
}
