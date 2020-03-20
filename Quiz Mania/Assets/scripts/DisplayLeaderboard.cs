using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLeaderboard : MonoBehaviour
{

    public Text[] names;
    public Text[] scores;

    // public string[] onlineHighscore;

    private NetworkController nc;

    // Start is called before the first frame update
    void Start()
    {
        nc = FindObjectOfType<NetworkController>();
        nc.startGetScores();

        // StartCoroutine(Delay());

        Invoke("DisplayScores", 2.0f);

        // onlineHighscore = (string[]) nc.onlineHighscore.Clone();

        DisplayScores();
    }

    private void DisplayScores(){
        for(int i =0, j= 0; i< nc.onlineHighscore.Length; i++, j++){
            // Debug.Log(nc.onlineHighscore[i]);
            names[i].text = nc.onlineHighscore[j];
            j++;
            scores[i].text = nc.onlineHighscore[j].ToString();
        }
    }
}
