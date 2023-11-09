using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 previousPosition;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom; 
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 10;
    //[SerializeField] private float sensitivity = .5f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Mouse2))
        {
            turn.x += Input.GetAxis("Mouse X") * sensitivity;
            turn.y += Input.GetAxis("Mouse Y") * sensitivity;
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        }
        */

        cam.transform.position = target.position;

        if (Input.GetMouseButtonDown(2))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.Rotate(new Vector3(1,0,0), direction.y*180);
            cam.transform.Rotate(new Vector3(0,1,0), -direction.x*180, Space.World);
            
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        
        cam.transform.Translate(new Vector3(0,0,-distanceToTarget));

        //Zoom In
        if (Input.GetAxis("Mouse ScrollWheel")>0 && (cam.fieldOfView > maxZoom))
        {
            cam.fieldOfView--;
        }

        //Zoom Out
        if (Input.GetAxis("Mouse ScrollWheel")<0 && (cam.fieldOfView < minZoom))
        {
            cam.fieldOfView++;
        }
    }
}
