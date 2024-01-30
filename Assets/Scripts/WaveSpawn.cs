using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float timeBetweenEnemies = 0.5f;
    public int waveNumber;
    public List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        waveNumber = GameManager.Instance.waveNumber;
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        enemies.RemoveAll(e => e.IsDestroyed());
        if (enemies.Count == 0)
            Destroy(gameObject);
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    void SpawnEnemy()
    {
        enemies.Add(Instantiate(enemyPrefab, transform.position, transform.rotation));
    }

}
