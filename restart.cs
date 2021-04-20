using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
   public void restartScene()
    {   // als je het einde hebt bereikt kan je op restart klikken en het level opnieuw spelen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
