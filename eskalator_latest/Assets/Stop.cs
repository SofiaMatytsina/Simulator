using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stop : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    public void Quit_Scene()
    {
        
        Debug.Log("Quit!");
        Application.Quit();
    }
}
