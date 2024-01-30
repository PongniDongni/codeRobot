using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter : MonoBehaviour
{
    protected GameObject boton;
    protected GameObject control;
    private void Start()
    {

        boton = GameObject.Find("Cubo.030");
        control = GameObject.Find("control");
    }
    private void OnMouseDown()
    {
        boton.transform.localPosition += new Vector3(0, 0, -0.05f);
        if (control.GetComponent<control>().getNumero() != "---" && control.GetComponent<control>().getNumero() != "")
        {
            if (int.Parse(control.GetComponent<control>().getNumero()) <= 100 && int.Parse(control.GetComponent<control>().getNumero()) >= 0)
            {
                if (control.GetComponent<control>().getModoControl() == "guardar_posiciones")
                {
                    control.GetComponent<control>().guardar_posiciones();
                }
                else if (control.GetComponent<control>().getModoControl() == "cargar_posiciones")
                {
                    control.GetComponent<control>().cargar_posiciones();
                }
                else if (control.GetComponent<control>().getModoControl() == "guardar_posiciones_lista")
                {
                    //control.GetComponent<control>().guardar_posiciones_lista();
                }
                else if (control.GetComponent<control>().getModoControl() == "cargar_posiciones_lista")
                {
                    //control.GetComponent<control>().cargar_posiciones_lista();
                }
                else if (control.GetComponent<control>().getModoControl() == "cambiar_speed")
                {
                    control.GetComponent<control>().cambiar_Speed();
                }
                else if (control.GetComponent<control>().getModoControl() == "run_posicion")
                {
                    if (int.Parse(control.GetComponent<control>().getNumero()) == 0)
                    {
                        control.GetComponent<control>().run_posicion();
                    }
                    else
                    {
                        GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "Debe ingresar un número valido";
                    }

                }
            }
        }
        else
        {
            if (control.GetComponent<control>().getModoControl() == "rotacion")
            {
                GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "Debe seleccionar una operación"; //mensaje

            }
            else
            {
                GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "Debe ingresar un número valido"; //mensaje
            }
        }


    }
    private void OnMouseUp()
    {
        boton.transform.localPosition += new Vector3(0, 0, 0.05f);
    }
}
