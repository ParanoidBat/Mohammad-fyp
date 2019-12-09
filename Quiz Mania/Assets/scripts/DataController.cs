using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DataController : MonoBehaviour{

    public RoundData[] allRoundsData;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene("Main Menu");
    }

    public RoundData GetCurrentRoundData()
    {
        return allRoundsData[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
