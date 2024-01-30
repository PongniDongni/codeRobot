using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManoJoint : MonoBehaviour
{
    HingeJoint hinge;
    Rigidbody rb;
    protected GameObject boton;
    protected GameObject control;
    bool pres = false;
    private bool coord = false;

    void Start()
    {
        boton = GameObject.Find("Cubo.006");
        hinge = GameObject.Find("restoMano").GetComponent<HingeJoint>();
        rb = GameObject.Find("restoMano").GetComponent<Rigidbody>();
        control = GameObject.Find("control");
    }
    void Update()
    {

        float angle = GameObject.Find("restoMano").GetComponent<Transform>().localEulerAngles.y;
        if(angle > 180) 
        {
            angle = angle - 360;
        }
        if (Mathf.Round(angle) >= 90)
        {
            Debug.Log("Limite mano positivo");
            GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
            GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado de la mano";
        }
        if (Mathf.Round(angle) <= -90)
        {
            Debug.Log("Limite mano negativo");
            GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
            GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado de la mano";
        }

        
    }
    private void OnMouseDown(){
        boton.transform.localPosition += new Vector3(0,0,-0.05f);
        pres = true;
        control = GameObject.Find("control");
        GameObject.Find("pinza1").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("pinza2").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";
        if (control.GetComponent<control>().getModoControl() == "rotacion")
        {
            
            if(control.GetComponent<control>().getModoMovimiento() == "coordenadas")
            {
                coord = true;
                GameObject.Find("Cube1").GetComponent<LookAt>().enabled = false;
                
            }
            //if(control.GetComponent<control>().getModoMovimiento() == "articulado")
            {
                GameObject.Find("Cube2").transform.SetParent(GameObject.Find("Vacío").transform);
                hinge.useSpring = false;
                hinge.useMotor = true;
                JointLimits limits = hinge.limits;
                limits.min = -90;
                limits.bounciness = 0;
                limits.bounceMinVelocity = 0.2f;
                limits.max = 90;
                hinge.limits = limits;
                hinge.useLimits = true;

                var motor = hinge.motor;
                motor.force = control.GetComponent<control>().getVelocidad();
                motor.targetVelocity = control.GetComponent<control>().getVelocidad() * (-1);
                motor.freeSpin = false;
                hinge.motor = motor;
                hinge.useMotor = true;

                GameObject.Find("restoMano").GetComponent<Rigidbody>().isKinematic = false;
                GameObject.Find("Mano360").GetComponent<Rigidbody>().isKinematic = false;
                rb.constraints = RigidbodyConstraints.None;
            }
                
        }
     
            
    }
    private void OnMouseUp(){
        boton.transform.localPosition += new Vector3(0,0,0.05f);
        pres = true;
        coord = false;
        if (control.GetComponent<control>().getModoMovimiento() == "coordenadas")
        {
            //GameObject.Find("Cube1").GetComponent<LookAt>().enabled = true;
        }
        GameObject.Find("Cube2").transform.SetParent(GameObject.Find("baseGiro").transform);
        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";
        GameObject.Find("restoMano").GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find("Mano360").GetComponent<Rigidbody>().isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
    }
}
