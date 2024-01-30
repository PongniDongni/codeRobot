using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonRes : MonoBehaviour
{
    protected GameObject boton;
    protected GameObject control;
    protected GameObject mad;
    protected GameObject mad2;
    protected GameObject mad3;
    protected Vector3 pos1;
    protected Vector3 pos2;
    protected Vector3 pos3;
    protected Quaternion rot;
    protected Quaternion rot2;
    void Start()
    {
        GameObject.Find("ButtonRestad").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
        control = GameObject.Find("control");
        mad = GameObject.Find("madera");
        mad2 = GameObject.Find("madera2");
        mad3 = GameObject.Find("otro");
        pos1 = mad.transform.position;
        pos2 = mad2.transform.position;
        pos3 = mad3.transform.position;
        rot = mad3.transform.rotation;
        rot2 = mad2.transform.rotation;
    }

    private void OnButtonClick()
    {
        if (control.GetComponent<control>().getObjeto() == false)
        {
            mad3.transform.position = pos3;
            mad3.transform.rotation = rot;

            if (control.GetComponent<control>().getMadera() == "Normal")
            {
                mad.transform.position = pos1;
                mad.transform.rotation = rot;
                mad2.transform.position = pos2;
                mad2.transform.rotation = rot2;

            }
            else
            {
                mad.transform.position = pos2;
                mad2.transform.position = pos1;
                mad2.transform.rotation = rot2;
            }
        }
        else
        {
            GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "No se puede restablecer posicion con la madera tomada";
        }
    }
}
