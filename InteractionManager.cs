using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{   
        //allow access from anywhere (singleton)
    public static InteractionManager Instance { get; set; }
    //Attribute that stores hovered weapon
    public Weapon Weaponhovered = null;
  
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
        //Shoot raycast in the middle of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {  
            //object that is hit by the raycast
            GameObject objectHitByRaycast = hit.transform.gameObject;

            //check if this object is a weapon and it is not the active weapon
            if (objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false)
            {   
                //Store the weapon that is hit by the raycast as hovered weapon
                Weaponhovered = objectHitByRaycast.gameObject.GetComponent<Weapon>();
                //Access the outline component and enable it
                Weaponhovered.GetComponent<Outline>().enabled = true;
                //F to pick up
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //Set the weapon picked up to the weapon hovered
                    WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                if (Weaponhovered)
                {
                    //Disable outline script if it is not a weapon.
                    Weaponhovered.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}
