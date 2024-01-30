using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CodoJoint : MonoBehaviour
{
    HingeJoint hinge;
    HingeJoint hingemano;
    protected GameObject boton;
    protected GameObject control;
    HingeJoint hingebrazo;
    Rigidbody rb;
    private bool coordenada = false;

    void Start()
    {
        boton = GameObject.Find("Cubo.007");
        hinge = GameObject.Find("codo").GetComponent<HingeJoint>();
        hingemano = GameObject.Find("restoMano").GetComponent<HingeJoint>();
        control = GameObject.Find("control");
        rb = GameObject.Find("codo").GetComponent<Rigidbody>();
        hingebrazo = GameObject.Find("brazo").GetComponent<HingeJoint>();

    }
    void Update()
    {
        float angle = GameObject.Find("codo").GetComponent<Transform>().localEulerAngles.z;
        if (angle > 180)
        {
            angle = angle - 360;
        }
        //Debug.Log(angle);

        if (Mathf.Round(angle) >= 15)
        {
            GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
            GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado del codo";
            Debug.Log("Limite codo positivo");
        }
        if (Mathf.Round(angle) <= -130)
        {
            Debug.Log("Limite codo negativo");
            GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
            GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado del codo";
        }
        
        if(coordenada == true)
        {
            if(control.GetComponent<control>().getChoque() == false)
            {
                GameObject.Find("Movimiento").transform.Translate(Vector3.up * 1f * Time.deltaTime, Space.World);
            }
            
        }

    }
    private void OnMouseDown(){
        boton.transform.localPosition += new Vector3(0,0,-0.05f);
        GameObject.Find("pinza1").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("pinza2").GetComponent<MeshCollider>().enabled = true;
        control = GameObject.Find("control");
        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";
        
        if (control.GetComponent<control>().getModoControl() == "rotacion")
        {
            
                if(control.GetComponent <control>().getModoMovimiento() == "articulado")
                {
                    JointLimits limits = hinge.limits;
                    limits.min = -130;
                    limits.bounciness = 0;
                    limits.bounceMinVelocity = 0.2f;
                    limits.max = 15;
                    hinge.limits = limits;
                    hinge.useLimits = true;

                    var motor = hinge.motor;
                    motor.force = control.GetComponent<control>().getVelocidad();
                    motor.targetVelocity = control.GetComponent<control>().getVelocidad();
                    motor.freeSpin = false;
                    hinge.motor = motor;
                    hinge.useMotor = true;

                    GameObject.Find("codo").GetComponent<Rigidbody>().isKinematic = false;
                    GameObject.Find("restoMano").GetComponent<Rigidbody>().isKinematic = false;
                    GameObject.Find("Mano360").GetComponent<Rigidbody>().isKinematic = false;
                    hingemano.useSpring = true;
                    hingemano.useMotor = false;
                    rb.constraints = RigidbodyConstraints.None;
                }
                if(control.GetComponent<control>().getModoMovimiento() == "coordenadas")
                {
                    coordenada = true;
                    GameObject.Find("Cube1").GetComponent<LookAt>().enabled = true;

                }
            
        }

            
    }
    private void OnMouseUp(){
        boton.transform.localPosition += new Vector3(0,0,0.05f);
        coordenada = false;
        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";
        GameObject.Find("codo").GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find("restoMano").GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find("Mano360").GetComponent<Rigidbody>().isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        if (control.GetComponent<control>().getChoque() == true)
        {
            GameObject.Find("Movimiento").transform.Translate(-Vector3.up * 5f * Time.deltaTime, Space.World);
        }

    }
}
