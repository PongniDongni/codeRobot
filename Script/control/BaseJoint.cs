using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseJoint : MonoBehaviour
{
    HingeJoint hinge;
    Rigidbody rb;
    bool pres = false;
    bool coordenada = false;
    protected GameObject boton;
    protected GameObject control;

    void Start()
    {
        boton = GameObject.Find("Cubo.009");
        hinge = GameObject.Find("baseGiro").GetComponent<HingeJoint>();
        rb = GameObject.Find("baseGiro").GetComponent <Rigidbody>();
        control = GameObject.Find("control");

    }
    void Update()
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
        if (Mathf.Round(angle) >= 155)
        {
            Debug.Log("Limite base positivo");
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
            if (Mathf.Round(anglecodo) > 0)
            {
                Debug.Log("Limite base positivo");
            }
            else
            {
                if (Mathf.Round(angle3) < 0)
                {
                    Debug.Log("Limite base positivo");
                }
                else
                {
                    if (coordenada == true)
                    {
                        if (control.GetComponent<control>().getChoque() == false)
                        {
                            GameObject.Find("Movimiento").transform.Translate(-Vector3.right * 1f * Time.deltaTime, Space.World);
                        }
                    }
                }
                
            }
            
        }
        
        if (control.GetComponent<control>().getModoMovimiento() == "coordenadas")
        {

            float clampedX = Mathf.Clamp(GameObject.Find("Movimiento").transform.position.x, -1.3f, 3.6f);
            float clampedY = Mathf.Clamp(GameObject.Find("Movimiento").transform.position.y, 0.95f, 5.2f); //cambiado
            float clampedZ = Mathf.Clamp(GameObject.Find("Movimiento").transform.position.z, -6.1f, -0.15f); //y

            Vector3 clampedPosition = new Vector3(clampedX, clampedY, clampedZ);
            // Asignar la posición limitada
            GameObject.Find("Movimiento").transform.position = clampedPosition;

            if (clampedZ == -6.4f)
            {
                Debug.Log("Limite brazo positivo");
                GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
                GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado";
            }
            if (clampedZ == -0.15f)
            {
                Debug.Log("Limite brazo positivo");
                GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
                GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado";
            }
            if (clampedX == -1.3f)
            {
                Debug.Log("Limite brazo positivo");
                GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
                GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado";
            }
            if (clampedX == 3.6f)
            {
                Debug.Log("Limite brazo positivo");
                GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
                GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado";
            }
            if (clampedY == 0.95f)
            {
                Debug.Log("Limite brazo positivo");
                GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
                GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado";
            }
            if (clampedY == 5.2f)
            {
                Debug.Log("Limite brazo positivo");
                GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
                GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Límite alcanzado";
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
                    limits.min = Mathf.Round(GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle) - atras;
                    limits.bounciness = 0;
                    limits.bounceMinVelocity = 0;
                    limits.max = 155;
                    hinge.limits = limits;
                    hinge.useLimits = true;

                    var motor = hinge.motor;
                    motor.force = 1;
                    motor.targetVelocity = control.GetComponent<control>().getVelocidad() * (-1);
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
        float anglebrazo = GameObject.Find("brazo").GetComponent<Transform>().eulerAngles.z - 180;
        float anglemano = GameObject.Find("restoMano").GetComponent<Transform>().localEulerAngles.y;
        if (anglemano > 180)
        {
            anglemano = anglemano - 360;
        }
        float angle4 = GameObject.Find("Mano360").GetComponent<Transform>().localEulerAngles.z;
        if (angle4 > 180)
        {
            angle4 = angle4 - 360;
        }
        float angle5 = GameObject.Find("codo").GetComponent<Transform>().localEulerAngles.z;
        if (angle5 > 180)
        {
            angle5 = angle5 - 360;
        }
        string cadena2 = (Mathf.Round(angle)) + ", " + Mathf.Round(anglebrazo) + ", " + Mathf.Round(angle5) + ", " + Mathf.Round(anglemano) + ", " + Mathf.Round(angle4) + " | " + (Math.Truncate(GameObject.Find("Movimiento").transform.position.x * 100) / 100) + ", " + (Math.Truncate(GameObject.Find("Movimiento").transform.position.y * 100) / 100) + ", " + (Math.Truncate(GameObject.Find("Movimiento").transform.position.z * 100) / 100);
        GameObject.Find("Posicion").GetComponent<UnityEngine.UI.Text>().text = cadena2;

    }
    private void OnMouseDown(){
        boton.transform.localPosition += new Vector3(0,0,-0.05f);
        GameObject.Find("pinza1").GetComponent<MeshCollider>().enabled = true;
        GameObject.Find("pinza2").GetComponent<MeshCollider>().enabled = true;

        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";

        pres = true;
        if(control.GetComponent<control>().getChoque() == false)
        {
            if (control.GetComponent<control>().getModoControl() == "rotacion")
            {
                if(control.GetComponent <control>().getModoMovimiento() == "articulado")
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
                    motor.targetVelocity = control.GetComponent<control>().getVelocidad();
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
                    //GameObject.Find("Cube").transform.SetParent(GameObject.Find("pega").transform);
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
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = false;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "";
        GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = false;
        GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = false;
        GameObject.Find("baseGiro").GetComponent<Rigidbody>().isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        if (control.GetComponent<control>().getChoque() == true)
        {
            GameObject.Find("Movimiento").transform.Translate(Vector3.right * 5f * Time.deltaTime, Space.World);
        }
    }
}
