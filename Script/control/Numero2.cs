using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numero2 : MonoBehaviour
{
    private GameObject boton;
    protected GameObject control;

    private void Start()
    {
        boton = GameObject.Find("Cubo.019");
        control = GameObject.Find("control");
    }

    private void OnMouseDown()
    {
        boton.transform.localPosition += new Vector3(0, 0, -0.05f);
        control.GetComponent<control>().setNumero("2");

    }

    private void OnMouseUp()
    {
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
    }
}
