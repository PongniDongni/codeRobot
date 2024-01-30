using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static System.Math;

public class control : MonoBehaviour
{
    ArrayList DatosGuardados = new ArrayList();

    private string modo = "rotacion";
    private string Movimiento = "articulado";
    private string madera = "Normal";
    private string nombre = "madera";
    private string pieza = "pinza1";
    
    private bool bases = false;
    private bool brazos = false;
    private bool codos = false;
    private bool mano = false;
    private bool mano360 = false;
    
    private int velocidad = 50;
    private bool choque = false;
    private bool brazo = false;
    private bool codo = false;
    private bool objeto = false;
    private bool nuevo = false;
    private bool pinza = false;
    public float [][] posiciones = new float[100][];
    //public float[] lista = {30f,35f,-123f,-50f, 65f }; // posicion inicial robot en joins  30f,34f,-125f,-50f, 65f
    private float [][][] lista_posiciones = new float[100][][];
    private Queue<int> lista_posiciones_aux = new Queue<int>();
    private string ejecucion = "no";
    private string numero = "---";
    private string numero_lista = "0";
    private int contador = 0;
    private int conta = 0;
    //coordenadas
    private float [][] posicionXYZ = new float[100][];
    //private float [] xyz = { 1.114007f, 4.662637f, -2.351572f }; // posicion inicial robot en xyz
    public int getVelocidad(){
        return velocidad;
    }
    public void setVelocidad(int a){
        velocidad = a;
    }
    public void setChoque(bool c){
        choque=c;
    }
    public bool getChoque(){
        return choque;
    }
    public void setPinza(bool c){
        pinza=c;
    }
    public bool getPinza(){
        return pinza;
    }

     public void setObjeto(bool c){
        objeto=c;
     }
    public bool getObjeto(){
        return objeto;
    }

    public void setModoControl(string a){
        if(a=="rotacion"){
            numero = "---";
        }else{
            numero="---";//"0"
        }
        GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text=numero;
        modo = a;
    }

    public string getModoControl(){
        return modo;
    }

    public void setModoMovimiento(string a){
        if(a=="articulado"){
            numero = "---";
        }else{
            numero = "---";//"0"
        }
        GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text=numero;
        Movimiento = a;
    }

    public string getModoMovimiento(){
        return Movimiento;
    }
    public void setMadera(string a){
        madera = a;
    }

    public string getMadera(){
        return madera;
    }
    public void setNombre(string a){
        nombre = a;
    }

    public string getNombre(){
        return nombre;
    }
    public void setPieza(string a)
    {
        pieza = a;
    }

    public string getPieza()
    {
        return pieza;
    }

    public void setEjecucion(string a){
        ejecucion = a;
    }

    public string getEjecucion(){
        return ejecucion;
    }

    public void setNumero(string a){
        if(numero.Length<3){
            numero += a;
        }else{
            numero = a;
        }
        GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text=numero;
    }

    public void Numero(){
        if(numero.Length <= 1){
            numero = "";
        }else{
            numero = numero.Substring(0,numero.Length - 1);
            
        }
        GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text=numero;
    }

