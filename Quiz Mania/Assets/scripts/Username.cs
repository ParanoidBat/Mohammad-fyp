// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Username : MonoBehaviour
{

    public InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("username")){
            string value = PlayerPrefs.GetString("username");

            if(!string.IsNullOrEmpty(value))
                SceneManager.LoadScene("Main Menu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckUsername(){
        string name = inputField.text;

        if (string.IsNullOrEmpty(name)){
            return;
        }

        else
            PlayerPrefs.SetString("username", name);
            SceneManager.LoadScene("Main Menu");
    }
}
