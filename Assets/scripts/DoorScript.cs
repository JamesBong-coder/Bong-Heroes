using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Rigidbody rigid;
    public AudioSource audSour;
    public int move;


    public void Start()
    {
        move = 0;
        rigid = GetComponent<Rigidbody>();
        audSour = GetComponent<AudioSource>();
        
    }
    public void Update()
    {
        if ((rigid.worldCenterOfMass.y > 12f && move ==1) || (rigid.worldCenterOfMass.y < 4.16f && move ==-1))
        {
            rigid.velocity = new Vector3(0, 0, 0);           
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audSour.Play();
            rigid.velocity = new Vector3(0, 2f, 0);
            move = 1;
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            audSour.Play();
            rigid.velocity = new Vector3(0, -2f, 0);
            move = -1;
        }
    }
}
