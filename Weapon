using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    //Check if player is holding the weapon
    public bool isActiveWeapon;

    [Header("Shooting")]
    //Create new attributes linked to shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

        [Header("Burst")]
    //Create shooting attributes for weapons that use burst
    public int bulletsPerBurst = 3;
    public int BulletsLeftInBurst;

    [Header("Spread")]
    //Spread of bullets
    public float spreadIntensity;
    //Spread for hipfiring
    public float hipfireSpreadIntensity;
    //Spread for ADS
    public float adsSpreadIntensity;
    //Adding a bullet prefab

    [Header("Bullet")]
    public GameObject bulletPrefab;
    //Setting an adjustable position for where the bullet will start when released
    public Transform bulletSpawn;
    //Set a velocity for the bullet
    public float bulletVelocity = 35;
    //Setting bullet lifetime
    public float bulletPrefabLifeTime = 3f;

    //Add the muzzle effect as an object within the game
    public GameObject muzzleEffect;
    //Make reference to the animator;
    internal Animator animator;

    [Header("Loading")]
    //Add attribute for how long it to takes to reload
    public float reloadTime;
    //Add attribute for magazine size and bullets remaining in weapon
    public int magazineSize, bulletsLeft;
    //Add attribute to track whether the wqeapon is being reloaded
    public bool isReloading;

    //Create attribute to save the weapon spawn positions
    public Vector3 spawnPos;

    //Boolean for if ads is being used
    bool isADS;

    //Weapon related UI
    public TextMeshProUGUI displayAmmo;

    //Add the different weapons
    public enum WeaponModel
    {
        Handgun,
        AssaultRifle,
        Sniper,
        BurstRifle,
        Shotgun
    }

    //Weapon model attribute to identify weapon
    public WeaponModel thisWeaponModel;
    public enum ShootingMode
    {
        Single,
        Burst,
        Automatic
    }

    public ShootingMode currentShootingMode;
    private void Awake()
    {
        readyToShoot = true;
        BulletsLeftInBurst = bulletsPerBurst;
        //add the animator
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;

        //Set spread intensity to hitfire spread intensity at the start
        spreadIntensity = hipfireSpreadIntensity;
    }

    void Update()
    {   
        if(isActiveWeapon)
        {   
            //Check if right click is held down
            if (Input.GetMouseButtonDown(1))
            {   
                EnterADS();
            }
            if (Input.GetMouseButtonUp(1))
            {   
                ExitADS();
            }

            GetComponent<Outline>().enabled = false;
            //Check if magazine is empty and play empty sound.
            if (bulletsLeft == 0 && isShooting)
            {
                SoundManager.Instance.emptyMagazineSoundHandgun.Play();
            }
            
            //check if the shooting mode is auto
            if (currentShootingMode == ShootingMode.Automatic)
            {
                //Check if the player is holding the mouse button
                isShooting = Input.GetKey(KeyCode.Mouse0);            
            }
            else if(currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
            {
                //Check if player is clicking the mouse button
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            //Set conditions to allow reload and set the button to "R"
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
            {
                Reload();
            }

            if (readyToShoot && isShooting && bulletsLeft > 0)
            {
                BulletsLeftInBurst = bulletsPerBurst;
                FireWeapon();
            }

        }
    }

    private void EnterADS()
    {
        //Enable the ads
        animator.SetTrigger("enterADS");
        isADS = true;
        //Only disable crosshair if not sniper
        if (thisWeaponModel != WeaponModel.Sniper)
        {
            HUDManager.Instance.Crosshair.SetActive(false);            
        }
                
        //Set spread intensity to the ADS spread intensity
        spreadIntensity = adsSpreadIntensity;
    }

    private void ExitADS()
    {
        //Disable the ads
        animator.SetTrigger("exitADS");
        isADS = false;
        HUDManager.Instance.Crosshair.SetActive(true);

        //Reset spread intensity once left ads
        spreadIntensity = hipfireSpreadIntensity;
    }

    private void FireWeapon()
    {   
        //decrement bullets
        bulletsLeft--;

        //activate the muzzle particle effect
        muzzleEffect.GetComponent<ParticleSystem>().Play();

        //Play recoil animation depending on ADS
        if (isADS)
        {
            animator.SetTrigger("RECOIL_ADS");
        }
        else
        {
            animator.SetTrigger("RECOIL");
        }
        //Trigger the animation when firing weapon
        
        //PLay shooting sound reffering to the weapon currently used
        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        //set ready to shoot to false to prevent shooting multiple times at the same time
        readyToShoot = false;
        //Create vector to calculate shot direction and spread
        Vector3 shootingDirection = CalculateSpreadAndDirection().normalized;
        //Releasing the bullet from the set position
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        //position the bullet to the shooting direction
        bullet.transform.forward = shootingDirection;
        //Adding the force that will shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        //Destroy the bullet when lifetime is reached
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
        
        //Check if the shot is complete
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }
        
        //Check if in burst mode to see if more bullets are being shot
        if (currentShootingMode == ShootingMode.Burst && BulletsLeftInBurst > 1)
        {
            //decrement the number of bullets left in the shot
            BulletsLeftInBurst--;
            //Keep shooting
            Invoke("FireWeapon", shootingDelay);
        }
    }
    //Create the reset shot method to allow shooting again

    //Add the reload method
    private void Reload()
    {   
        //play reload sound
        SoundManager.Instance.PlayReloadSound(thisWeaponModel);
        //play reload animation
        animator.SetTrigger("RELOAD");
        //Make the reload checking boolean equal to true
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        //Reset the bullets in weapon
        bulletsLeft = magazineSize;
        //Set the reloading check boolean back to false
        isReloading = false;
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }
    public Vector3 CalculateSpreadAndDirection()
    {
        //Shoot from the center of screen to check direction of camera
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //position of collision
        Vector3 targetPoint; 
        if (Physics.Raycast(ray, out hit))
        {
            //For hitting an object
            targetPoint = hit.point;
        }
        else
        {
            //Getting direction if shooting at sky
            targetPoint = ray.GetPoint(100);
        }

        //Create the direction vector
        Vector3 direction = targetPoint - bulletSpawn.position;

        //Calculate spread in both axis
        float z = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        //Return the calculated direction and spread
        return direction + new Vector3(0, y, z);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        //destroy the bullet
        Destroy(bullet);
    }
}
