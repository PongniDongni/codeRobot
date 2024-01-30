using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocidad : MonoBehaviour
{
    private GameObject boton;
    private void Start()
    {
        boton = GameObject.Find("Cubo.001");
    }

    private void OnMouseDown()
    {
        boton.transform.localPosition += new Vector3(0, 0, -0.05f);
        var velocidad = (GameObject.Find("control").GetComponent<control>());
        if (GameObject.Find("control").GetComponent<control>().getModoControl() == "rotacion" && GameObject.Find("control").GetComponent<control>().getEjecucion() == "no")
        {
            GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "SPEED";
            GameObject.Find("control").GetComponent<control>().setModoControl("cambiar_speed");
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
