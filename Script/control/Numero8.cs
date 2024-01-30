using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numero8 : MonoBehaviour
{
    private GameObject boton;
    protected GameObject control;

    private void Start()
    {
        boton = GameObject.Find("Cubo.023");
        control = GameObject.Find("control");
    }

    private void OnMouseDown()
    {
        boton.transform.localPosition += new Vector3(0, 0, -0.05f);
        control.GetComponent<control>().setNumero("8");

    }

    private void OnMouseUp()
    {
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
    }
}
