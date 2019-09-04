using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text HighscoreText;
    public Image GameOverImage;
    List<int> listOfScores = new List<int>();
	// Use this for initialization
	void Start () {
        Aggregator.Subscribe<int>("GameOver", GameOverActions);
	}

    private void GameOverActions(int points)
    {
        listOfScores.Add(points);
        HighscoreText.text = String.Join("\n", listOfScores.OrderByDescending(o=>o).Select((s, i) => i + 1 + ". " + s).ToArray());
        this.gameObject.SetActive(true);
        GameOverImage.gameObject.SetActive(true);
    }

    public void StartButtonClick()
    {
        Aggregator.Publish("StartGame", true);
        this.gameObject.SetActive(false);
        GameOverImage.gameObject.SetActive(false);
    }

    
}
