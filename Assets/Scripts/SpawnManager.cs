using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    private float spawnRange = 9;
    public int enemyCount;
    public int enemyToSpawn = 0;

    public GameObject powerUpPrefab;
    private GameManager gameManagerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if(enemyCount  == 0 && !gameManagerScript.isGameOver){
            enemyToSpawn++;
            SpawnEnemy(enemyToSpawn);
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation); 
        }
        
    }

    void SpawnEnemy(int enemiesToSpawn){

        for(int i = 0; i < enemiesToSpawn; i++){
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation); 
        }

    }

    private Vector3 GenerateSpawnPosition(){
        float spawnPositionX = Random.Range(-spawnRange, spawnRange);
        float spawnPositionZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(spawnPositionX, 0, spawnPositionZ);
    }
}
