using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abort : MonoBehaviour
{
    protected GameObject boton;
    protected GameObject control;
    private void Start()
    {

        boton = GameObject.Find("Cubo.004");
        control = GameObject.Find("control");
    }
    private void OnMouseDown()
    {
        boton.transform.localPosition += new Vector3(0, 0, -0.05f);
        control.GetComponent<control>().setModoControl("rotacion");
        GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "ABORT";
        GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "Operación cancelada";
        control.GetComponent<control>().setNumero("---");//"0"
        control.GetComponent<control>().setEjecucion("no");
    }
    private void OnMouseUp()
    {
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
    }
}
