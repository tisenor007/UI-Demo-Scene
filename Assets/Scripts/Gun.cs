using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Text ammoText;
    public int ammo;
    public GameObject head;
    public int gunRange;
    public int hitDamage;
    public int coolDown;
    public Character handHoldingThisGun;
    public ParticleSystem muzzelFlash;
    public AudioSource gunSound;
    protected GameObject target;
    protected Character targetScript;
    private int maxAmmo = 60;
    private int maxCoolDown;
    private RaycastHit hit;
    private bool isFiring;

    // Start is called before the first frame update
    void Start()
    {
        maxCoolDown = coolDown;
        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        coolDown--;
        ammoText.text = ammo.ToString();
        muzzelFlash.Stop();
        isFiring = false;
    }
    public void Shoot()
    {
        if (ammo <= 0)
        {
            Debug.Log("OUT OF AMMO!");
        }
        else
        {
            if (coolDown <= 0)
            {
                isFiring = true;
                muzzelFlash.Play();
                gunSound.Play();
                Debug.Log("FIRE!");
                RemoveAmmo(1);
                coolDown = maxCoolDown;
                if (Physics.Raycast(head.transform.position, head.transform.forward, out hit, gunRange))
                {
                    if (hit.transform.gameObject.tag == "Enemy" || hit.transform.gameObject.tag == "Player")// && hit.transform.gameObject.tag != head.tag || hit.transform.gameObject.tag == "Player" && hit.transform.gameObject.tag != head.tag)
                    {
                        target = hit.transform.gameObject;
                        targetScript = target.GetComponent<Character>();
                        if (targetScript != null && target != null)
                        {
                            targetScript.TakeDamage(hitDamage);
                            if (targetScript.getIsDead() == true && targetScript.getXPAbsorbed() == false)
                            {
                                handHoldingThisGun.addXP(100);
                                targetScript.setXPAbsorbed(true);
                            }
                            Debug.Log(target.name + " HAS BEEN HIT!");
                        }
                    }
                }
            }
        }
    }
    public bool getIsFiring()
    {
        return isFiring;
    }
    public void setIsFiring(bool firngStatus)
    {
        isFiring = firngStatus;
    }
    public void AddAmmo(int addedAmmo)
    {
        ammo = ammo + addedAmmo;
        if (ammo >= maxAmmo)
        {
            ammo = maxAmmo;
        }
    }
    public void RemoveAmmo(int removedAmmo)
    {
        ammo = ammo - removedAmmo;
        if (ammo <= 0)
        {
            ammo = 0;
        }
    }
}
