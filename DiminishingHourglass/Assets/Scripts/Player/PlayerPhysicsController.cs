using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsController : MonoBehaviour
{

    [System.Serializable]
    public class MoveSettings
    {
        public float runningVel;
        public float jumpVel;

        public LayerMask groundLayers;
        public LayerMask waterLayers;
        public LayerMask enemyLayers;

        public void Setup()
        {
            groundLayers = LayerMask.GetMask("Platform");
            waterLayers = LayerMask.GetMask("Water");
            enemyLayers = LayerMask.GetMask("Enemy");
            jumpVel = 10f;
            runningVel = 8f;
        }
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = 0.75f;
    }

    public MoveSettings moveSettings = new MoveSettings();
    public PhysSettings physSettings = new PhysSettings();

    Vector3 velocity = Vector3.zero;
    Rigidbody playerRigidbody;
    private SphereCollider col = null;
    bool isGrounded;
    bool isFloating;
    bool isStandingOnEnemy;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(col.bounds.center, col.radius, moveSettings.groundLayers);

    }

    private bool IsFloating()
    {
        return Physics.CheckSphere(col.bounds.center, col.radius, moveSettings.waterLayers);
    }

    private bool IsStandingOnEnemy()
    {
        return Physics.CheckSphere(col.bounds.center, col.radius, moveSettings.enemyLayers);
    }

    private void Jumping()
    {
        isGrounded = IsGrounded();
        isFloating = IsFloating();
        isStandingOnEnemy = IsStandingOnEnemy();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded && !isFloating)
            {
                velocity.y = moveSettings.jumpVel;
            }
            else if (isGrounded && isFloating)
            {
                velocity.y = moveSettings.jumpVel;
            }
            else if (!isGrounded && isFloating)
            {
                velocity.y = moveSettings.jumpVel;
            }
            else if (isStandingOnEnemy)
            {
                velocity.y = moveSettings.jumpVel;
            }
            else
            {
                velocity.y -= physSettings.downAccel;
            }


        }
        if (!Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.Space))
        {
            if (isGrounded || isFloating)
            {
                velocity.y = -1;
            }
            else if (isStandingOnEnemy)
            {
                velocity.y = -1;
            }
            else
            {
                velocity.y -= physSettings.downAccel;
            }
        }

    }

}
