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
        // �ƶ�
        pendingVelocity.z = 0;
        cc.Move(pendingVelocity * Time.deltaTime);
        // ���¶���
        animator.SetBool("Grounded", cc.isGrounded);
        animator.SetFloat("Speed", cc.velocity.magnitude);
        // ��������
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

        // ȥ��Y��Ӱ��
        characterPos.y = 0;
        targetPos.y = 0;
        // ��ɫ�泯Ŀ�������
        Vector3 faceToDir = targetPos - characterPos;
        // ��ɫ�泯Ŀ�귽�����Ԫ��
        Quaternion faceToQuat = Quaternion.LookRotation(faceToDir);
        // �����ֵ
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
