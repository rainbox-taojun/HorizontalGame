using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected CharacterController cc;
    protected Animator animator;
    protected bool rotateComplete = true;
    
    Vector3 pendingVelocity;

    public float runSpeed;
    public float jumpPower;
    public int damage = 100;
    public int health = 99;
    public GameObject deathFX;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        // 移动
        pendingVelocity.z = 0;
        cc.Move(pendingVelocity * Time.deltaTime);
        // 更新动画
        animator.SetBool("Grounded", cc.isGrounded);
        animator.SetFloat("Speed", cc.velocity.magnitude);
        // 更新重力
        pendingVelocity.y += cc.isGrounded ? 0f : Physics.gravity.y * 10f * Time.deltaTime;

        AttackCheck();
    }

    public void Move(float inputX)
	{
        Debug.Log(inputX);
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
        rotateComplete = false;
        var targetPos = transform.position + lookDir;
        var characterPos = transform.position;

        // 去除Y轴影响
        characterPos.y = 0;
        targetPos.y = 0;
        // 角色面朝目标的向量
        Vector3 faceToDir = targetPos - characterPos;
        // 角色面朝目标方向的四元数
        Quaternion faceToQuat = Quaternion.LookRotation(faceToDir);
        // 球面插值
        Quaternion slerp = Quaternion.Slerp(transform.rotation, faceToQuat, turnSpeed * Time.deltaTime);

        if (slerp == faceToQuat)
		{
            rotateComplete = true;
		}
        transform.rotation = slerp;
    }

    public void Death()
	{
        var fx = Instantiate(deathFX, transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(fx, 2);
        Destroy(gameObject);

    }

    public void TakeDamage(Character inflicter, int damage)
	{
        inflicter.Jump();
        health -= damage;

        if (health <= 0)
		{
            Death();
		}
	}

    public void AttackCheck()
	{
        var dist = cc.height / 2;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, dist + 0.05f))
		{
            if (hit.transform.GetComponent<Character>() && hit.transform != transform)
			{
                hit.transform.GetComponent<Character>().TakeDamage(this, damage);

            }
		}
	}
}
