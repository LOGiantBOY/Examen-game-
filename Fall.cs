using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {   // de scene word gerestart als je van de map valt
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
