using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    public InputField input;
    
    public void NameBegin()
    {
        New_Name.NewName = input.text;
        Debug.Log("Имя введено");
    }
}
