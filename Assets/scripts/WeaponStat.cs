using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    public int Attack;
    public int Defend;
    public float TimeDamage;
    public float DamageDist;
    private GameObject Player;

    public void Start()
    {
        Player = GameObject.Find("Player");
    }


    public void AttackFunk()
    {
        Player.GetComponent<Weapon>().Attack();
    }
}
