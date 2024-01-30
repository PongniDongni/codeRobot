using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Pinza : MonoBehaviour
{
    HingeJoint hinge;
    Rigidbody rb;
    HingeJoint hinge2;
    Rigidbody rb2;
    private bool activo = false;
    private bool stopRotacion = false;
    private bool abierto = false;
    protected Vector3 rotacion;
    protected Vector3 rotacion2;
    protected GameObject pieza1;
    protected GameObject pieza;
    protected GameObject pieza2;
    protected GameObject boton;
    protected GameObject control;
    private GameObject madera; // Objeto agarrado por la pinza
    private Rigidbody gravedad;
    private GameObject madera2; // Objeto agarrado por la pinza
    private Rigidbody gravedad2;
    private GameObject otro;
    private float Angle;
    private float Angle2;
    private bool si=false;
    private bool s=false;
    private void Start()
    {
        hinge = GameObject.Find("pinza1").GetComponent<HingeJoint>();
        hinge2 = GameObject.Find("pinza2").GetComponent<HingeJoint>();
        rb = GameObject.Find("pinza1").GetComponent<Rigidbody>();
        rb2 = GameObject.Find("pinza2").GetComponent<Rigidbody>();
        
        //pieza1 = GameObject.Find("Pinza1");
        pieza = GameObject.Find("pinza1");
        //pieza2 = GameObject.Find("Pinza2");
        boton = GameObject.Find("Cubo.026");
        control = GameObject.Find("control");
        madera = GameObject.Find("madera");
        madera2 = GameObject.Find("madera2");
        
        gravedad = madera.GetComponent<Rigidbody>();
        gravedad2 = madera2.GetComponent<Rigidbody>();
        //Angle = pieza1.GetComponent<HingeJoint>().angle;
        Angle2 = Mathf.Round(GameObject.Find("pinza1").GetComponent<Transform>().localEulerAngles.x);
    }

    private void FixedUpdate()
    {
        if (activo)
        {

            madera = GameObject.Find(control.GetComponent<control>().getNombre());
            gravedad = madera.GetComponent<Rigidbody>();
            
            if (control.GetComponent<control>().getModoMovimiento() == "articulado")
            {
                pieza = GameObject.Find("pinza1");
            }
            else
            {
                //pieza = GameObject.Find("Pinza1");
            }

            if (abierto == true)
            {
                if (control.GetComponent<control>().getObjeto())
                {
                    if (!stopRotacion) // si existe objeto
                    {
                        JointLimits limits = hinge.limits;
                        limits.min = -45;
                        limits.bounciness = 0;
                        limits.bounceMinVelocity = 0;
                        limits.max = 0;
                        hinge.limits = limits;
                        hinge.useLimits = true;

                        var motor = hinge.motor;
                        motor.force = control.GetComponent<control>().getVelocidad();
                        motor.targetVelocity = control.GetComponent<control>().getVelocidad();
                        motor.freeSpin = false;
                        hinge.motor = motor;
                        hinge.useMotor = true;


                        JointLimits limits2 = hinge2.limits;
                        limits2.min = -45;
                        limits2.bounciness = 0;
                        limits2.bounceMinVelocity = 0;
                        limits2.max = 0;
                        hinge2.limits = limits2;
                        hinge2.useLimits = true;

                        var motor2 = hinge2.motor;
                        motor2.force = control.GetComponent<control>().getVelocidad();
                        motor2.targetVelocity = control.GetComponent<control>().getVelocidad();
                        motor2.freeSpin = false;
                        hinge2.motor = motor2;
                        hinge2.useMotor = true;

                        GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = false;
                        rb.constraints = RigidbodyConstraints.None;

                        GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = false;
                        rb2.constraints = RigidbodyConstraints.None;

                        //351 - 20
                        if (control.GetComponent<control>().getObjeto() == true || Mathf.Round(GameObject.Find("pinza1").GetComponent<Transform>().localEulerAngles.x) == Angle2)
                        {

                            //GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = true;
                            //GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = true;
                            
                            if(madera.GetComponent<Madera>().col1 == true && madera.GetComponent<Madera>().col2 == true)
                            {
                                stopRotacion = true;
                                abierto = false;
                                Debug.Log("puedo tomar la madera");
                                madera.transform.SetParent(GameObject.Find("Vacío").transform);
                                GameObject.Find("Vacío").GetComponent<HingeJoint>().connectedBody = madera.GetComponent<Rigidbody>();
                                gravedad.useGravity = false;
                                gravedad.isKinematic = true;
                                motor.targetVelocity = 0;
                                hinge.motor = motor;
                                motor2.targetVelocity = 0;
                                hinge2.motor = motor2;
                            }



                            if (madera.transform.parent == GameObject.Find("Vacío").transform)
                            {
                                //madera.transform.localPosition = new Vector3(madera.transform.localPosition.x, 0, madera.transform.localPosition.z);
                            }
                            
                            //rb2.constraints = RigidbodyConstraints.FreezeRotationX;
                            //rb.constraints = RigidbodyConstraints.FreezeRotationX;
                            //madera.transform.position = new Vector3(GameObject.Find("Vacío").transform.position.x,0,0);
                                                      
                            
                        }
                    }
                }
                else
                {
                    if (!stopRotacion)
                    {
                        JointLimits limits = hinge.limits;
                        limits.min = -45;
                        limits.bounciness = 0;
                        limits.bounceMinVelocity = 0;
                        limits.max = 0;
                        hinge.limits = limits;
                        hinge.useLimits = true;

                        var motor = hinge.motor;
                        motor.force = control.GetComponent<control>().getVelocidad();
                        motor.targetVelocity = control.GetComponent<control>().getVelocidad();
                        motor.freeSpin = false;
                        hinge.motor = motor;
                        hinge.useMotor = true;


                        JointLimits limits2 = hinge2.limits;
                        limits2.min = -45;
                        limits2.bounciness = 0;
                        limits2.bounceMinVelocity = 0;
                        limits2.max = 0;
                        hinge2.limits = limits2;
                        hinge2.useLimits = true;

                        var motor2 = hinge2.motor;
                        motor2.force = control.GetComponent<control>().getVelocidad();
                        motor2.targetVelocity = control.GetComponent<control>().getVelocidad();
                        motor2.freeSpin = false;
                        hinge2.motor = motor2;
                        hinge2.useMotor = true;

                        GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = false;
                        rb.constraints = RigidbodyConstraints.None;

                        GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = false;
                        rb2.constraints = RigidbodyConstraints.None;
                        Debug.Log(Mathf.Round(GameObject.Find("pinza1").GetComponent<Transform>().localEulerAngles.x));
                        if (Mathf.Round(GameObject.Find("pinza1").GetComponent<Transform>().localEulerAngles.x) == Angle2)
                        {
                            Debug.Log("Stop");
                            //GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = true;
                            //GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = true;
                            //rb2.constraints = RigidbodyConstraints.FreezeRotationX;
                            //rb.constraints = RigidbodyConstraints.FreezeRotationX;
                            stopRotacion = true;
                            abierto = false;
                        }
                    }
                }
            }
            else if (abierto == false) // cerrado
            {
                Debug.Log(abierto);
                if (!control.GetComponent<control>().getObjeto()) //sin objeto
                {
                    if (!stopRotacion)
                    {
                        JointLimits limits = hinge.limits;
                        limits.min = -45;
                        limits.bounciness = 0;
                        limits.bounceMinVelocity = 0;
                        limits.max = 0;
                        hinge.limits = limits;
                        hinge.useLimits = true;

                        var motor = hinge.motor;
                        motor.force = control.GetComponent<control>().getVelocidad();
                        motor.targetVelocity = control.GetComponent<control>().getVelocidad() * (-1);
                        motor.freeSpin = false;
                        hinge.motor = motor;
                        hinge.useMotor = true;


                        JointLimits limits2 = hinge2.limits;
                        limits2.min = -45;
                        limits2.bounciness = 0;
                        limits2.bounceMinVelocity = 0;
                        limits2.max = 0;
                        hinge2.limits = limits2;
                        hinge2.useLimits = true;

                        var motor2 = hinge2.motor;
                        motor2.force = control.GetComponent<control>().getVelocidad();
                        motor2.targetVelocity = control.GetComponent<control>().getVelocidad() * (-1);
                        motor2.freeSpin = false;
                        hinge2.motor = motor;
                        hinge2.useMotor = true;

                        GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = false;
                        rb.constraints = RigidbodyConstraints.None;

                        GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = false;
                        rb2.constraints = RigidbodyConstraints.None;
                        Debug.Log(Mathf.Round(GameObject.Find("pinza1").GetComponent<Transform>().localEulerAngles.x));
                        if (Mathf.Round(GameObject.Find("pinza1").GetComponent<Transform>().localEulerAngles.x) == 355 || Mathf.Round(GameObject.Find("pinza1").GetComponent<Transform>().localEulerAngles.x) >= 350)
                        {
                            Debug.Log("Stop");
                            //GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = true;
                            //GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = true;
                            //rb2.constraints = RigidbodyConstraints.FreezeRotationX;
                            //rb.constraints = RigidbodyConstraints.FreezeRotationX;
                            stopRotacion = true;
                            abierto = true;
                        }
                    }
                }
                else
                {
                    if (!stopRotacion)
                    {
                        madera.GetComponent<Madera>().col1 = false;
                        madera.GetComponent<Madera>().col2 = false;
                        GameObject.Find("pinza1").GetComponent<Robot>().nombre = false;
                        GameObject.Find("pinza2").GetComponent<Robot>().nombre = false;
                        madera.transform.SetParent(null);
                        GameObject.Find("Vacío").GetComponent<HingeJoint>().connectedBody = null;
                        control.GetComponent<control>().setChoque(false);
                        control.GetComponent<control>().setObjeto(false);
                        gravedad.isKinematic = false;
                        gravedad.useGravity = true;
                        control.GetComponent<control>().setObjeto(false);
                        JointLimits limits = hinge.limits;
                        limits.min = -45;
                        limits.bounciness = 0;
                        limits.bounceMinVelocity = 0;
                        limits.max = 0;
                        hinge.limits = limits;
                        hinge.useLimits = true;

                        var motor = hinge.motor;
                        motor.force = control.GetComponent<control>().getVelocidad();
                        motor.targetVelocity = control.GetComponent<control>().getVelocidad() * (-1);
                        motor.freeSpin = false;
                        hinge.motor = motor;
                        hinge.useMotor = true;


                        JointLimits limits2 = hinge2.limits;
                        limits2.min = -45;
                        limits2.bounciness = 0;
                        limits2.bounceMinVelocity = 0;
                        limits2.max = 0;
                        hinge2.limits = limits2;
                        hinge2.useLimits = true;

                        var motor2 = hinge2.motor;
                        motor2.force = control.GetComponent<control>().getVelocidad();
                        motor2.targetVelocity = control.GetComponent<control>().getVelocidad() * (-1);
                        motor2.freeSpin = false;
                        hinge2.motor = motor;
                        hinge2.useMotor = true;

                        GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = false;
                        rb.constraints = RigidbodyConstraints.None;

                        GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = false;
                        rb2.constraints = RigidbodyConstraints.None;
                        Debug.Log(Mathf.Round(GameObject.Find("pinza1").GetComponent<Transform>().localEulerAngles.x));
                        if (Mathf.Round(GameObject.Find("pinza1").GetComponent<Transform>().localEulerAngles.x) == 355 || Mathf.Round(GameObject.Find("pinza1").GetComponent<Transform>().localEulerAngles.x) >= 350)
                        {
                            //GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = true;
                            //GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = true;
                            //rb2.constraints = RigidbodyConstraints.FreezeRotationX;
                            //rb.constraints = RigidbodyConstraints.FreezeRotationX;
                            stopRotacion = true;
                            abierto = true;
                        }
                    }

                }
            }
            else
            {
                abierto = !abierto;
                activo = false;
                GameObject.Find("pinza1").GetComponent<Robot>().nombre = false;
                GameObject.Find("pinza2").GetComponent<Robot>().nombre = false;
                GameObject.Find("Vacío").GetComponent<HingeJoint>().connectedBody = null;
                control.GetComponent<control>().setChoque(false);
                control.GetComponent<control>().setObjeto(false);
                madera.transform.SetParent(null);
                gravedad.isKinematic = false;
                gravedad.useGravity = true;
                control.GetComponent<control>().setObjeto(false);
                madera.GetComponent<Madera>().col1 = false;
                madera.GetComponent<Madera>().col2 = false;
            }
        }
    }

    public void OnMouseDown()
    {
        GameObject.Find("pinza1").GetComponent<MeshCollider>().enabled = false;
        GameObject.Find("pinza2").GetComponent<MeshCollider>().enabled = false;
        boton.transform.localPosition += new Vector3(0, 0, -0.05f);
        if (control.GetComponent<control>().getModoControl() == "rotacion")
        {
            activo = true;
            stopRotacion = false;
            GameObject.Find("Vacío").GetComponent<HingeJoint>().connectedBody = null;
            control.GetComponent<control>().setChoque(false);
            control.GetComponent<control>().setObjeto(false);
            madera.transform.SetParent(null);
            gravedad.isKinematic = false;
            gravedad.useGravity = true;
            control.GetComponent<control>().setObjeto(false);
            GameObject.Find("pinza1").GetComponent<Robot>().nombre = false;
            GameObject.Find("pinza2").GetComponent<Robot>().nombre = false;
            madera.GetComponent<Madera>().col1 = false;
            madera.GetComponent<Madera>().col2 = false;

        }
    }
    public void OnMouseUp()
    {
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
        if (abierto == true)
        {
            GameObject.Find("Vacío").GetComponent<HingeJoint>().connectedBody = null;
            control.GetComponent<control>().setChoque(false);
            control.GetComponent<control>().setObjeto(false);
            madera.transform.SetParent(null);
            gravedad.isKinematic = false;
            gravedad.useGravity = true;
            control.GetComponent<control>().setObjeto(false);
            GameObject.Find("pinza1").GetComponent<Robot>().nombre = false;
            GameObject.Find("pinza2").GetComponent<Robot>().nombre = false;
            madera.GetComponent<Madera>().col1 = false;
            madera.GetComponent<Madera>().col2 = false;
        }
    }
}
