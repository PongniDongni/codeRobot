using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    private GameObject camara;
    private GameObject control;
    private float Speed = 5f;
    private float rotationX = 0f;
    private Vector3 lastMousePosition;
    private float xMin = -9f;
    private float xMax = 18f;
    private float yMin = 1f;
    private float yMax = 9f;
    private float zMin = -17f;
    private float zMax = 10f;
    private bool limite = false;
    private float excludeXMin = -3.4f;
    private float excludeYMin = 1f;
    private float excludeZMin = -7f;
    private float excludeXMax = 4f;
    private float excludeYMax = 7f;
    private float excludeZMax = 2.13f;

    // Start is called before the first frame update
    void Start()
    {
        camara = GameObject.Find("Main Camera");
        control = GameObject.Find("control");

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            float rotationY = Input.GetAxis("Mouse X") * Speed;
            rotationX -= Input.GetAxis("Mouse Y") * Speed;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            camara.transform.eulerAngles = new Vector3(rotationX, transform.eulerAngles.y + rotationY, 0f);


        }

        float horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        camara.transform.Translate(new Vector3(horizontal, vertical, 0f));

        float zoomAmount = Input.GetAxis("Mouse ScrollWheel") * Speed;
        camara.transform.Translate(new Vector3(0f, 0f, zoomAmount));

        float clampedX = Mathf.Clamp(camara.transform.position.x, xMin, xMax);
        float clampedY = Mathf.Clamp(camara.transform.position.y, yMin, yMax);
        float clampedZ = Mathf.Clamp(camara.transform.position.z, zMin, zMax);

        Vector3 clampedPosition = new Vector3(clampedX, clampedY, clampedZ);

        // Asignar la posición limitada a la cámara
        camara.transform.position = clampedPosition;


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cubo")
        {
            Speed = 0f;
        }
       
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cubo")
        {
            Speed = 5f;
        }
    }
 }
