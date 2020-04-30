using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrimmer : MonoBehaviour
{

    private AudioSource aud;
    private GameObject Scrim;
    void Start()
    {
        Scrim = GameObject.Find("GameManager").GetComponent<ManagerInGame>().Scrim;
        Scrim.SetActive(false);
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(StartSctrim());

    }
    private IEnumerator StartSctrim()
    {
        aud.Play();
        Scrim.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Scrim.SetActive(false);
        Destroy(gameObject);
    }
}
