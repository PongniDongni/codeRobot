using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioMadera : MonoBehaviour
{
    protected GameObject boton;
    protected GameObject control;


    protected Vector3 pos1;
    protected Vector3 pos2;
    protected Vector3 pos3;
    protected Vector3 pos4;
    private void Start()
    {
        GameObject.Find("ButtonCambiar").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
        control = GameObject.Find("control");
        pos1 = GameObject.Find("madera").transform.position;
        pos2 = GameObject.Find("madera2").transform.position;
    }

    private void OnButtonClick()
    {
        if (control.GetComponent<control>().getObjeto() == false)
        {
            if (control.GetComponent<control>().getMadera() == "Normal")
            {
                GameObject.Find("madera2").transform.position = pos1;
                //mad3.transform.position = pos4;
                GameObject.Find("madera").transform.position = pos2;
                control.GetComponent<control>().setMadera("otro");
                GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "Cambiada";
            }
            else
            {
                GameObject.Find("madera2").transform.position = pos2;
                GameObject.Find("madera").transform.position = pos1;
                //mad3.transform.position = pos3;
                control.GetComponent<control>().setMadera("Normal");
                GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "Cambiada";
            }
        }
        else
        {
            GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "No se puede cambiar con la madera tomada";
        }
        
    }
}
