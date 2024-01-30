using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numero7 : MonoBehaviour
{
    private GameObject boton;
    protected GameObject control;

    private void Start()
    {
        boton = GameObject.Find("Cubo.024");
        control = GameObject.Find("control");
    }

    private void OnMouseDown()
    {
        boton.transform.localPosition += new Vector3(0, 0, -0.05f);
        control.GetComponent<control>().setNumero("7");

    }

    private void OnMouseUp()
    {
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
    }
}
