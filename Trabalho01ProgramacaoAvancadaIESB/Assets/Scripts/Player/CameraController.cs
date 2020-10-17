using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothMovingSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float smoothRotationSpeed;
    [SerializeField] private float maxZoomIn;
    [SerializeField] private float maxZoomOut;
    [SerializeField] private float zoomSpeed;
    private Camera mainCamera;
    private Transform cameraTrasform;
    private float defaultZoom;

    private Vector3 lastMousePosition;
    private Quaternion cameraRotation;

    private void Awake()
    {
        cameraTrasform = this.GetComponentInChildren<Camera>().transform;
        mainCamera = this.GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        defaultZoom = mainCamera.fieldOfView;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Input.mouseScrollDelta.y > 0) && (mainCamera.fieldOfView > maxZoomIn))
        {
            mainCamera.fieldOfView -= zoomSpeed;
        }

        if ((Input.mouseScrollDelta.y < 0) && (mainCamera.fieldOfView < maxZoomOut))
        {
            mainCamera.fieldOfView += zoomSpeed;
        }

        // if (Input.GetMouseButtonDown(0))
        // {
        //     lastMousePosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        // }

        // if (Input.GetMouseButton(0))
        // {
        //     Vector3 direction = lastMousePosition - mainCamera.ScreenToViewportPoint(Input.mousePosition);

        //     cameraRotation *= Quaternion.Euler(Vector3.up * direction.x);

        //     lastMousePosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        // }

        //this.transform.position = target.position;
        this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position, smoothMovingSpeed * Time.fixedDeltaTime);
        //this.transform.rotation = target.rotation;
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, target.transform.rotation, smoothRotationSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f)
        {
            cameraRotation = Quaternion.Euler(Vector3.up * rotationSpeed * Input.GetAxis("Mouse X"));
        }
        cameraRotation = Quaternion.Euler(Vector3.up * 10);
        this.transform.rotation = cameraRotation * target.transform.rotation;
    }
}