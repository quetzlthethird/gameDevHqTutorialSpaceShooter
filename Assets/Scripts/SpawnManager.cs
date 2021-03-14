using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject _tripleShotPowerUp ;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine() );
        StartCoroutine(SpawnPowerupRoutine() );
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator SpawnEnemyRoutine()
    {
        //I had limited it by a new option enemycount, but this created 10 at once
        while (_stopSpawning == false)
        {  
            Vector3 posToSpawn = new Vector3(Random.Range(-11.0f,11.0f), 10.0f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab , posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform ;  
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.0f,9.0f), 10.0f, 0);
            GameObject newPowerUp = Instantiate(_tripleShotPowerUp, posToSpawn, Quaternion.identity);
            // _PowerUpSpawnRate = Random.Range(3.0f,7.0f);
            yield return new WaitForSeconds(Random.Range(3,8) );
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true; 
    }

}

//NOTES===================================================
//yield return null = wait one frame, then next line
//if you add the call to update, it spawns LIKE MAD
//to get it into a container, create a variable when instantiating
//then use that variable to reassign the parent
//while(true) {} == infinite loop!
