using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    //работа с сортировкой, записью и чтением таблицы рекордов
    public static void RecordTopList(int[] score, string[] name, int difficult)//Запись рекордов в базу
    {
        for (int i = 0; i < 7; i++)
        {
            PlayerPrefs.SetInt("Score" + Convert.ToString(i) + Convert.ToString(difficult), score[i]);
            PlayerPrefs.SetString("Name" + Convert.ToString(i) + Convert.ToString(difficult), name[i]);
        }
    }
    public static int[] GetScoreTopList(int difficult)//Чтение очков из базы
    {
        int[] score = new int[7];
        for (int i = 0; i < 7; i++)
        {
            score[i] = PlayerPrefs.GetInt("Score" + Convert.ToString(i) + Convert.ToString(difficult));
        }
        return score;
    }
    public static string[] GetNameTopList(int difficult)//Чтение имен из базы
    {
        string[] name = new string[7];
        for (int i = 0; i < 7; i++)
        {
            name[i] = PlayerPrefs.GetString("Name" + Convert.ToString(i) + Convert.ToString(difficult));
        }
        return name;
    }
    public static void NewTopList(string namePlayer, int ScorePlayer, int difficult)
    {
        int[] score = score = GetScoreTopList(difficult);
        string[] name = GetNameTopList(difficult);
        int x = 0;

        while (x < 7 && ScorePlayer <= score[x]) x++;
        if (x == 7) return;
        else
        {
            for (int i = 6; i > x; i--)
            {
                score[i] = score[i - 1];
                name[i] = name[i - 1];
            }
            score[x] = ScorePlayer;
            name[x] = namePlayer;
            RecordTopList(score, name, difficult);
        }

    }//создание и заись обновленной таблицы рекордов
}
