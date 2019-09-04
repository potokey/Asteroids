using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour {
    public Text ActualScoresText;
    public Text ActualLivesText;
    // Use this for initialization
	void Start () {
        Aggregator.Subscribe<int>("SendPointsToUI", SetPointsAction);
        Aggregator.Subscribe<int>("SendLivesToUI", SetLivesAction);
        Aggregator.Subscribe<bool>("StartGame", StartGameAction);
    }

    private void SetLivesAction(int lives)
    {
        ActualLivesText.text = "Lives: " +lives;
    }

    private void StartGameAction(bool obj)
    {        
        ActualScoresText.text ="Scores: 0";
        ActualLivesText.text = "Lives: 3";
    }

    private void SetPointsAction(int points)
    {
        ActualScoresText.text = "Scores: "+ points;
    }
}
