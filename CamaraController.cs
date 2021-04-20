using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {   // de camera volgt de player
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
