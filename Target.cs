using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{   
    //Set a colour to the target
    public Color originalCol = Color.red;

    //Set a colour that target turns when hit
    public Color hitCol = Color.green;

    //Time to reset back to original colour
    public float resetTime = 2f;

    //Make reference to the renderer
    private Renderer targetRenderer;

    void Start()
    {   
        //Access the renderer of the target
        targetRenderer = GetComponent<Renderer>();
        //Set the colour of the target to the original colour
        targetRenderer.material.color = originalCol;
    }

    void OnCollisionEnter(Collision collision)
    {   
        //Ensures that the collision is with the bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {   
            //Changes colour when hit
            targetRenderer.material.color = hitCol;
            //Destroys the bullet
            Destroy(collision.gameObject);

            //Starts the time to reset the colour
            StartCoroutine(ResetColour());
        }
    }

    //Waits resetTime seconds and resets colour
    IEnumerator ResetColour()
    {
        yield return new WaitForSeconds(resetTime);
        targetRenderer.material.color = originalCol;
    }
}
