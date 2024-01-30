using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    private GameObject boton;
    private GameObject control;
    // Start is called before the first frame update
    void Start()
    {
        boton = GameObject.Find("Cubo.002");
        control = GameObject.Find("control");
    }
    private void OnMouseDown()
    {
        boton.transform.localPosition += new Vector3(0, 0, -0.05f);
        if (control.GetComponent<control>().getModoControl() == "rotacion" && GameObject.Find("control").GetComponent<control>().getEjecucion() == "no")
        {
            GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "RUN";
            control.GetComponent<control>().setModoControl("run_posicion");
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
