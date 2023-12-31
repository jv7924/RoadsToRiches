using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 previousPosition;
    private bool cameraMove = false;
    [SerializeField] private int speed = 2;
    [SerializeField] private GameObject startPosition;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom; 
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 10;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Rotate Camera
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

        //Reset Camera
        if (cameraMove)
        {
            //cam.transform.position = Vector3.Lerp(cam.transform.position, startPosition.transform.position, speed * Time.deltaTime);
            //cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, startPosition.transform.rotation, speed * Time.deltaTime);
        }

        if (cam.transform.rotation == startPosition.transform.rotation)
        {
            cameraMove = false;
        }

        if (Input.GetKeyDown("r"))
        {
            cam.transform.position = startPosition.transform.position;
            cam.transform.rotation = startPosition.transform.rotation;
            cam.fieldOfView = 60;
            //cameraMove = true;
        }
    }
}
