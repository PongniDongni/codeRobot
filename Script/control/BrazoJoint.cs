using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrazoJoint : MonoBehaviour
{
    HingeJoint hinge;
    Rigidbody rb;
    HingeJoint hingecodo;
    bool pres = false;
    private bool coordenada = false;
    protected GameObject boton;
    protected GameObject control;

    void Start()
    {
        boton = GameObject.Find("Cubo8");
        hinge = GameObject.Find("brazo").GetComponent<HingeJoint>();
        hingecodo = GameObject.Find("codo").GetComponent<HingeJoint>();
        rb = GameObject.Find("brazo").GetComponent<Rigidbody>();
        control = GameObject.Find("control");

    }
    void Update()
    {
        float angle = GameObject.Find("brazo").GetComponent<Transform>().eulerAngles.z - 180;
        //Debug.Log(angle);
        if (Mathf.Round(angle) >= 50)
        {
            Debug.Log("Limite brazo positivo");
            GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
            GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado del brazo";
        }
        if (Mathf.Round(angle) <= -130)
        {
            Debug.Log("Limite brazo negativo");
            GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
            GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado del brazo";
        }

        float angle2 = GameObject.Find("baseGiro").GetComponent<Transform>().localEulerAngles.z;
        if (angle2 > 180)
        {
            angle2 = angle2 - 360;
        }
        float angle3 = GameObject.Find("baseGiro").GetComponent<Transform>().localEulerAngles.x;
        
        if (angle3 > 180)
        {
            angle3 = angle3 - 360;
        }
        Debug.Log(angle3);
        if (Mathf.Round(angle2) >= 155)
        {
            Debug.Log("Limite base positivo");
        }
        else
        {
            if (Mathf.Round(angle3) < 0)
            {
                Debug.Log("Limite base positivo");
                GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
                GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado";
            } else
            {
                if (coordenada == true)
                {
                    if (control.GetComponent<control>().getChoque() == false)
                    {
                        GameObject.Find("Movimiento").transform.Translate(-Vector3.forward * 1f * Time.deltaTime, Space.World);
                    }

                }
            }
            
        }
        
    }
    private void OnMouseDown(){
        boton.transform.localPosition += new Vector3(0,0,-0.05f);
        
        GameObject.Find("pinza1").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("pinza2").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";
        pres = true;
        if (control.GetComponent<control>().getModoControl() == "rotacion")
        {
            
                if(control.GetComponent<control>().getModoMovimiento() == "articulado")
                {
                    JointLimits limits = hinge.limits;
                    limits.min = -120;
                    limits.bounciness = 0;
                    limits.bounceMinVelocity = 0.2f;
                    limits.max = 50;
                    hinge.limits = limits;
                    hinge.useLimits = true;

                    var motor = hinge.motor;
                    motor.force = control.GetComponent<control>().getVelocidad();
                    motor.targetVelocity = control.GetComponent<control>().getVelocidad();
                    motor.freeSpin = false;
                    hinge.motor = motor;
                    hinge.useMotor = true;

                    GameObject.Find("brazo").GetComponent<Rigidbody>().isKinematic = false;
                    GameObject.Find("codo").GetComponent<Rigidbody>().isKinematic = false;
                    GameObject.Find("restoMano").GetComponent<Rigidbody>().isKinematic = false;
                    GameObject.Find("Mano360").GetComponent<Rigidbody>().isKinematic = false;
                    hingecodo.useSpring = true;
                    hingecodo.useMotor = false;
                    rb.constraints = RigidbodyConstraints.None;
                }
                if(control.GetComponent<control>().getModoMovimiento() == "coordenadas")
                {
                    coordenada = true;
                    GameObject.Find("Cube1").GetComponent<LookAt>().enabled = true;
                }
            
        }
            
    }
    private void OnMouseUp()
    {
        pres = true;
        coordenada = false;
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";
        GameObject.Find("brazo").GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find("codo").GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find("restoMano").GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find("Mano360").GetComponent<Rigidbody>().isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        if (control.GetComponent<control>().getChoque() == true)
        {
            GameObject.Find("Movimiento").transform.Translate(Vector3.forward * 5f * Time.deltaTime, Space.World);
        }
    }
}
