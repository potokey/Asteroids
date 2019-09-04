using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    public Text ActualScoresText;
    public Text ActualLivesText;
    public GameObject Joystick;
    public GameObject ShotButton;
    void Start()
    {
        Aggregator.Subscribe<int>("SendPointsToUI", SetPointsAction);
        Aggregator.Subscribe<int>("SendLivesToUI", SetLivesAction);
        Aggregator.Subscribe<bool>("StartGame", StartGameAction);
        Aggregator.Subscribe<int>("GameOver", GameOverActions);
    }

    private void SetLivesAction(int lives)
    {
        ActualLivesText.text = "Lives: " + lives;
    }

    private void StartGameAction(bool obj)
    {
        ActualScoresText.text = "Scores: 0";
        ActualLivesText.text = "Lives: 3";
        if (Application.platform == RuntimePlatform.Android)
        {          
            Joystick.SetActive(true);
            ShotButton.SetActive(true);
        }
    }

    private void GameOverActions(int obj)
    {
        Joystick.SetActive(false);
        ShotButton.SetActive(false);
    }


    private void SetPointsAction(int points)
    {
        ActualScoresText.text = "Scores: " + points;
    }

    public void Shot()
    {
        Aggregator.Publish<bool>("Shot", true);
    }
}
