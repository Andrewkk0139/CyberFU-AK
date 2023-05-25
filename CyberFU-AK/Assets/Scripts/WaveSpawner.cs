using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{ 
public enum SpawnState
{
    Spawning,
    Waiting,
    Counting
}

[System.Serializable]
public class Wave
{
    public string name;
    public Transform enemy;
    public int count;
    public float rate;
}

public Wave[] waves;
private int nextWave = 0;

public Transform[] spawnPoints;

public float timeBetweenWaves = 6f;
public float waveCountdown;

private float searchCountdown = 1f;

private SpawnState state = SpawnState.Counting;

// Start is called before the first frame update
void Start()
{
    if (spawnPoints.Length == 0)
    {

    }

    waveCountdown = timeBetweenWaves;
}

// Update is called once per frame
void Update()
{
    if (state == SpawnState.Waiting)
    {
        if (!EnemyIsAlive())
        {
            WaveCompleted();
        }
        else
        {
            return;
        }
    }

    if (waveCountdown <= 0)
    {
        if (state != SpawnState.Spawning)
        {
            StartCoroutine(SpawnWave(waves[nextWave]));
        }
    }
    else
    {
        waveCountdown -= Time.deltaTime;
    }
}

void WaveCompleted()
{
    state = SpawnState.Counting;
    waveCountdown = timeBetweenWaves;

    if (nextWave + 1 > waves.Length - 1)
    {
        nextWave = 0;
    }
    else
    {
        nextWave++;
    }
}

bool EnemyIsAlive()
{
    searchCountdown -= Time.deltaTime;
    if (searchCountdown <= 0)
    {
        searchCountdown = 1f;
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            return false;
        }
    }

    return true;
}

IEnumerator SpawnWave(Wave _wave)
{
    state = SpawnState.Spawning;

    state = SpawnState.Waiting;

    for (int i = 0; i < _wave.count; i++)
    {
        SpawnEnemy(_wave.enemy);
        yield return new WaitForSeconds(10f);
    }

    yield break;
}

void SpawnEnemy(Transform _enemy)
{
    Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
    Instantiate(_enemy, _sp.position, _sp.rotation);
}
}