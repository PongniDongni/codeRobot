using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borrar : MonoBehaviour
{
    protected GameObject control;
    protected GameObject boton;

    void Start()
    {
        control = GameObject.Find("control");
        boton = GameObject.Find("Cubo.029");
    }

    private void OnMouseDown()
    {
        boton.transform.localPosition += new Vector3(0, 0, -0.05f);
        if(GameObject.Find("control").GetComponent<control>().getEjecucion() == "no")
        {
            control.GetComponent<control>().Numero();
        }
        

    }

    private void OnMouseUp()
    {
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
    }
}
