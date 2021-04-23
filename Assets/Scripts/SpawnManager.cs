using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerUps;

    [SerializeField]
    private float _waitForSeconds = 5.0f;
    private bool _stopSpawning = false;



    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }


    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            GameObject enemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-2.3f, 2.3f), 8, 0), Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_waitForSeconds);
        }
     }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        { 
            int randomPowerUp = Random.Range(0, 4);
            Instantiate(_powerUps[randomPowerUp], new Vector3(Random.Range(-2.3f, 2.3f), 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 10));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
