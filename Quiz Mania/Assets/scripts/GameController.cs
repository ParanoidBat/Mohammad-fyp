using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
    public GameObject quizWindow;

    public Text questionText;
    public Text scoreText;
    public Text countdownText;
    public Text roundEndScoreDisplay;
    public Text highScoreDisplay;

    private bool isRoundActive;
    private float timeLeft;
    private int questionIndex;
    private int Score;

    public SimpleObjectPool answerButtonsObjectPool;
    public Transform answerButtonParent;

    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionsArray;
    
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    private string sceneName;

    // Use this for initialization
    void Start()
    {
        dataController = FindObjectOfType<DataController>(); //get the data controller instance to populate game with data
        LoadData();
        questionsArray = currentRoundData.questions;
        timeLeft = currentRoundData.countdown;
        UpdateTimeLeft();

        Score = 0;
        questionIndex = 0; //question number from questions list

        DisplayQuestion();
        isRoundActive = true;

    }

    private void RemoveOldAnswerButtons() //remove previous question's answer buttons and recycle them
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonsObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    private void DisplayQuestion()
    {
        RemoveOldAnswerButtons();

        QuestionData questionData = questionsArray[questionIndex];
        questionText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++) //get the number of answers this question has. then make that amount of answer buttons
        {
            GameObject answerButtonGameObject = answerButtonsObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent); // parent is the answers panel
            answerButtonGameObject.transform.localScale = Vector3.one;

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);
        }
    }

    public void EndRound()
    {
        isRoundActive = false;

        if(sceneName == "Quiz Window Physics"){
            dataController.LoadHighScore("physics");
            dataController.UpdateHighScore(Score, "physics");
        }
        else if(sceneName == "Quiz Window Maths"){
            dataController.LoadHighScore("maths");
            dataController.UpdateHighScore(Score, "maths");
        }
        else if(sceneName == "Quiz Window Tech"){
            dataController.LoadHighScore("tech");
            dataController.UpdateHighScore(Score, "tech");
        }

        highScoreDisplay.text = dataController.GetHighestPlayerScore(sceneName).ToString(); // text game object is not made. make one in unity editor

        // deactivate windows to show panel
        quizWindow.SetActive(false);
        questionDisplay.SetActive(false);

        roundEndScoreDisplay.text = scoreText.text;

        roundEndDisplay.SetActive(true);
    }

    public void OnClickAnswerButton(bool isCorrect)
    {
        if (isCorrect) //if it is the correct answer
        {
            Score += currentRoundData.scoreAdded;
            scoreText.text = Score.ToString();
            timeLeft = currentRoundData.countdown;
        }
        else //the answer was incorrect so end the round
        {
            EndRound();
        }

        if (questionsArray.Length > questionIndex + 1) //if there are still questions in the questions list, continue with next question
        {
            questionIndex++;
            DisplayQuestion();
        }
        else //end the round
        {
            EndRound();
        }

    }

    private void UpdateTimeLeft()
    {
        countdownText.text = Mathf.Round(timeLeft).ToString();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (isRoundActive)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimeLeft();

            if (timeLeft <= 0f)
            {
                EndRound();
            }

        }
    }

    void LoadData(){ // get the data of the category for whom scene we're in

        sceneName = SceneManager.GetActiveScene().name;

        if(sceneName == "Quiz Window Physics"){
            currentRoundData = dataController.GetPhysicsRoundData();
        }

        else if (sceneName == "Quiz Window Maths"){
            currentRoundData = dataController.GetMathsRoundData();
        }

        else if(sceneName == "Quiz Window Tech"){
            currentRoundData = dataController.GetTechRoundData();
        }

        else{
            Debug.LogError("Data could not be loaded!");
        }
    }
}
