using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    Character character;

    float lastCheckStateTime = 0f;
    float simulateInputX;
    bool simulateJump;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastCheckStateTime + 2)
		{
            lastCheckStateTime = Time.time;
            simulateInputX = Random.Range(-1f, 1f);
            simulateJump = Random.Range(0, 2) == 1 ? true : false;
        }

        MoveControll(simulateInputX);
        JumpControll(simulateJump);
    }

    void MoveControll(float inputX)
	{
        character.Move(inputX);
        if(inputX != 0)
		{
            var dir = Vector3.right * inputX;
            character.Rotate(dir, 10);
		}
	} 

    void JumpControll(bool jump)
	{
        if (jump)
		{
            simulateJump = false;
            character.Jump();
        }

    }
}
