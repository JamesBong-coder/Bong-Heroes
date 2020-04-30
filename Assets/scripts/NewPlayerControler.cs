using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerControler : MonoBehaviour
{
    public float Speed = 5;
    public float gravity = 20;
    private Vector3 moveDir = Vector3.zero;
    private CharacterController CharControl;

    void Start()
    {
        CharControl = GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDir = transform.TransformDirection(moveDir);
        moveDir *= Speed;

        moveDir.y -= gravity * Time.deltaTime;
        CharControl.Move(moveDir*Time.deltaTime);
    }
}
