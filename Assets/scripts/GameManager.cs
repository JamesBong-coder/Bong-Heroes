using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instanse { get; set; }

    public GameObject MenuVhoda;
    public GameObject OsvMenu;
    public GameObject ReitMenu;
    public GameObject Difficult;
    public GameObject DeleteMenu;
    public InputField PlayerName;
    public Text[] Scores;



    public void Start()
    {
        Time.timeScale = 1;
        ReitMenu.SetActive(false);
        MenuVhoda.SetActive(false);
        Difficult.SetActive(false);
        DeleteMenu.SetActive(false);
        OsvMenu.SetActive(true);
    }


    public void SetDifficultButton()
    {
        Difficult.SetActive(true);
        OsvMenu.SetActive(false);
    }

    public void SetNameButton()
    {
        if (MazeSpawner.Complexity != 0)
        {
            Difficult.SetActive(false);
            MenuVhoda.SetActive(true);
        }
    }

    public void StartGameButton()
    {
        if (PlayerName.text == "")
            HealthBar.PlayerName = "Player";
        else
            HealthBar.PlayerName = PlayerName.text;

        Debug.Log(PlayerController.UserName);
        SceneManager.LoadScene("Labirinth", LoadSceneMode.Single);
    }

    public void SetEasyButton()
    {
        MazeSpawner.Complexity = 1;
    }

    public void SetNormalButton()
    {
        MazeSpawner.Complexity = 2;
    }

    public void SetHardButton()
    {
        MazeSpawner.Complexity = 3;
    }

    public void BackButton()
    {
        ReitMenu.SetActive(false);
        MenuVhoda.SetActive(false);
        Difficult.SetActive(false);
        DeleteMenu.SetActive(false);
        OsvMenu.SetActive(true);
    }

    public void ReitButton()
    {
        ShowReit(1);

        OsvMenu.SetActive(false);
        ReitMenu.SetActive(true);
    }

    public void GetEasyReitButton()
    {
        ShowReit(1);
    }

    public void GetNormalReitButton()
    {
        ShowReit(2);
    }

    public void GetHardReitButton()
    {
        ShowReit(3);
    }

    public void ShowReit(int difficult)
    {
        int[] score = SaveAndLoad.GetScoreTopList(difficult);
        string[] name = SaveAndLoad.GetNameTopList(difficult);
        for (int i = 0; i < 7; i++)
        {
            Scores[i].text = Convert.ToString(i + 1) + ". " + name[i] + ":  " + Convert.ToString(score[i]);
        }
    }

    public void ShowDeleteButton()
    {
        ReitMenu.SetActive(false);
        DeleteMenu.SetActive(true);
    }

    public void DeleteReitButton()
    {
        PlayerPrefs.DeleteAll();
        DeleteMenu.SetActive(false);
        OsvMenu.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
