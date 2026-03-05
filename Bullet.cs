using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        //Detect if the bullet collides with anything
        if (collision.gameObject.CompareTag("Target"))
        {
            //Destroy if bullet hits an object
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            //Destroy if bullet hits an object
            Destroy(gameObject);
        }
    }
}
