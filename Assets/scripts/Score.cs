using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text timer;
    public int Complexity;
    
    [HideInInspector]
    public int[] Time = new int[2];
    private float millisecond;

    public int Kills;
    private GameObject Player;

    void Start()
    {
        Time[0] = 0;
        Time[1] = 0;
        millisecond = 0;
        Kills = 0;
        Player = GameObject.Find("Player");
        Complexity = MazeSpawner.Complexity;
    }

    void Update()
    {
        Kills = Player.GetComponent<HealthBar>().Kills;
    }

    public void FixedUpdate()
    {
        millisecond += 0.02f;
        if(millisecond >= 1)
        {
            millisecond = 0;
            Time[1] += 1;
        }
        if(Time[1] == 60)
        {
            Time[1] = 0;
            Time[0] += 1;
        }
        timer.text = Convert.ToString(Time[0]) + ":" + Convert.ToString(Time[1]);
    }
    public int[] GetScore()
    {
        int[] score = new int[2];
        score[0] = Complexity * 1000 - Time[0] * 60 - Time[1];
        score[1] = score[0] + Kills * 350;

        return score;
    }
}
