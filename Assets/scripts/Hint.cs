using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public GameObject E;

    public void Start()
    {
        E.gameObject.SetActive(false);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            E.gameObject.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        E.gameObject.SetActive(false);
    }
}
