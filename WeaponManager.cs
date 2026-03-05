using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; set; }

    //Create Weapon slot
    public List<GameObject> weaponSlot;

    //Variable to identify the weapon slot
    public GameObject activeWeaponSlot;
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

    private void Start()
    {   
        //Set the only weapon slot as the active one when first run
        activeWeaponSlot = weaponSlot[0];
    }

    public void PickupWeapon(GameObject weaponPickedUp)
    {
        //Add picked up weapon to the weapon slot
        AddWeaponIntoActiveSlot(weaponPickedUp);
    }

    private void AddWeaponIntoActiveSlot(GameObject weaponPickedUp)
    {
        //Allow player to drop weapon
        DropCurrentWeapon(weaponPickedUp);

        var pickOutline = weaponPickedUp.GetComponent<Outline>();
        if (pickOutline != null) pickOutline.enabled = false;

        //Set the weapon as the child of active weapon slot
        weaponPickedUp.transform.SetParent(activeWeaponSlot.transform, false);

        //Access weapon script of the weapon
        Weapon weapon = weaponPickedUp.GetComponent<Weapon>();

        //Set the position to allign with player camera
        weaponPickedUp.transform.localPosition = new Vector3(weapon.spawnPos.x, weapon.spawnPos.y, weapon.spawnPos.z);

        //Set rotations back to normal
        weaponPickedUp.transform.localRotation = Quaternion.Euler(0, 0, 0);

        //Make the weapon active and enable animator
        weapon.isActiveWeapon = true;
        weapon.animator.enabled = true;
    }

    private void DropCurrentWeapon(GameObject weaponPickedUp)
    {
        //Check if active weapon has a weapon inside
        if (activeWeaponSlot.transform.childCount > 0)
        {
            //drop weapon inside the slot
            var weapondropping = activeWeaponSlot.transform.GetChild(0).gameObject;

            var dropOutline = weapondropping.GetComponent<Outline>();
            if (dropOutline != null) dropOutline.enabled = false;
            //disable from being active
            weapondropping.GetComponent<Weapon>().isActiveWeapon = false;
            weapondropping.GetComponent<Weapon>().animator.enabled = false;
            //set the weapon parent as the same parent as weapon picking up            
            weapondropping.transform.SetParent(weaponPickedUp.transform.parent);
            //Set rotation and position
            weapondropping.transform.localPosition = weaponPickedUp.transform.localPosition;
            weapondropping.transform.localRotation = weaponPickedUp.transform.localRotation;
        }
    }
}
