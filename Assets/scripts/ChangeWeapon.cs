using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
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
            if(Input.GetKeyDown(KeyCode.E)) Destroy(gameObject);

        }
    }
    public void OnTriggerExit(Collider other)
    {
        E.gameObject.SetActive(false);
    }
}
