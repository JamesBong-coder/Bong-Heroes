using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public int Damage = 5;
    private float timeDestroy;

    void Start()
    {
        timeDestroy = Time.time + 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDestroy < Time.time)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Enimies>().Health -= 5;
            Destroy(gameObject);
        }
    }
}
