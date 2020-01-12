using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour{

    private RoundData[] allRoundsData;
    private HighScore highScore;
    private string fileName = "data.json";

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        LoadGameData();
        LoadHighScore();

        SceneManager.LoadScene("Main Menu");
    }

    public RoundData GetCurrentRoundData()
    {
        return allRoundsData[0];
    }

    public void UpdateHighScore(int newScore)
    {
        Debug.LogError("prevHighScore: " + highScore.highest_score);
        // If newScore is greater than highest score, update it
        if (newScore > highScore.highest_score)
        {
            highScore.highest_score = newScore;
            SaveHighScore();
        }
    }

    public int GetHighestPlayerScore()
    {
        return highScore.highest_score;
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
            allRoundsData = loadedData.allRoundsData;
        }
        else
        {
            Debug.LogError("Could'nt load data!");
        }
    }

    private void LoadHighScore()
    {
        highScore = new HighScore();

        if (PlayerPrefs.HasKey("highest_score"))
        {
            highScore.highest_score = PlayerPrefs.GetInt("highest_score");
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("highest_score", highScore.highest_score);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
