using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    protected GameObject control;
    protected GameObject boton;
    bool estado;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("control");
        boton = GameObject.Find("Cubo.003");
        estado = true;
    }

    private void OnMouseDown()
    {
        boton.transform.localPosition += new Vector3(0, 0, -0.05f);
        estado = !estado;
        if (estado == false)
        {
            GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "OFF";
            control.GetComponent<control>().setModoControl("------");
            GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "";
            GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = "";
            GameObject.Find("Letra").GetComponent<UnityEngine.UI.Text>().text = "CONTROL OFF";
            GameObject.Find("Opciones").GetComponent<UnityEngine.UI.Text>().text = "";
            control.GetComponent<control>().setEjecucion("no");
        }
        else
        {
            GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "ON";
            control.GetComponent<control>().setModoControl("rotacion");
            GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "ROTATION";
            GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = "";
            GameObject.Find("Letra").GetComponent<UnityEngine.UI.Text>().text = "CONTROL ON";
            if(control.GetComponent<control>().getModoMovimiento() == "articulado")
            {
                GameObject.Find("Opciones").GetComponent<UnityEngine.UI.Text>().text = "JOINTS";
            }
            else
            {
                GameObject.Find("Opciones").GetComponent<UnityEngine.UI.Text>().text = "XYZ";
            }
            
        }
        GameObject.Find("CuboX").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.001").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.002").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.004").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.005").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.006").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.007").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo8").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.009").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.010").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo11").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo12").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.013").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.030").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.015").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.016").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.017").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.018").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.019").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.020").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.021").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.022").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.023").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.024").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.025").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.026").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.027").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.028").GetComponent<BoxCollider>().enabled = estado;
        GameObject.Find("Cubo.029").GetComponent<BoxCollider>().enabled = estado;
        

    }
    private void OnMouseUp()
    {
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
    }
}
