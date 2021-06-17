using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryMenu : MonoBehaviour
{
    public void Start_Scene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit_Scene()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}