    public string getNumero(){
        return numero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float[] lista = { 30f, 35f, -124f, -45f, 65f };
        float[] xyz = { 1.114007f, 4.662637f, -2.351572f };
        posiciones[0] = lista;
        posicionXYZ[0] = xyz;
        if (choque)
        {
            
            GameObject.Find("PanelChoque").GetComponent<UnityEngine.UI.Image>().enabled = true;
            GameObject.Find("ErrorChoque").GetComponent<UnityEngine.UI.Text>().text = "Error en el trayecto, verificar el movimiento";
            GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
            //GameObject.Find("ButtonAceptar").GetComponent<UnityEngine.UI.Image>().enabled = true;
            //GameObject.Find("Textb").GetComponent<UnityEngine.UI.Text>().enabled = true;
            ejecucion = "no";
            setModoControl("rotacion");
            
        }
        if(Movimiento == "articulado")
        {
            float[] posicion = null;
            if (ejecucion == "posicion" || ejecucion == "lista")
            {
                if (ejecucion == "lista")
                {
                    if (lista_posiciones[int.Parse(numero_lista)] != null)
                    {
                        posicion = lista_posiciones[int.Parse(numero_lista)][contador];
                    }
                    else
                    {
                        posicion = null;
                    }
                }
                else
                {
                    posicion = posiciones[int.Parse(numero)];
                }
                if (posicion != null)
                {
                    GameObject.Find("Cube2").transform.SetParent(GameObject.Find("Vacío").transform);
                    if (Mathf.Round(GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle) != Mathf.Round(posicion[0]))
                    {
                        //rotation
                        //GameObject.Find("Cube2").transform.SetParent(GameObject.Find("baseGiro").transform);
                        if ((Abs(GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle - posicion[0]) / (velocidad / 50)) >= 1)
                        {
                            GameObject.Find("baseGiro").transform.Rotate(new Vector3(0, 0, 1) * (velocidad/50) * -1 * ((GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle - posicion[0]) / Abs(GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle - posicion[0])));
                        }
                        else
                        {
                            GameObject.Find("baseGiro").transform.Rotate(new Vector3(0, 0, 1) * -1 * ((GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle - posicion[0]) / Abs(GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle - posicion[0])));
                        }
                        
                    }
                    else
                    {
                        Debug.Log("base llegado");
                        bases = true;
                        
                    }
                    float angle0 = GameObject.Find("brazo").GetComponent<Transform>().localEulerAngles.z;
                    if (angle0 > 180)
                    {
                        angle0 = angle0 - 360;
                    }
                    
                    if (Mathf.Round(GameObject.Find("brazo").GetComponent<HingeJoint>().angle) != Mathf.Round(posicion[1]))
                    {
                        //GameObject.Find("Cube2").transform.SetParent(GameObject.Find("baseGiro").transform);
                        if ((Abs(GameObject.Find("brazo").GetComponent<HingeJoint>().angle - posicion[1]) / (velocidad / 50)) >= 1)
                        {
                            GameObject.Find("brazo").transform.Rotate(new Vector3(0, 0, 1) * (velocidad / 50) * -1 * ((GameObject.Find("brazo").GetComponent<HingeJoint>().angle - posicion[1]) / Abs(GameObject.Find("brazo").GetComponent<HingeJoint>().angle - posicion[1])));
                        }
                        else
                        {
                            GameObject.Find("brazo").transform.Rotate(new Vector3(0, 0, 1) * -1 * ((GameObject.Find("brazo").GetComponent<HingeJoint>().angle - posicion[1]) / Abs(GameObject.Find("brazo").GetComponent<HingeJoint>().angle - posicion[1])));
                        }

                    }
                    else
                    {
                        Debug.Log("brazo llegado");
                        brazos = true;
                        
                    }
                    float angle = GameObject.Find("codo").GetComponent<Transform>().localEulerAngles.z;
                    if (angle > 180)
                    {
                        angle = angle - 360;
                    }
                   
                    if (Mathf.Round(angle) != Mathf.Round(posicion[2]))
                    {
                        //GameObject.Find("Cube2").transform.SetParent(GameObject.Find("baseGiro").transform);
                        if ((Abs(Mathf.Round(angle) - posicion[2]) / (velocidad / 50)) >= 1)
                        {
                            GameObject.Find("codo").transform.Rotate(new Vector3(0, 0, 1) * (velocidad / 50) * -1 * ((Mathf.Round(angle) - posicion[2]) / Abs(Mathf.Round(angle) - posicion[2])));
                        }
                        else
                        {
                            GameObject.Find("codo").transform.Rotate(new Vector3(0, 0, 1) * -1 * ((Mathf.Round(angle) - posicion[2]) / Abs(Mathf.Round(angle) - posicion[2])));
                        }
                        
                    }
                    else
                    {
                        Debug.Log("codo llegado");
                        codos = true;
                        
                    }
                    
                    //mano
                    float angle2 = GameObject.Find("restoMano").GetComponent<Transform>().localEulerAngles.y;
                    if (angle2 > 180)
                    {
                        angle2 = angle2 - 360;
                    }
                    if (Mathf.Round(angle2) != Mathf.Round(posicion[3]))
                    {
                       // GameObject.Find("Cube2").transform.SetParent(GameObject.Find("Vacío").transform);
                        if ((Abs((Mathf.Round((angle2))) - posicion[3]) / (velocidad/50)) >= 1)
                        {
                            GameObject.Find("restoMano").transform.Rotate(new Vector3(-1, 0, 0) * (velocidad/50) * -1 * (((Mathf.Round((angle2))) - posicion[3]) / Abs((Mathf.Round((angle2))) - posicion[3])));
                            

                        }
                        else
                        {
                            GameObject.Find("restoMano").transform.Rotate(new Vector3(-1, 0, 0) * -1 * (((Mathf.Round((angle2))) - posicion[3]) / Abs((Mathf.Round((angle2))) - posicion[3])));
                            

                        }
                        
                    }
                    else
                    {
                        Debug.Log("restoMano llegado");
                        mano = true;
                       
                    }

                    float angle3 = GameObject.Find("Mano360").GetComponent<Transform>().localEulerAngles.z;
                    if (angle3 > 180)
                    {
                        angle3 = angle3 - 360;
                    }
                    if (Mathf.Round(angle3) != Mathf.Round(posicion[4]))
                    {
                       // GameObject.Find("Cube2").transform.SetParent(GameObject.Find("baseGiro").transform);
                        if ((Abs((Mathf.Round(angle3)) - posicion[4]) / (velocidad / 50)) >= 1)
                        {
                            GameObject.Find("Mano360").transform.Rotate(new Vector3(0, 0, 1) * (velocidad / 50) * -1 * (((Mathf.Round(angle3)) - posicion[4]) / Abs((Mathf.Round(angle3)) - posicion[4])));
                        }
                        else
                        {
                            GameObject.Find("Mano360").transform.Rotate(new Vector3(0, 0, 1) * -1 * (((Mathf.Round(angle3)) - posicion[4]) / Abs((Mathf.Round(angle3)) - posicion[4])));
                        }
                        
                    }
                    else
                    {
                        Debug.Log("Mano360 llegado");
                        mano360 = true;
                        
                    }


                    if (bases == true && brazos == true && mano360 == true && codos == true && mano == true)
                    {
                        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
                        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Posicion "+numero+ " cargada";
                        contador = 0;
                        ejecucion = "no";
                        setModoControl("rotacion");
                        Debug.Log("Listo");
                        GameObject.Find("baseGiro").GetComponent<Rigidbody>().isKinematic = true;
                        GameObject.Find("brazo").GetComponent<Rigidbody>().isKinematic = true;
                        GameObject.Find("codo").GetComponent<Rigidbody>().isKinematic = true;
                        GameObject.Find("restoMano").GetComponent<Rigidbody>().isKinematic = true;
                        GameObject.Find("Mano360").GetComponent<Rigidbody>().isKinematic = true;

                        GameObject.Find("codo").GetComponent<HingeJoint>().useSpring = false;
                        GameObject.Find("codo").GetComponent<HingeJoint>().useMotor = true;
                        GameObject.Find("restoMano").GetComponent<HingeJoint>().useSpring = false;
                        GameObject.Find("restoMano").GetComponent<HingeJoint>().useMotor = true;
                        GameObject.Find("Mano360").GetComponent<HingeJoint>().useSpring = false;
                        GameObject.Find("Mano360").GetComponent<HingeJoint>().useMotor = true;

                        GameObject.Find("baseGiro").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
                        GameObject.Find("brazo").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
                        GameObject.Find("codo").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
                        GameObject.Find("restoMano").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
                        GameObject.Find("Mano360").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;

                        GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "posicion cargada";
                        GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
                        
                        GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "Rotation";
                        
                        //GameObject.Find("Cube").transform.SetParent(GameObject.Find("pega").transform);

                    }
                }
            }
            else if (ejecucion == "posicion" && posiciones == null)
            {
                GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "No posee posicion guardada"; //mensaje
                numero = "---"; //"0"
                GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
                ejecucion = "no";
            }
            else if (ejecucion == "lista" && lista_posiciones[int.Parse(numero_lista)] == null)
            {
                //GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "No posee lista guardada";
                numero = "---";//"0"
                //GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
                ejecucion = "no";
            }
        }
        else
        {
            if (ejecucion == "PosicionXYZ")
            {
                GameObject.Find("Cube1").GetComponent<LookAt>().enabled = true;
                float[] posicion = null;
                posicion = posicionXYZ[int.Parse(numero)];
                if (posicion != null)
                {
                    Vector3 nuevo = new Vector3(posicion[0], posicion[1], posicion[2]);
                    Vector3 direccion = nuevo - GameObject.Find("Movimiento").transform.position;
                    GameObject.Find("Movimiento").transform.position += direccion.normalized * 1f * Time.deltaTime;
                    if (direccion.magnitude < 0.10f)
                    {
                        GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "posicion cargada"; //mensaje
                        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
                        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Posicion " + numero + " cargada";
                        setModoControl("rotacion");
                        GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
                        GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "Rotation";
                        ejecucion = "no";
                    }
                }
            }
            else if (ejecucion == "PosicionXYZ" && posicionXYZ == null)
            {
                //GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "No posee posicion guardada"; //mensaje
                numero = "---";//"0"
                //GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
                ejecucion = "no";
            }
        }
        if (ejecucion == "brazo")
        {
            GameObject.Find("brazo").GetComponent<Rigidbody>().isKinematic = true; // colocar falso al terminar
            GameObject.Find("codo").GetComponent<Rigidbody>().isKinematic = true;
            GameObject.Find("restoMano").GetComponent<Rigidbody>().isKinematic = true;
            GameObject.Find("Mano360").GetComponent<Rigidbody>().isKinematic = true;
            GameObject.Find("baseGiro").GetComponent<Rigidbody>().isKinematic = true;
            float[] posicion = { 35f, -10f, 30f };
            if (Mathf.Round(GameObject.Find("brazo").GetComponent<HingeJoint>().angle) != posicion[conta])
            {
                /*
                HingeJoint hingeBrazo = GameObject.Find("brazo").GetComponent<HingeJoint>();
                hingeBrazo.useMotor = false;
                JointSpring springBrazo = hingeBrazo.spring;
                springBrazo.spring = 21000;
                springBrazo.damper = 10100;
                springBrazo.targetPosition = posicion[conta];
                hingeBrazo.spring = springBrazo;
                hingeBrazo.useSpring = true;
                GameObject.Find("brazo").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                //GameObject.Find("cube").transform.SetParent(GameObject.Find("pega").transform);
                */
                
                if ((Abs(GameObject.Find("brazo").GetComponent<HingeJoint>().angle - posicion[conta]) / velocidad) >= 1)
                {
                    GameObject.Find("brazo").transform.Rotate(new Vector3(0, 0, 1) * velocidad * -1 * ((GameObject.Find("brazo").GetComponent<HingeJoint>().angle - posicion[conta]) / Abs(GameObject.Find("brazo").GetComponent<HingeJoint>().angle - posicion[conta])));
                }
                else
                {
                    GameObject.Find("brazo").transform.Rotate(new Vector3(0, 0, 1) * -1 * ((GameObject.Find("brazo").GetComponent<HingeJoint>().angle - posicion[conta]) / Abs(GameObject.Find("brazo").GetComponent<HingeJoint>().angle - posicion[conta])));
                }
                
            }
            else
            {
                conta++;
                
                if (conta == 3)
                {
                    GameObject.Find("brazo").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
                    GameObject.Find("brazo").GetComponent<HingeJoint>().useSpring = false;
                    GameObject.Find("brazo").GetComponent<HingeJoint>().useMotor = true;
                    setModoControl("rotacion");
                    //GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
                    //GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "Rotation";
                    ejecucion = "codo";
                    conta = 0;
                    brazo = true;
                }
            }

        }
        if (ejecucion == "codo")
        {
            Debug.Log(Mathf.Round(GameObject.Find("codo").GetComponent<Transform>().eulerAngles.z - 180));
            float[] posicion = { -110f, -125f, -120f };
            float angle = GameObject.Find("codo").GetComponent<Transform>().localEulerAngles.z;
            if (angle > 180)
            {
                angle = angle - 360;
            }
            if (Mathf.Round(angle) != posicion[conta]) //guardado
            {
                /*
                HingeJoint hingeCodo = GameObject.Find("codo").GetComponent<HingeJoint>(); //(Mathf.Round(GameObject.Find("codo").GetComponent<HingeJoint>().angle) + Mathf.Round(GameObject.Find("brazo").GetComponent<HingeJoint>().angle)
                hingeCodo.useMotor = false;
                JointSpring springCodo = hingeCodo.spring;
                springCodo.spring = 200;
                springCodo.damper = 100;
                springCodo.targetPosition = posicion[conta];
                hingeCodo.spring = springCodo;
                hingeCodo.useSpring = true;
                GameObject.Find("codo").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GameObject.Find("restoMano").GetComponent<HingeJoint>().useLimits = false;//guardado
                //GameObject.Find("Cube").transform.SetParent(GameObject.Find("pega").transform);
                */

                if ((Abs(Mathf.Round(angle) - posicion[conta]) / velocidad) >= 1)
                {
                    GameObject.Find("codo").transform.Rotate(new Vector3(0, 0, 1) * velocidad * -1 * ((Mathf.Round(angle) - posicion[conta]) / Abs(Mathf.Round(angle) - posicion[conta])));
                }
                else
                {
                    GameObject.Find("codo").transform.Rotate(new Vector3(0, 0, 1) * -1 * ((Mathf.Round(angle) - posicion[conta]) / Abs(Mathf.Round(angle) - posicion[conta])));
                }
                
            }
            else
            {
                conta++;
                
                if (conta == 3)
                {
                    setModoControl("rotacion");
                    GameObject.Find("codo").GetComponent<HingeJoint>().useSpring = false;
                    GameObject.Find("codo").GetComponent<HingeJoint>().useMotor = true;
                    GameObject.Find("codo").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
                    //GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
                    //GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "Rotation";
                    ejecucion = "mano";
                    conta = 0;
                }
            }

        }
        if (ejecucion == "mano")
        {
            float[] posicion = { -50f, -11f, -30f };
            float pos = GameObject.Find("restoMano").GetComponent<Transform>().localEulerAngles.y;
            if (pos > 180)
            {
                pos = pos - 360;
            }
            Debug.Log(Mathf.Round(pos));
            if (Mathf.Round((pos)) != posicion[conta])
            {
                /*
                HingeJoint hingeMano = GameObject.Find("restoMano").GetComponent<HingeJoint>();
                hingeMano.useMotor = false;
                JointSpring springMano = hingeMano.spring;
                springMano.spring = 10;
                springMano.damper = 10;
                springMano.targetPosition = posicion[conta];
                hingeMano.spring = springMano;
                hingeMano.useSpring = true;
                GameObject.Find("restoMano").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                //GameObject.Find("Movimiento").transform.SetParent(GameObject.Find("ManoGeneral").transform);
                //GameObject.Find("Cube").transform.SetParent(GameObject.Find("Vacío").transform);
                //GameObject.Find("Cube (1)").GetComponent<LookAt>().enabled = false;
                */
                
                if ((Abs((Mathf.Round((pos))) - posicion[conta]) / velocidad) >= 1)
                {
                    GameObject.Find("restoMano").transform.Rotate(new Vector3(-1, 0, 0) * velocidad * -1 * (((Mathf.Round((pos))) - posicion[conta]) / Abs((Mathf.Round((pos))) - posicion[conta])));
                    //GameObject.Find("Mano").transform.Rotate(new Vector3(0, 1, 0) * velocidad * -1 * ((GameObject.Find("ManoGeneral").GetComponent<choquePieza>().getAngulo() - posicion[conta]) / Abs(GameObject.Find("ManoGeneral").GetComponent<choquePieza>().getAngulo() - posicion[conta])));
                    
                }
                else
                {
                    GameObject.Find("restoMano").transform.Rotate(new Vector3(-1, 0, 0) * -1 * (((Mathf.Round((pos))) - posicion[conta]) / Abs((Mathf.Round((pos))) - posicion[conta])));
                    //GameObject.Find("Mano").transform.Rotate(new Vector3(0, 1, 0) * -1 * ((GameObject.Find("ManoGeneral").GetComponent<choquePieza>().getAngulo() - posicion[conta]) / Abs(GameObject.Find("ManoGeneral").GetComponent<choquePieza>().getAngulo() - posicion[conta])));
                    
                }
                
            }
            else
            {
                conta++;
                Debug.Log(conta);
                if (conta == 3)
                {
                    setModoControl("rotacion");
                    GameObject.Find("restoMano").GetComponent<HingeJoint>().useSpring = false;
                    GameObject.Find("restoMano").GetComponent<HingeJoint>().useMotor = true;
                    GameObject.Find("restoMano").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
                    //GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
                    //GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "Rotation";
                    ejecucion = "muñeca";
                    conta = 0;
                }
            }

        }
        if (ejecucion == "muñeca")
        {
            Debug.Log(Mathf.Round(GameObject.Find("Mano360").GetComponent<Transform>().localEulerAngles.z) - 180 - 45);
            float[] posicion = { 78f, 59f, 65f };//no cambiar
            if (Mathf.Round(GameObject.Find("Mano360").GetComponent<Transform>().localEulerAngles.z) - 180 - 45 != Mathf.Round(posicion[conta]))
            {
                
                /*
                HingeJoint hingeMano360 = GameObject.Find("Mano360").GetComponent<HingeJoint>();
                hingeMano360.useMotor = false;
                JointSpring springMano360 = hingeMano360.spring;
                springMano360.spring = 50;
                springMano360.damper = 50;
                springMano360.targetPosition = posicion[conta];
                hingeMano360.spring = springMano360;
                hingeMano360.useSpring = true;
                GameObject.Find("Mano360").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                */
                
                //GameObject.Find("Cube").transform.SetParent(GameObject.Find("pega").transform);
                if ((Abs((Mathf.Round(GameObject.Find("Mano360").GetComponent<Transform>().localEulerAngles.z) - 180 - 45) - posicion[conta]) / velocidad) >= 1)
                {
                    GameObject.Find("Mano360").transform.Rotate(new Vector3(0, 0, 1) * velocidad * -1 * (((Mathf.Round(GameObject.Find("Mano360").GetComponent<Transform>().localEulerAngles.z) - 180 - 45) - posicion[conta]) / Abs((Mathf.Round(GameObject.Find("Mano360").GetComponent<Transform>().localEulerAngles.z) - 180 - 45) - posicion[conta])));
                }
                else
                {
                    GameObject.Find("Mano360").transform.Rotate(new Vector3(0, 0, 1) * -1 * (((Mathf.Round(GameObject.Find("Mano360").GetComponent<Transform>().localEulerAngles.z) - 180 - 45) - posicion[conta]) / Abs((Mathf.Round(GameObject.Find("Mano360").GetComponent<Transform>().localEulerAngles.z) - 180 - 45) - posicion[conta])));
                }
                
            }
            else
            {
                conta++;
                //GameObject.Find("Mano360").GetComponent<HingeJoint>().useLimits = false;
                
                if (conta == 3)
                {
                    setModoControl("rotacion");
                    //Debug.Log("run listo");
                    //GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
                    //GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "Rotation";
                    ejecucion = "base";
                    conta = 0;
                }
            }
        }
        if (ejecucion == "base")
        {
            float[] posicion = { 10f, 50f, 30f }; //validar angulos
            if (Mathf.Round(GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle) != posicion[conta])
            {
                /*
                HingeJoint hingeBase = GameObject.Find("baseGiro").GetComponent<HingeJoint>();
                hingeBase.useMotor = false;
                JointSpring springBase = hingeBase.spring;
                springBase.spring = 10;
                springBase.damper = 10;
                springBase.targetPosition = posicion[conta];
                hingeBase.spring = springBase;
                hingeBase.useSpring = true;
                GameObject.Find("baseGiro").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                //GameObject.Find("Cube").transform.SetParent(GameObject.Find("pega").transform);
                */
                
                if ((Abs(GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle - posicion[conta]) / velocidad) >= 1)
                {
                    GameObject.Find("baseGiro").transform.Rotate(new Vector3(0, 0, 1) * velocidad * -1 * ((GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle - posicion[conta]) / Abs(GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle - posicion[conta])));
                }
                else
                {
                    GameObject.Find("baseGiro").transform.Rotate(new Vector3(0, 0, 1) * -1 * ((GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle - posicion[conta]) / Abs(GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle - posicion[conta])));
                }
                
            }
            else
            {
                conta++;
                if (conta == 3)
                {
                    setModoControl("rotacion");
                    GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
                    GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "Rotation";
                    ejecucion = "no";
                    conta = 0;
                }
            }

        }
    }

