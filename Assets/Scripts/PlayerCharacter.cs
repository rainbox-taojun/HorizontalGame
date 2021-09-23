using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
	public Transform grabSocket;
	public Transform grabObject;
	public int armsAnimatorLayer = 1;

	private void Start()
	{
		animator.SetLayerWeight(armsAnimatorLayer, 1f);
	}

	protected override void Update()
	{
		animator.SetBool("Grab", grabObject ? true : false);
		base.Update();
	}

	public void GrabCheck()
	{
		if (grabObject != null && rotateComplete)
		{
			// 如果有抓取物且转向完成
			grabObject.transform.SetParent(null);
			grabObject.GetComponent<Rigidbody>().isKinematic = false;
			grabObject = null;
		}
		else
		{
			// 如果没有抓取物
			var dist = cc.radius;

			RaycastHit hit;

			if (Physics.Raycast(transform.position, transform.forward, out hit, dist + 1f))
			{
				if (hit.collider.CompareTag("GrabBox"))
				{
					grabObject = hit.transform;
					grabObject.SetParent(grabSocket);
					grabObject.localPosition = Vector3.zero;
					grabObject.localRotation = Quaternion.identity;
					grabObject.GetComponent<Rigidbody>().isKinematic = true;
				}
			}
		}
	}
}
