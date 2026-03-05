using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    // Singleton instance of AmmoManager
    // This allows other scripts to access it globally using AmmoManager.Instance
    public static AmmoManager Instance { get; set; }
    
    // Reference to a TextMeshPro UI element that will display the player's ammo
    public TextMeshProUGUI displayAmmo;

        // Awake is called before Start and is used for initialization
        // This is where the Singleton pattern is implemented
        private void Awake()
    {
        if (Instance != null && Instance != this)
        {   
            // Destroy this duplicate to ensure only one AmmoManager exists
            Destroy(gameObject);
        }
        else
        {   
            // Otherwise set this object as the global instance
            Instance = this;
        }
    }
}
