using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 previousPosition;
    [SerializeField] private int speed;
    [SerializeField] private GameObject startPosition;
    [SerializeField] private Camera cam;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Rotate Camera
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.Rotate(new Vector3(1,0,0), direction.y*180);
            cam.transform.Rotate(new Vector3(0,1,0), -direction.x*180, Space.World);
            
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        //WASD Movement
        Vector3 inputDir = new Vector3(0,0,0);
        if (Input.GetKey(KeyCode.W)) inputDir.z += 1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z -= 1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x -= 1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x += 1f;
        if (Input.GetKey(KeyCode.Space)) inputDir.y += 1f;
        if (Input.GetKey(KeyCode.LeftControl)) inputDir.y -= 1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x + transform.up * inputDir.y;
        transform.position += moveDir * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.P)) Debug.Log(cam.transform.rotation.eulerAngles.y);

        if (Input.GetKeyDown(KeyCode.Z) && speed != 10) speed += 1;
        if (Input.GetKeyDown(KeyCode.X) && speed != 1) speed -= 1;

        //Reset Camera
        if (Input.GetKeyDown("r"))
        {
            cam.transform.position = startPosition.transform.position;
            cam.transform.rotation = startPosition.transform.rotation;
        }
    }
}
