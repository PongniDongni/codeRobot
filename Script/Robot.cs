using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private GameObject control;
    public bool nombre = false;
    void Start()
    {
        control = (GameObject.Find("control"));
        
    }

    void OnCollisionEnter(Collision collision)
    {
        
        foreach (ContactPoint contact in collision.contacts)
        {
            if (collision.gameObject.tag == "Mesa")
            {
                control.GetComponent<control>().setChoque(true);

            }
            if (collision.gameObject.tag == "Madera1")
            {
                nombre = true;
                control.GetComponent<control>().setObjeto(true);
                control.GetComponent<control>().setNombre("madera");
                control.GetComponent<control>().setPieza(this.name);
            }
            if (collision.gameObject.tag == "Madera2")
            {
                nombre = true;
                control.GetComponent<control>().setObjeto(true);
                control.GetComponent<control>().setNombre("madera2");
                control.GetComponent<control>().setPieza(this.name);
            }
            if (collision.gameObject.tag == "Madera3")
            {
                nombre = true;
                control.GetComponent<control>().setObjeto(true);
                control.GetComponent<control>().setNombre("otro");
                control.GetComponent<control>().setPieza(this.name);
            }
            

        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Mesa")
        {
            GameObject.Find("pinza1").GetComponent<Rigidbody>().isKinematic = false;
            GameObject.Find("pinza2").GetComponent<Rigidbody>().isKinematic = false;
            control.GetComponent<control>().setChoque(false);
            GameObject.Find("PanelChoque").GetComponent<UnityEngine.UI.Image>().enabled = false;
            GameObject.Find("ErrorChoque").GetComponent<UnityEngine.UI.Text>().text = "";
        }
        
    }
    void OnCollisionStay(Collision other)
    {
        foreach (ContactPoint contact in other.contacts)
        {
            if (other.gameObject.tag == "Mesa")
            {
                control.GetComponent<control>().setChoque(true);
            }
        }
    }
}
