using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] MainMovements movementScript;

    void Start()
    {
        
    }



    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


    public void BtnDestroyCar()
    {
        Destroy(movementScript.gameObject);
       
    }

    public void ActiveComonentCar()
    {
        movementScript.enabled = true;

    }

    public void DeActiveComonentCar()
    {
        movementScript.enabled = false;

    }

}
