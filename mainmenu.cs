using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public void Playgame()
    {   // als je op de play butten drukt dan ga je naar de game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
