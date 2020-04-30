using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public static PlayerController Instance { get; set; }

    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    public float lookSpeed = 3f;

    public static string UserName;


    private PlayerMotor motor;

	void Start () {
        motor = GetComponent<PlayerMotor>();
        Debug.Log(UserName);
	}



    void Update () {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
    
        //if( xMov==0 && zMov==0) GetComponent<Animator>().



  

        Vector3 movHor = transform.right * xMov;
        Vector3 movVer = transform.forward * zMov;

        Vector3 velocity = (movVer + movHor).normalized * speed;

       

        motor.Move(velocity);

        float yRot = Input.GetAxisRaw("Mouse X");      
        Vector3 Yrotation = new Vector3( 0f, yRot, 0f) * lookSpeed;
        motor.RotateY(Yrotation);

        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 Xrotation = new Vector3( xRot, 0f, 0f) * lookSpeed;
        motor.RotateX(Xrotation);
    }
}
