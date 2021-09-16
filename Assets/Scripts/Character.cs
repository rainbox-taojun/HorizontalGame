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

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // �ƶ�
        pendingVelocity.z = 0;
        cc.Move(pendingVelocity * Time.deltaTime);
        // ���¶���
        animator.SetBool("Grounded", cc.isGrounded);
        animator.SetFloat("Speed", cc.velocity.magnitude);
        // ��������
        pendingVelocity.y += cc.isGrounded ? 0f : Physics.gravity.y * 10f * Time.deltaTime;
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

	}

    public void TakeDamage(Character inflicter, int damage)
	{

	}

    public void AttackCheck()
	{

	}
}
