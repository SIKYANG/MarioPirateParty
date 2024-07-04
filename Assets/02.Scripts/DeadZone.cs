using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public AudioClip deadClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCamera>().enabled = false;
            
            AudioSource.PlayClipAtPoint(deadClip, transform.position);
            
            Destroy(collision.gameObject);
        }
       
    }

    
}
