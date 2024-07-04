using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

   
    private void OnCollisionExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {


            Destroy(gameObject);
        }
    }
}
