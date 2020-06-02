using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform mTransform;
    public Rigidbody rigid;

    public Animator anim;

    public bool onGround = true;

    //Input
    float vertical = 0.0f;
    float horizontal = 0.0f;
    //Movement
    Vector3 direction = Vector3.zero;
    public float movementSpeed = 3.0f;
    //Rotation
    public float mouseSpeed = 2;
    public float lookAngle;
    public float tiltAngle;

    void Start()
    {
        mTransform = this.transform;

        rigid = GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (anim == null)
            anim = GetComponentInChildren<Animator>();
        anim.applyRootMotion = false;
    }

    private void Update()
    {
        updateInput();
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    void updateInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetMouseButton(1))
        {
            RotatePlayer();
        }
    }

    void UpdateMovement()
    {
        direction = transform.forward * vertical + transform.right * horizontal;
        Vector3 velocity = direction;
        velocity.Normalize();

        velocity = velocity * movementSpeed;
        //velocity.y = rigid.velocity.y;

        rigid.velocity = velocity;

    }

    private void RotatePlayer()
    {
        float h = Input.GetAxis("Mouse X");

        lookAngle += h * mouseSpeed;
        transform.rotation = Quaternion.Euler(0, lookAngle, 0);
    }

    void UpdateAnimator()
    {
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);
    }

}
