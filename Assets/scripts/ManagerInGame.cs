using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerInGame : MonoBehaviour {

    public GameObject Pause;
    public GameObject Hint;
    public GameObject Main;
    public GameObject Scrim;
    public GameObject LosePanel;
    public Text Kills;
    public Text Complexity;

    public GameObject VictoryPanel;
    public Text VictoryCompl;
    public Text VictoryKills;
    public Text VictoryTime;
    public Text VictoryScore;

    public Camera Cam;
    private GameObject Player;

    void Start () {
        Cursor.visible = false;
        Player = GameObject.Find("Player");
        Pause.SetActive(false);
        Hint.SetActive(false);
        Cam.gameObject.SetActive(false);
        LosePanel.SetActive(false);
        Main.SetActive(true);
        switch (MazeSpawner.Complexity)
        {
            case 1:
                Complexity.text = "Сложность: Легко";
                break;
            case 2:
                Complexity.text = "Сложность: Нормально";
                break;
            case 3:
                Complexity.text = "Сложность: Сложно";
                break;
        }
        Kills.text = "Убийства: 0";
        Time.timeScale = 1;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)){
            Cursor.visible = true;
            Pause.SetActive(true);
            Time.timeScale = 0;
            Player.GetComponent<CameraScript>().SetRotation(0);
            Kills.text = "Убийства: " + Convert.ToString(GameObject.Find("Score").GetComponent<Score>().Kills);
        }
    }

    public void BackButton()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
        Player.GetComponent<CameraScript>().SetRotation(2);
        Cursor.visible = false;
    }
    public void BackHintButton()
    {
        Hint.SetActive(false);
        Time.timeScale = 1;
        Cam.gameObject.SetActive(false);
        Player.GetComponent<CameraScript>().SetRotation(2);
        Cursor.visible = false;
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Labirinth", LoadSceneMode.Single);
    }
    public void HintActive()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
        Hint.SetActive(true);
        Cam.gameObject.SetActive(true);
        Player.GetComponent<CameraScript>().SetRotation(0);
    }
    public void Finish()
    {
        Cursor.visible = true;
        VictoryCompl.text = Complexity.text;
        VictoryKills.text = "Убийства: " + Convert.ToString(GameObject.Find("Score").GetComponent<Score>().Kills);
        int[] score = GameObject.Find("Score").GetComponent<Score>().GetScore();
        VictoryTime.text = "ВРЕМЯ: " + Convert.ToString(score[0]);
        VictoryScore.text = "ИТОГ: " + Convert.ToString(score[1]);
        Main.SetActive(false);
        VictoryPanel.SetActive(true);
        Time.timeScale = 0;
        Player.GetComponent<CameraScript>().SetRotation(0);
        SaveAndLoad.NewTopList(HealthBar.PlayerName, score[1], MazeSpawner.Complexity);
    }
   public void Lose()
   {
        Cursor.visible = true;
        Time.timeScale = 0;
        Player.GetComponent<CameraScript>().SetRotation(0);
        LosePanel.SetActive(true);
   }
}
