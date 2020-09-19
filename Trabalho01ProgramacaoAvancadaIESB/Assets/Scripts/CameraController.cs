using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothMovingSpeed;
    [SerializeField] private float smoothRotationSpeed;
    [SerializeField] private float maxZoomIn;
    [SerializeField] private float maxZoomOut;
    [SerializeField] private float zoomSpeed;
    private Camera camera;
    private Transform cameraTrasform;
    private float defaultZoom;

    private Vector3 lastMousePosition;
    private Quaternion cameraRotation;

    private void Awake()
    {
        cameraTrasform = this.GetComponentInChildren<Camera>().transform;
        camera = this.GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        defaultZoom = camera.fieldOfView;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //camera.LookAt(target);

        if ((Input.mouseScrollDelta.y > 0) && (camera.fieldOfView > maxZoomIn))
        {
            camera.fieldOfView -= zoomSpeed;
        }

        if ((Input.mouseScrollDelta.y < 0) && (camera.fieldOfView < maxZoomOut))
        {
            camera.fieldOfView += zoomSpeed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = lastMousePosition - camera.ScreenToViewportPoint(Input.mousePosition);

            cameraRotation *= Quaternion.Euler(Vector3.up * direction.x);

            lastMousePosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }

        //this.transform.position = target.position;
        this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position, smoothMovingSpeed * Time.deltaTime);
        //this.transform.rotation = target.rotation;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, target.transform.rotation, smoothRotationSpeed * Time.deltaTime);
    }
}