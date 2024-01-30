using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuñecaJoint2 : MonoBehaviour
{
    HingeJoint hinge;
    Rigidbody rb;
    bool pres = false;
    protected GameObject boton;
    protected GameObject control;

    void Start()
    {
        boton = GameObject.Find("Cubo.010");
        hinge = GameObject.Find("Mano360").GetComponent<HingeJoint>();
        rb = GameObject.Find("Mano360").GetComponent<Rigidbody>();
        control = GameObject.Find("control");

    }
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        pres = true;
        boton.transform.localPosition += new Vector3(0, 0, -0.05f);
        GameObject.Find("pinza1").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("pinza2").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";
        //GameObject.Find("Cube").transform.SetParent(GameObject.Find("baseGiro").transform);
        control = GameObject.Find("control");
        if (control.GetComponent<control>().getModoControl() == "rotacion")
        {
            if(control.GetComponent<control>().getChoque() == false)
            {
                GameObject.Find("Cube2").transform.SetParent(GameObject.Find("baseGiro").transform);
                JointLimits limits = hinge.limits;
                limits.min = -180;
                limits.bounciness = 0;
                limits.bounceMinVelocity = 0.2f;
                limits.max = 180;
                hinge.limits = limits;
                hinge.useLimits = true;

                var motor = hinge.motor;
                motor.force = control.GetComponent<control>().getVelocidad();
                motor.targetVelocity = control.GetComponent<control>().getVelocidad() * (-1);
                motor.freeSpin = false;
                hinge.motor = motor;
                hinge.useMotor = true;

                GameObject.Find("Mano360").GetComponent<Rigidbody>().isKinematic = false;
                rb.constraints = RigidbodyConstraints.None;
            }
            
        }
            
    }
    private void OnMouseUp()
    {
        pres = true;
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";
        GameObject.Find("Mano360").GetComponent<Rigidbody>().isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }
}
