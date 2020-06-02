using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float followSpeed = 3;
    public float mouseSpeed = 2;

    public Transform target;
    public Transform pivot;
    public Transform camTrans;
    public Transform mTransform;

    float turnSmoothing = 0.1f;
    public float minAngle = -35;
    public float maxAngle = 35;

    float smoothX;
    float smoothY;
    float smoothXvelocity;
    float smoothYvelocity;
    public float lookAngle;
    public float tiltAngle;

    private Vector3 velocity = Vector3.zero;


    public void Start()
    {
        mTransform = this.transform;
    }

    public void Update()
    {
        //HandleRotation();
    }

    private void LateUpdate()
    {
        FollowTarget();
        if (Input.GetMouseButton(0))
        {
            RotateCamera();
        }

        if (Input.GetMouseButton(1))
        {
            PlaceCameraBehindTarget();
        }
    }



    void FollowTarget()
    {
        //float speed = Time.deltaTime * followSpeed;
        //Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, speed);
        //transform.position = targetPosition;

        float smoothTime = 0.3f;
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
    }

    void PlaceCameraBehindTarget()
    {
        //pivot.localRotation = target.rotation;
        float rotationSpeed = 30.0f;
        pivot.rotation = Quaternion.Lerp(pivot.rotation, target.rotation, Time.fixedDeltaTime * rotationSpeed);
        lookAngle = 0.0f;
        tiltAngle = 0.0f;
    }

    void HandleRotation()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        if (turnSmoothing > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXvelocity, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYvelocity, turnSmoothing);
        }
        else
        {
            smoothX = h;
            smoothY = v;
        }

        tiltAngle -= smoothY * mouseSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
    }

    void RotateCamera()
    {
        float h = Input.GetAxis("Mouse X");
        lookAngle += h * mouseSpeed;


        float v = Input.GetAxis("Mouse Y");
        tiltAngle -= v * mouseSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);

        pivot.localRotation = target.rotation;
        pivot.rotation *= Quaternion.Euler(tiltAngle, lookAngle, 0.0f);
    }

}
