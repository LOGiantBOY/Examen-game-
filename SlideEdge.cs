using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideEdge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {   // een border zodat je van randjes afglijdt
            collision.gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
