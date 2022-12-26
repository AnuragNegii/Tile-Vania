using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessions : MonoBehaviour
{
    [SerializeField] int playerLives = 2;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake() 
    {
        int numGameSessions = FindObjectsOfType<GameSessions>().Length;
        if(numGameSessions > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();     
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }else
        {
        ResetGameSession();
        }
    }

    public void AddToScore(int pointToAdd){
        score += pointToAdd;
        scoreText.text = score.ToString();     

    }
    void TakeLife(){
        playerLives = playerLives -1;//playaerlives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    void ResetGameSession(){
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
    }

}
