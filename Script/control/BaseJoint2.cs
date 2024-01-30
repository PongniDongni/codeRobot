using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseJoint2 : MonoBehaviour
{
    HingeJoint hinge;
    Rigidbody rb;
    protected GameObject boton;
    protected GameObject control;
    private bool pres = false;
    private bool coordenada = false;

    void Start()
    {
        boton = GameObject.Find("Cubo.015");
        hinge = GameObject.Find("baseGiro").GetComponent<HingeJoint>();
        rb = GameObject.Find("baseGiro").GetComponent<Rigidbody>();
        control = GameObject.Find("control");

    }
    void FixedUpdate()
    {
        float angle = GameObject.Find("baseGiro").GetComponent<Transform>().localEulerAngles.z;
        if (angle > 180)
        {
            angle = angle - 360;
        }
        float angle3 = GameObject.Find("baseGiro").GetComponent<Transform>().localEulerAngles.y;

        if (angle3 > 180)
        {
            angle3 = angle3 - 360;
        }
        Debug.Log(angle);
        if (Mathf.Round(angle) >= 155)
        {
            Debug.Log("Limite base positivo");
            GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
            GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado de la base";
            
        }
        if (Mathf.Round(angle) <= -155)
        {
            Debug.Log("Limite base negativo");
            GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
            GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado de la base";
        }
        else
        {
            float anglecodo = GameObject.Find("codo").GetComponent<Transform>().localEulerAngles.y;
            if (anglecodo > 180)
            {
                anglecodo = anglecodo - 360;
            }
            if (Mathf.Round(anglecodo) < 0)
            {
                Debug.Log("Limite base positivo");
                GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
                GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado de la base";
            }
            else
            {
                if (Mathf.Round(angle3) > 0)
                {
                    Debug.Log("Limite base positivo");
                    GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
                    GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado";
                }
                else
                {
                    if (coordenada == true)
                    {
                        if (control.GetComponent<control>().getChoque() == false)
                        {
                            GameObject.Find("Movimiento").transform.Translate(Vector3.right * 1f * Time.deltaTime, Space.World);
                        }
                    }
                }
                
            }
            
        }

       
        int atras = 0;
        
        if (control.GetComponent<control>().getChoque() == true)
        {
            if(pres == true)
            {
                atras++;
                if (control.GetComponent<control>().getModoControl() == "rotacion")
                {
                    GameObject.Find("Cube2").transform.SetParent(GameObject.Find("baseGiro").transform);
                    JointLimits limits = hinge.limits;
                    limits.min = -155;
                    limits.bounciness = 0;
                    limits.bounceMinVelocity = 0;
                    limits.max = Mathf.Round(GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle) + atras;
                    hinge.limits = limits;
                    hinge.useLimits = true;

                    var motor = hinge.motor;
                    motor.force = 1;
                    motor.targetVelocity = control.GetComponent<control>().getVelocidad();
                    motor.freeSpin = false;
                    hinge.motor = motor;
                    hinge.useMotor = true;

                    GameObject.Find("baseGiro").GetComponent<Rigidbody>().isKinematic = false;
                    GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = true;
                    GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = true;
                    rb.constraints = RigidbodyConstraints.None;

                }
            }

        }
        else
        {
            pres = false;
        }
        

    }
    private void OnMouseDown(){
        boton.transform.localPosition += new Vector3(0,0,-0.05f);
        GameObject.Find("pinza1").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("pinza2").GetComponent<MeshCollider>().enabled = true;

        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";

        pres = true;
        if (control.GetComponent<control>().getChoque() == false)
        {
            if (control.GetComponent<control>().getModoControl() == "rotacion")
            {
                if(control.GetComponent<control>().getModoMovimiento() == "articulado")
                {
                    GameObject.Find("Cube2").transform.SetParent(GameObject.Find("baseGiro").transform);
                    JointLimits limits = hinge.limits;
                    limits.min = -155;
                    limits.bounciness = 0;
                    limits.bounceMinVelocity = 0.2f;
                    limits.max = 155;
                    hinge.limits = limits;
                    hinge.useLimits = true;

                    var motor = hinge.motor;
                    motor.force = control.GetComponent<control>().getVelocidad();
                    motor.targetVelocity = control.GetComponent<control>().getVelocidad() * (-1);
                    motor.freeSpin = false;
                    hinge.motor = motor;
                    hinge.useMotor = true;

                    GameObject.Find("baseGiro").GetComponent<Rigidbody>().isKinematic = false;
                    GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = true;
                    GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = true;
                    rb.constraints = RigidbodyConstraints.None;
                }
                if(control.GetComponent<control>().getModoMovimiento() == "coordenadas")
                {
                    coordenada = true;
                    GameObject.Find("Cube1").GetComponent<LookAt>().enabled = true;
                }
                
            }
        }
        
    }
    private void OnMouseUp()
    {
        pres = true;
        coordenada = false;
        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = false;
        GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
        GameObject.Find("baseGiro").GetComponent<Rigidbody>().isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        if (control.GetComponent<control>().getChoque() == true)
        {
            GameObject.Find("Movimiento").transform.Translate(-Vector3.right * 5f * Time.deltaTime, Space.World);
        }
    }
}
