using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected CharacterController cc;
    protected Animator animator;

    Vector3 pendingVelocity;

    public float runSpeed;
    public float jumpPower;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ÒÆ¶¯
        pendingVelocity.z = 0;
        cc.Move(pendingVelocity);
    }

    public void Move(float inputX)
	{
        pendingVelocity.x = inputX * runSpeed;
    }

    public void Jump()
	{
        if (cc.isGrounded)
		{
            pendingVelocity.y = jumpPower;
        }
	}

    public void Rotate(Vector3 lookDir, float turnSpeed)
	{

	}

    public void Death()
	{

	}

    public void TakeDamage(Character inflicter, int damage)
	{

	}

    public void AttackCheck()
	{

	}
}
