﻿using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour{

    public RoundData[] allRoundsDataPhysics;
    public RoundData[] allRoundsDataMaths;
    public RoundData[] allRoundsDataTech;

    private HighScore highScore;

    private NetworkController nc;

    private string fileName = "data.json";
    private string username;

    // Use this for initialization
    void Start()
    {
        nc = FindObjectOfType<NetworkController>();

        DontDestroyOnLoad(gameObject);

        LoadGameData();

        SceneManager.LoadScene("Username");
    }

    public RoundData GetPhysicsRoundData()
    {
        return allRoundsDataPhysics[0];
    }

    public RoundData GetMathsRoundData()
    {
        return allRoundsDataMaths[0];
    }

    public RoundData GetTechRoundData()
    {
        return allRoundsDataTech[0];
    }

    private int CalculateHighestScore(){
        int math, phy, tech, highest;
        math = highScore.mathsHighestScore;
        phy = highScore.physicsHighestScore;
        tech = highScore.techHighestScore;

        return math + phy + tech;
    }

    private void GetUsername(){
        username = PlayerPrefs.GetString("username");
    }

    public void UpdateHighScore(int newScore, string key)
    {
        // If newScore is greater than highest score, update it
        if (key == "physics" && newScore > highScore.physicsHighestScore)
        {
            highScore.physicsHighestScore = newScore;
            SaveHighScore(key);

            GetUsername();

            nc.startPostScores(CalculateHighestScore(), username);
        }

        else if (key == "maths" && newScore > highScore.mathsHighestScore)
        {
            highScore.mathsHighestScore = newScore;
            SaveHighScore(key);

            GetUsername();

            nc.startPostScores(CalculateHighestScore(), username);
        }

        else if (key == "tech" && newScore > highScore.techHighestScore)
        {
            highScore.techHighestScore = newScore;
            SaveHighScore(key);

            GetUsername();

            nc.startPostScores(CalculateHighestScore(), username);
        }
    }

    public int GetHighestPlayerScore(string key)
    {
        if (key == "Quiz Window Physics")
            return highScore.physicsHighestScore;

        if(key == "Quiz Window Maths")
            return highScore.mathsHighestScore;

        return highScore.techHighestScore;
    }

    private void LoadGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string jsonData = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            GameData loadedData = JsonUtility.FromJson<GameData>(jsonData);

            // Retrieve the allRoundData property of loadedData
            allRoundsDataPhysics = loadedData.allRoundsDataPhysics;
            allRoundsDataMaths = loadedData.allRoundsDataMaths;
            allRoundsDataTech = loadedData.allRoundsDataTech;
        }
        else
        {
            Debug.LogError("Could'nt load data!");
        }
        
        // anroid data loading
        TextAsset file = Resources.Load(fileName) as TextAsset;
        if(file != null){
            string content = file.ToString ();

            GameData Data = JsonUtility.FromJson<GameData>(content);

            // Retrieve the allRoundData property of loadedData
            allRoundsDataPhysics = Data.allRoundsDataPhysics;
            allRoundsDataMaths = Data.allRoundsDataMaths;
            allRoundsDataTech = Data.allRoundsDataTech;
        }

    }

    public void LoadHighScore(string key)
    {
        highScore = new HighScore();

        if (PlayerPrefs.HasKey("physics"))
        {
            highScore.physicsHighestScore = PlayerPrefs.GetInt("physics");
        }

        if (PlayerPrefs.HasKey("maths"))
        {
            highScore.mathsHighestScore = PlayerPrefs.GetInt("maths");
        }

        if (PlayerPrefs.HasKey("tech"))
        {
            highScore.techHighestScore = PlayerPrefs.GetInt("tech");
        }
    }

    private void SaveHighScore(string key)
    {
        int highestScore;

        if(key == "physics")
            highestScore = highScore.physicsHighestScore;
        else if(key == "maths")
            highestScore = highScore.mathsHighestScore;
        else
            highestScore = highScore.techHighestScore;

        PlayerPrefs.SetInt(key, highestScore);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
