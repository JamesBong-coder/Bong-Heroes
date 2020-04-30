using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enimies : MonoBehaviour
{
    [HideInInspector]
    public int MaxHealth;
    public int Health = 10;
    public string EnemyName;

    public Animator anim;
    private GameObject Player;

    public float seeDistance;
    public float AttackDistance;
    public float speed;
    public int Damage;
    public float Delay;
    private float NextKick;

    public AudioClip[] clips;
    private AudioSource Aud;
    private bool Dead;

    void Start()
    {
        Dead = false;
        NextKick = Time.time;
        MaxHealth = Health;
        //anim = gameObject.GetComponent<Animator>();
        Aud = gameObject.GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, Player.transform.position) < seeDistance && Health > 0)
        {
            if(Vector3.Distance(transform.position, Player.transform.position) > AttackDistance)
            {
                transform.LookAt(Player.transform);
                transform.Translate(0, 0, speed * Time.deltaTime);
                Aud.clip = clips[0];
                Aud.PlayOneShot(clips[0]);
                anim.SetTrigger("Walk");
            }
            else
            {
                if (Time.time > NextKick)
                {                 
                    anim.SetTrigger("Attack");
                    NextKick = Time.time + Delay;
                }

            }
        }
        if (Health <= 0 && !Dead)
        {
            Dead = true;
            anim.SetTrigger("Dead");
            Destroy(gameObject.GetComponent<CapsuleCollider>());
            Destroy(gameObject.GetComponent<BoxCollider>());           
            Player.GetComponent<HealthBar>().Kill();
        }
       // Debug.Log(Vector3.Distance(transform.position, Player.transform.position));
    }

     public void Attack()
    {
        Aud.clip = clips[1];
        Aud.Play();
        Player.GetComponent<HealthBar>().Damage(Damage);
    }
}
