using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardar : MonoBehaviour
{
    private GameObject boton;
    private GameObject control;

    private void Start()
    {
        boton = GameObject.Find("Cubo.027");
        control = GameObject.Find("control");
    }

    private void OnMouseDown()
    {
        boton.transform.localPosition += new Vector3(0, 0, -0.05f);
        if (control.GetComponent<control>().getModoControl() == "rotacion" && control.GetComponent<control>().getEjecucion() == "no")
        {
            GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "RECORD POSITION";
            control.GetComponent<control>().setModoControl("guardar_posiciones");
        }
        else
        {
            GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "Termine operación actual";
        }
    }

    private void OnMouseUp()
    {
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
    }
}
