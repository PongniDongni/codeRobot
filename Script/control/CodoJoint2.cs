using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CodoJoint2 : MonoBehaviour
{
    HingeJoint hinge; //codo
    HingeJoint hingemano;
    HingeJoint hingebrazo;
    protected GameObject boton;
    protected GameObject control;
    Rigidbody rb;
    private bool pres = false;
    private bool coordenada = false;

    void Start()
    {
        boton = GameObject.Find("Cubo12");
        hinge = GameObject.Find("codo").GetComponent<HingeJoint>();
        hingemano = GameObject.Find("restoMano").GetComponent <HingeJoint>();
        control = GameObject.Find("control");
        rb = GameObject.Find("codo").GetComponent<Rigidbody>();
        hingebrazo = GameObject.Find("brazo").GetComponent<HingeJoint>();

    }
    void Update()
    {
        
        if(coordenada == true)
        {
            if (control.GetComponent<control>().getChoque() == false)
            {
                GameObject.Find("Movimiento").transform.Translate(-Vector3.up * 1f * Time.deltaTime, Space.World);
            }
        }
    }
    private void OnMouseDown(){
        boton.transform.localPosition += new Vector3(0,0,-0.05f);
        pres = true;
        GameObject.Find("pinza1").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("pinza2").GetComponent<MeshCollider>().enabled = true;
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
                    motor.targetVelocity = control.GetComponent<control>().getVelocidad() * (-1);
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
        pres = true;
        coordenada = false;
        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";
        GameObject.Find("codo").GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find("restoMano").GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find("Mano360").GetComponent<Rigidbody>().isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        if (control.GetComponent<control>().getChoque() == true)
        {
            GameObject.Find("Movimiento").transform.Translate(Vector3.up * 5f * Time.deltaTime, Space.World);
        }
    }
}
