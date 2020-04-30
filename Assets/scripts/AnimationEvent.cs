using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public Animator anim;
    public GameObject Enemy;
    private GameObject Player;
    private float AttackDistance;

    public void Start()
    {
        Player = GameObject.Find("Player");
        AttackDistance = Enemy.GetComponent<Enimies>().AttackDistance;
    }

    public void StopAnimation()
    {
        anim.enabled = false;
        Destroy(this);
    }

    public void Damage()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= (AttackDistance + 0.5f))
            Enemy.GetComponent<Enimies>().Attack();
    }
}
