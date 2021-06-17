using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positions : MonoBehaviour
{
    public string position;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane0")
        {
            position = "НижнийЭтаж";
        }

        if (collision.gameObject.name == "Plane")
        {
            position = "ВерхнийЭтаж";
        }

        if (collision.gameObject.name == "Step")
        {
            position = "ЭскалаторВниз";
        }

        if (collision.gameObject.name == "Step 1")
        {
            position = "ЭскалаторВверх";
        }
    }
}