    public void cambiar_Speed()
    {
        if (int.Parse(numero) >= 0 && int.Parse(numero) <= 100)
        {
            velocidad = int.Parse(numero);
        }
        else if (int.Parse(numero) > 100)
        {
            GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "Velocidad máxima 100";
            setModoControl("rotacion");
            GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "Rotation";
            GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
        }
        GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "Velocidad cambiada";
        GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
        GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Velocidad cambiada a " + velocidad;
        setModoControl("rotacion");
        GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "Rotation";
        GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
    }

    public void guardar_posiciones()
    {
        float[] lista_aux = new float[7];
        float[] lista_a = new float[3];
        if (int.Parse(numero) != 0)
        { // no incluye el 0 por la posicion inicial 0
            
            lista_aux[0] = Mathf.Round(GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle);
            Debug.Log(GameObject.Find("baseGiro").GetComponent<HingeJoint>().angle);

            lista_aux[1] = Mathf.Round(GameObject.Find("brazo").GetComponent<Transform>().eulerAngles.z - 180);
            Debug.Log(Mathf.Round(GameObject.Find("brazo").GetComponent<Transform>().eulerAngles.z - 180));

            float angle = GameObject.Find("codo").GetComponent<Transform>().localEulerAngles.z;
            if (angle > 180)
            {
                angle = angle - 360;
            }
            lista_aux[2] = Mathf.Round(angle);
            Debug.Log(Mathf.Round(angle));

            float angle2 = GameObject.Find("restoMano").GetComponent<Transform>().localEulerAngles.y;
            if (angle2 > 180)
            {
                angle2 = angle2 - 360;
            }
            lista_aux[3] = Mathf.Round(angle2);
            Debug.Log(Mathf.Round(angle2));

            float angle3 = GameObject.Find("Mano360").GetComponent<Transform>().localEulerAngles.z;
            if (angle3 > 180)
            {
                angle3 = angle3 - 360;
            }
            lista_aux[4] = Mathf.Round(angle3);
            Debug.Log(Mathf.Round(angle3));

            posiciones[int.Parse(numero)] = lista_aux;
            
            //coordenadas
            lista_a[0] = GameObject.Find("Movimiento").transform.position.x;
            lista_a[1] = GameObject.Find("Movimiento").transform.position.y;
            lista_a[2] = GameObject.Find("Movimiento").transform.position.z;
            Debug.Log(lista_a[0]);
            Debug.Log(lista_a[1]);
            Debug.Log(lista_a[2]);
            posicionXYZ[int.Parse(numero)] = lista_a;
            GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "posicion guardada en " + numero;
            GameObject.Find("PanelError").GetComponent<UnityEngine.UI.Image>().enabled = true;
            GameObject.Find("Error").GetComponent<UnityEngine.UI.Text>().text = "Posicion " + numero + " guardada";

            //archivo
            string cadena = numero + "|" + lista_aux[0]+ "|" + lista_aux[1] + "|" + lista_aux[2] + "|" + lista_aux[3] + "|" + lista_aux[4] + "|" + lista_a[0] + "|" + lista_a[1] + "|" + lista_a[2];

            DatosGuardados.Add(cadena);
            guardarArchivos();

            
            

        }
        else
        {
            GameObject.Find("Mensajes").GetComponent<UnityEngine.UI.Text>().text = "No puede cambiar la posicion inicial del robot";
        }

        setModoControl("rotacion");
        GameObject.Find("Tipo").GetComponent<UnityEngine.UI.Text>().text = "Rotation";
        GameObject.Find("Numeros").GetComponent<UnityEngine.UI.Text>().text = numero;
        int i = 0;
        GameObject.Find("Mensajesss").GetComponent<UnityEngine.UI.Text>().text = "      Posicion| Angulos Joints | coord. XYZ\r\n   ";
        for (i = 0; i < posiciones.Length; i++)
        {
            if (posiciones[i] != null)
            {
                string cadena2 = i + " | " + posiciones[i][0] + ", " + posiciones[i][1] + ", " + posiciones[i][2] + ", " + posiciones[i][3] + ", " + posiciones[i][4] + " | " + (Math.Truncate(posicionXYZ[i][0] * 100) / 100) + ", " + (Math.Truncate(posicionXYZ[i][1] * 100) / 100) + ", " + (Math.Truncate(posicionXYZ[i][2] * 100) / 100);

                GameObject.Find("Mensajesss").GetComponent<UnityEngine.UI.Text>().text = GameObject.Find("Mensajesss").GetComponent<UnityEngine.UI.Text>().text + "\n" + "  " + cadena2;
            }

        }
    }

