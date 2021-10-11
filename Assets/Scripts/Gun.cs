using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Text ammoText;
    public int ammo;
    public Camera fpsCam;
    public int gunRange;
    public int hitDamage;
    public Character handHoldingThisGun;
    public ParticleSystem muzzelFlash;
    public AudioSource gunSound;
    protected GameObject target;
    protected Character targetScript;
    private int maxAmmo;
    private RaycastHit hit;
    private bool isFiring;

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
        maxAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = ammo.ToString();
        muzzelFlash.Stop();
    }
    public void Shoot()
    {
        if (ammo <= 0)
        {
            //nothing
        }
        else
        {
            isFiring = true;
            muzzelFlash.Play();
            gunSound.Play();
            Debug.Log("FIRE!");
            ammo--;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, gunRange))
            {
                if (hit.transform.gameObject.tag == "Enemy")
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
}
