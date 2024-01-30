using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
    private int con = 0;
    // Update is called once per frame
    void Update()
    {

        if (con == 0)
        {
            GameObject.Find("control").GetComponent<control>().cargarArchivo();
            GameObject.Find("control").GetComponent<control>().setNumero("0");
            GameObject.Find("control").GetComponent<control>().setChoque(false);
            GameObject.Find("control").GetComponent<control>().cargar_posiciones();
            con = 1;
            
        }

    }
}