    public void guardarArchivos()
    {
        TextWriter escribir = new StreamWriter("Datos.txt");
        escribir.Close();

        foreach (string cad in DatosGuardados)
        {
            StreamWriter agregar = File.AppendText("Datos.txt");
            agregar.WriteLine(cad);
            agregar.Close();
        }
    }

    public void cargarArchivo()
    {
        try
        {
            StreamReader leer = new StreamReader("Datos.txt");
            string aux;
            string linea;
            string angulo0;
            string angulo1;
            string angulo2;
            string angulo3;
            string angulo4;
            string X;
            string y;
            string z;
            string numero;
            int pos = 0;
            
            int[] listapos = { 0, 0, 0,0,0,0,0 ,0,0,0,0};

            while ((linea = leer.ReadLine()) != null)
            {
                DatosGuardados.Add(linea);
                int i = 0;
                for(pos = 0; pos < linea.Length; pos++)
                {
                    if (linea[pos] == '|')
                    {
                        listapos[i] = pos;
                        Debug.Log(listapos[i]);
                        i++;
                    }
                }
                aux = linea.Substring(0, listapos[0]);
                numero = aux;
                //pos . largo palabra
                aux = linea.Substring(listapos[0] + 1 , (listapos[1] - (listapos[0] + 1)));
                angulo0 = aux;

                aux = linea.Substring(listapos[1] + 1 , (listapos[2] - (listapos[1] + 1)));
                angulo1 = aux;

                aux = linea.Substring(listapos[2] + 1, (listapos[3] - (listapos[2] + 1)));
                angulo2 = aux;

                aux = linea.Substring(listapos[3] + 1, (listapos[4] - (listapos[3] + 1)));
                angulo3 = aux;

                aux = linea.Substring(listapos[4] + 1, (listapos[5] - (listapos[4] + 1)));
                angulo4 = aux;

                aux = linea.Substring(listapos[5] + 1, (listapos[6] - (listapos[5] + 1)));
                X = aux;

                aux = linea.Substring(listapos[6] + 1, (listapos[7] - (listapos[6] + 1)));
                y = aux;

                linea = linea.Substring(listapos[7] + 1);
                z = linea;
                
                float[] lista_aux = {float.Parse(angulo0), float.Parse(angulo1), float.Parse(angulo2), float.Parse(angulo3), float.Parse(angulo4)};
                posiciones[int.Parse(numero)] = lista_aux;
                
                float[] lista_a = {float.Parse(X),float.Parse(y),float.Parse(z)};
                posicionXYZ[int.Parse(numero)] = lista_a;

                

            }
            leer.Close();
        }catch (Exception e)
        {
            Console.WriteLine(e);
            Debug.Log(e);
        }

        for (int i = 0; i < posiciones.Length; i++)
        {
            if (posiciones[i] != null)
            {
                string cadena2 = i + " | " + posiciones[i][0] + ", " + posiciones[i][1] + ", " + posiciones[i][2] + ", " + posiciones[i][3] + ", " + posiciones[i][4] + " | " + (Math.Truncate(posicionXYZ[i][0] * 100) / 100) + ", " + (Math.Truncate(posicionXYZ[i][1] * 100) / 100) + ", " + (Math.Truncate(posicionXYZ[i][2] * 100) / 100);

                GameObject.Find("Mensajesss").GetComponent<UnityEngine.UI.Text>().text = GameObject.Find("Mensajesss").GetComponent<UnityEngine.UI.Text>().text + "\n" + "  " + cadena2;
            }

        }
    }

    public void cargar_posiciones()
    {
        if (Movimiento == "articulado")
        {
            numero_lista = "0";
            ejecucion = "posicion";
            bases = false;
            brazos = false;
            codos = false;
            mano = false;
            mano360 = false;
        }
        else
        {
            numero_lista = "0";
            ejecucion = "PosicionXYZ";
        }
    }

    public void run_posicion()
    {
        numero_lista = "0";
        ejecucion = "brazo";

    }

}
