using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCharacter character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");

        character.Move(h);

        if(h != 0)
		{
            var dir = Vector3.right * h;
            character.Rotate(dir, 10);
        }

        if (Input.GetKeyDown(KeyCode.Space))
		{
            character.Jump();
		}

        if (Input.GetKeyDown(KeyCode.G))
		{
            character.GrabCheck();
        }
    }
}
