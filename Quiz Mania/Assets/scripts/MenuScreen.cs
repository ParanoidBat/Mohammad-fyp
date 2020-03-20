using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGamePhysics()
    {
        SceneManager.LoadScene("Quiz Window Physics");
    }

    public void StartGameMaths()
    {
        SceneManager.LoadScene("Quiz Window Maths");
    }

    public void StartGameTech()
    {
        SceneManager.LoadScene("Quiz Window Tech");
    }

    public void LeaderBoard(){
        SceneManager.LoadScene("Leaderboard Scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
