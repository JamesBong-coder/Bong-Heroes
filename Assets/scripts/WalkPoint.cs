using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkPoint : MonoBehaviour
{
    public int WalkPoints;
    public GameObject WalkPointPrefab;
    public Text WalkPointText;
    private float NextSpawn;
    

    private void Start()
    {
        NextSpawn = Time.time;
        WalkPointText.text = Convert.ToString(WalkPoints);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (WalkPoints > 0)
            {
                if (NextSpawn < Time.time)
                {
                    Instantiate(WalkPointPrefab, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
                    WalkPoints--;
                    WalkPointText.text = Convert.ToString(WalkPoints);
                    NextSpawn = Time.time + 5;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if(other.tag == "WalkPoint")
            {
                WalkPoints += 3;
                WalkPointText.text = Convert.ToString(WalkPoints);

            }
        }
    }
}
