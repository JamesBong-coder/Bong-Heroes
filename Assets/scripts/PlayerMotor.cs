using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    public Camera cam;
    public Light lig;

    private Rigidbody rb;

    private Vector3 velocity = Vector3.zero;
    private Vector3 Xrotation = Vector3.zero;
    private Vector3 Yrotation = Vector3.zero;

    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        PerformMove();
        PerformRotate();
	}

    public void Move(Vector3 vel)
    {
        velocity = vel;
    }
    public void RotateX(Vector3 rot)
    {
        Xrotation = rot;
    }
    public void RotateY(Vector3 rot)
    {
        Yrotation = rot;
    }

    public void PerformMove()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
           // GetComponent<Animator>().SetTrigger("Скелет|БЕГ");
        }
        
    }
    public void PerformRotate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Yrotation));
        if (cam != null)
        {
            if(cam.transform.rotation.x<72.5f && cam.transform.rotation.x>-40)
            cam.transform.Rotate(-Xrotation);
            
        }
        if (lig != null)
        {
            if (lig.transform.rotation.x < 72.5f && lig.transform.rotation.x > -40)
                lig.transform.Rotate(-Xrotation);

        }
    }

}
