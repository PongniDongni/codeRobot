using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    GameObject target;
    public float angulo;
    HingeJoint hinge;
    Rigidbody rb;
    protected GameObject control;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Cube2");
        hinge = GameObject.Find("restoMano").GetComponent<HingeJoint>();
        rb = GameObject.Find("restoMano").GetComponent<Rigidbody>();
        control = GameObject.Find("control");
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

        transform.LookAt(targetPosition);
        float valor = GameObject.Find("Mano360").transform.eulerAngles.z;

        if (GameObject.Find("control").GetComponent<control>().getModoMovimiento() == "coordenadas")
        {
            angulo = this.transform.eulerAngles.x;
            if (angulo > 180)
            {
                angulo = angulo - 360;
            }
            //Debug.Log(angulo); //contrario
            angulo = angulo * -1;

            //GameObject.Find("restoMano").transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 90f);
            //GameObject.Find("restoMano").transform.rotation = this.transform.rotation;
            GameObject.Find("restoMano").transform.LookAt(targetPosition);
            //Debug.Log(GameObject.Find("Cube1").transform.eulerAngles);
        }
    }
}
