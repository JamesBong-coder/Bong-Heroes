using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public GameObject[] Weapons;
    public GameObject[] AvatarWeapon;
    private bool[] UnlockWeapon = new bool[4];
    public int currentWeapon;
    public int AttackPlayer;
    public int DefendPlayer;
    public float TimeDamagePlayer;
    public float DamageDistPlayer;

    private int SizeMagazine = 5;
    public int bulletsInMagazine;
    public int bullets = 10;

    public GameObject Gun;
    public Text TextBullets;
    public Text Pod;

    public Collider enemy;

    private float NextShoot;
    private bool Finish;

    void Start()
    {
        TextBullets.text = Convert.ToString(bullets) + "/" + Convert.ToString(bulletsInMagazine);
        NextShoot = Time.time;
        for (int i = 0; i < 4; i++) UnlockWeapon[i] = false;
        currentWeapon = 0;
        Weapons[currentWeapon].SetActive(true);
        AttackPlayer = Weapons[currentWeapon].GetComponent<WeaponStat>().Attack;
        DefendPlayer = Weapons[currentWeapon].GetComponent<WeaponStat>().Defend;
        TimeDamagePlayer = Weapons[currentWeapon].GetComponent<WeaponStat>().TimeDamage;
        DamageDistPlayer = Weapons[currentWeapon].GetComponent<WeaponStat>().DamageDist;
        Finish = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (other.tag)
            {
                case "Axe":
                    UnlockWeapon[0] = true;
                    AvatarWeapon[0].SetActive(true);
                    break;
                case "Sword":
                    UnlockWeapon[1] = true;
                    AvatarWeapon[1].SetActive(true);
                    break;
                case "Chainsaw":
                    UnlockWeapon[2] = true;
                    AvatarWeapon[2].SetActive(true);
                    break;
                case "Gun":
                    bulletsInMagazine = SizeMagazine;
                    UnlockWeapon[3] = true;
                    AvatarWeapon[3].SetActive(true);
                    TextBullets.text = Convert.ToString(bullets) + "/" + Convert.ToString(bulletsInMagazine);
                    break;
                case "Bullets":
                    bullets += 3;
                    TextBullets.text = Convert.ToString(bullets) + "/" + Convert.ToString(bulletsInMagazine);
                    break;
                default:
                    break;
            }
        }
        if (other.tag == "Enemy")
        {
            enemy = other;
            if (NextShoot < Time.time)
            {
                if (Input.GetMouseButtonDown(0) && currentWeapon!=4)
                {
                    Weapons[currentWeapon].GetComponent<Animator>().SetTrigger("Kick");
                    if (currentWeapon == 3) Weapons[currentWeapon].GetComponent<AudioSource>().Play();
                    NextShoot = Time.time + TimeDamagePlayer;

                }
            }
        }
        if (other.tag == "Finish")
        {
            if (Input.GetKeyDown(KeyCode.E) && !Finish)
            {
                GameObject.Find("GameManager").GetComponent<ManagerInGame>().Finish();
                Finish = true;
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Change(0);
        if (Input.GetKeyDown(KeyCode.Alpha2) && UnlockWeapon[0]) Change(1);
        if (Input.GetKeyDown(KeyCode.Alpha3) && UnlockWeapon[1]) Change(2);
        if (Input.GetKeyDown(KeyCode.Alpha4) && UnlockWeapon[2]) Change(3);
        if (Input.GetKeyDown(KeyCode.Alpha5) && UnlockWeapon[3]) Change(4);

        if (NextShoot < Time.time)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentWeapon == 4)
                {
                    if (bulletsInMagazine == 0) return;
                    else
                    {
                        Weapons[currentWeapon].GetComponent<Animator>().SetTrigger("Kick");
                        Weapons[currentWeapon].GetComponent<AudioSource>().Play();
                        Gun.GetComponent<SimpleShoot>().ShootGun();
                        bulletsInMagazine--;
                        TextBullets.text = Convert.ToString(bullets) + "/" + Convert.ToString(bulletsInMagazine);
                    }
                }
                 else Weapons[currentWeapon].GetComponent<Animator>().SetTrigger("Kick");
                if (currentWeapon == 3) Weapons[currentWeapon].GetComponent<AudioSource>().Play();

                NextShoot = Time.time + TimeDamagePlayer;

            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            if(currentWeapon == 4 && bulletsInMagazine !=10 && bullets != 0)
            {
                if(bullets > SizeMagazine - bulletsInMagazine)
                {
                    Weapons[currentWeapon].GetComponent<Animator>().SetTrigger("Reload");
                    bullets = bullets - (SizeMagazine - bulletsInMagazine);
                    bulletsInMagazine = SizeMagazine;
                    TextBullets.text = Convert.ToString(bullets) + "/" + Convert.ToString(bulletsInMagazine);
                    Pod.gameObject.SetActive(false);
                    NextShoot = Time.time + TimeDamagePlayer;
                }
                else
                {
                    Weapons[currentWeapon].GetComponent<Animator>().SetTrigger("Reload");
                    bulletsInMagazine += bullets;
                    bullets = 0;
                    TextBullets.text = Convert.ToString(bullets) + "/" + Convert.ToString(bulletsInMagazine);
                    Pod.gameObject.SetActive(false);
                    NextShoot = Time.time + TimeDamagePlayer;
                }
            }
        }
        if (bulletsInMagazine == 0 && bullets != 0 && UnlockWeapon[3] && currentWeapon == 4) Pod.gameObject.SetActive(true);

        
    }
    public void Change(int cur)
    {
        Weapons[currentWeapon].SetActive(false);
        currentWeapon = cur;
        Weapons[currentWeapon].SetActive(true);
        AttackPlayer = Weapons[currentWeapon].GetComponent<WeaponStat>().Attack;
        DefendPlayer = Weapons[currentWeapon].GetComponent<WeaponStat>().Defend;
        TimeDamagePlayer = Weapons[currentWeapon].GetComponent<WeaponStat>().TimeDamage;
        DamageDistPlayer = Weapons[currentWeapon].GetComponent<WeaponStat>().DamageDist;
    }

    public void Attack()
    {
        if (enemy != null)
        {
            if (Vector3.Distance(transform.position, enemy.gameObject.transform.position) <= DamageDistPlayer)
                enemy.GetComponent<Enimies>().Health -= AttackPlayer;
        }
    }
}
