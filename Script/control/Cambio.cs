using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cambio : MonoBehaviour
{
    protected GameObject boton;
    protected GameObject control;
    protected GameObject brazo;
    protected GameObject robot;
    protected GameObject INV;
    private Transform pos;
    private void Start()
    {
        INV = GameObject.Find("Robott");
        boton = GameObject.Find("CuboX");
        control = GameObject.Find("control");
        robot = GameObject.Find("Robot");
        INV.GetComponent<HybridInverseKinematicsNode>().enabled = false;
        //GameObject.Find("Robott").SetActive(false);
        //robot.SetActive(false);
        //GameObject.Find("restoMano").GetComponent<LookAt>().enabled = false;

    }
    private void OnMouseDown()
    {
        if (control.GetComponent<control>().getModoMovimiento() == "articulado" && GameObject.Find("control").GetComponent<control>().getEjecucion() == "no")
        {
            control.GetComponent<control>().setModoMovimiento("coordenadas");
            GameObject.Find("Cube2").transform.SetParent(GameObject.Find("baseGiro").transform);
            GameObject.Find("Movimiento").transform.SetParent(null);
            GameObject.Find("Cube1").GetComponent<LookAt>().enabled = true;
            GameObject.Find("Opciones").GetComponent<UnityEngine.UI.Text>().text = "XYZ";
            INV.GetComponent<HybridInverseKinematicsNode>().enabled = true;
            control.GetComponent<control>().setNumero("---");

        }
        else
        {
            control.GetComponent<control>().setModoMovimiento("articulado");
            //INV.SetActive(false);
            INV.GetComponent<HybridInverseKinematicsNode>().enabled = false;
            GameObject.Find("Movimiento").transform.SetParent(GameObject.Find("Vacío.001").transform);
            GameObject.Find("Movimiento").transform.localPosition  = new Vector3(0,0, 0);
            GameObject.Find("Cube1").GetComponent<LookAt>().enabled = false;
            GameObject.Find("Cube2").transform.SetParent(GameObject.Find("baseGiro").transform);
            GameObject.Find("Opciones").GetComponent<UnityEngine.UI.Text>().text = "JOINTS";
            control.GetComponent<control>().setNumero("---");
        }
    }
}
