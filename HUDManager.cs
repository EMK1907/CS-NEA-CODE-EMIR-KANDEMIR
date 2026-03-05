using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{   
    //Singleton
    public static HUDManager Instance { get; set; }

    //References to ammo UI on the HUD
    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;

    //Reference to weapon image of current weapon
    [Header("Weapon")]
    public Image activeWeaponUI;

    //Transparent image
    public Sprite emptyslot;

    //Make reference to the crosshair
    public GameObject Crosshair;
        private void Awake()

    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {   
        //Get reference to the active weapon
        Weapon activeWeapon = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();

        //If there is an active weapon
        if (activeWeapon)
        {   
            //Set the UI based on active weapons magazine size and bullets left
            //Divide by bullets per burst for burst weapons
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletsPerBurst}";
            totalAmmoUI.text = $"{activeWeapon.magazineSize / activeWeapon.bulletsPerBurst}";

            //Get the weapon model
            Weapon.WeaponModel model = activeWeapon.thisWeaponModel;
            //Get the sprite of the specific weapon model
            activeWeaponUI.sprite = GetWeaponSprite(model);
        }
        else
        {
            //Leave empty if there is no active weapon
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";

            //Set active weapon slot to transparent
            activeWeaponUI.sprite = emptyslot;
        }
    }

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {   
        //Return attached prefab for every weapon
        switch (model)
        {   //Handgun
            case Weapon.WeaponModel.Handgun:
                return Resources.Load<GameObject>("Handgun_Weapon").GetComponent<SpriteRenderer>().sprite;
            //Aassault Rifle
            case Weapon.WeaponModel.AssaultRifle:
                return Resources.Load<GameObject>("AR_Weapon").GetComponent<SpriteRenderer>().sprite;
            //Burst Rifle
            case Weapon.WeaponModel.BurstRifle:
                return Resources.Load<GameObject>("BurstRifle_Weapon").GetComponent<SpriteRenderer>().sprite;
            //Shotgun
            case Weapon.WeaponModel.Shotgun:
                return Resources.Load<GameObject>("Shotgun_Weapon").GetComponent<SpriteRenderer>().sprite;
            //Sniper
            case Weapon.WeaponModel.Sniper:
                return Resources.Load<GameObject>("Sniper_Weapon").GetComponent<SpriteRenderer>().sprite;
            default:
            // If the model doesn't match any known weapon, return null (no sprite)
                return null;
        }
    }
}
