using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madera : MonoBehaviour
{
    public bool col1 = false;
    public bool col2 = false;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisi�n detectada");
        if (collision.gameObject.tag == "Pinza1")
        {
            col1 = true;
            Debug.Log("Colisi�n objeto 1");
        }
        if (collision.gameObject.tag == "Pinza2")
        {
            col2 = true;
            // Realiza acciones o ejecuta eventos cuando los objetos colisionan
            Debug.Log("Colisi�n  Objeto2");
        }
        if(col1 == true && col2 == true)
        {
            Debug.Log("Colisi�n entre Objeto1 y Objeto2");
        }

    }

    
}
