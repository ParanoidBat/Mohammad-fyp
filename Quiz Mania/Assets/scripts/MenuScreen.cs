using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("Quiz Window");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
