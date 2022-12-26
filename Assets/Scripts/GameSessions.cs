using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessions : MonoBehaviour
{
    [SerializeField] int playerLives = 2;
   
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
    void TakeLife(){
        playerLives = playerLives -1;//playaerlives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void ResetGameSession(){
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
