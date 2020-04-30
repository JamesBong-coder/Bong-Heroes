using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public float maxValue = 20;
    public Slider sliderPlayer;
    public static string PlayerName;
    public Slider sliderEnemy;
    public int Kills;

    private bool Dead;
    public float current;

    void Start()
    {

        sliderEnemy.gameObject.SetActive(false);
        sliderPlayer.GetComponentInChildren<Text>().text = PlayerName;
        sliderPlayer.maxValue = maxValue;
        sliderPlayer.minValue = 0;
        current = maxValue;
        Dead = false;
        Kills = 0;
    }

    void Update()
    {
        if (current < 0 && !Dead)
        {
            current = 0;
            GameObject.Find("GameManager").GetComponent<ManagerInGame>().Lose();
            Dead = true;
        }
        sliderPlayer.value = current;
        if(sliderEnemy.value == 0)
            sliderEnemy.gameObject.SetActive(false);
    }

    public void Damage(float dmg)
    {
        current -= dmg - GameObject.Find("Player").GetComponent<Weapon>().DefendPlayer;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy")
        {
            sliderEnemy.gameObject.SetActive(true);
            sliderEnemy.maxValue = other.GetComponent<Enimies>().MaxHealth;
            sliderEnemy.value = other.GetComponent<Enimies>().Health;
            sliderEnemy.GetComponentInChildren<Text>().text = other.GetComponent<Enimies>().EnemyName;
        }
        if (other.tag == "Hint")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject.Find("GameManager").GetComponent<ManagerInGame>().HintActive();
            }
        }
        if(other.tag == "Heal")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (current < maxValue - 20)
                    current += 20;
                else
                    current = maxValue;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            sliderEnemy.gameObject.SetActive(false);
        }
    }
    public void Kill()
    {
        Kills++;
        GameObject.Find("Player").GetComponent<Weapon>().enemy = null; ;

    }

}
