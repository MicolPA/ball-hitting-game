using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private SpawnManager spawnManagerScript;
    public TextMeshProUGUI levelCountText;
    public TextMeshProUGUI gameOverText;
    public Button restartGameButton;
    public GameObject player;
    public bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set current level text
        if(!isGameOver){
            levelCountText.text = "Level: " + spawnManagerScript.enemyToSpawn;
        }

        //Pause the game
        if(Input.GetKeyDown(KeyCode.P)){
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }

        DetectPlayerFall();
        
    }

    //Restart the game
    public void RestartGame(){
        gameOverText.gameObject.SetActive(false);
        restartGameButton.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameOver = false;
    }

    void DetectPlayerFall(){

        if(player.gameObject.transform.position.y < -5){

            gameOverText.gameObject.SetActive(true);
            restartGameButton.gameObject.SetActive(true);
            isGameOver = true;

        }
    }
}
