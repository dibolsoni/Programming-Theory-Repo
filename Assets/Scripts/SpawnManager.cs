using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    public float timeBetweenWaves = 20f;
    private float nextWaveTime;
    public GameObject waveSpawn;
    private List<GameObject> waves;

    public int WaveNumber { get { return waves.Count; }  }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        nextWaveTime = Time.time;
        waves = new List<GameObject>();
        NextWave();
    }


    public bool canSpawn
    {
        get
        {
            if (waves.Last().IsDestroyed() || Time.time >= nextWaveTime)
                return true;
            else
                return false;
        }
    }

    private void LateUpdate()
    {
        if (canSpawn)
        {
            NextWave();
        }
    }


    void NextWave()
    {
        waves.Add(Instantiate(waveSpawn));
        GameManager.Instance.NextWave();
        nextWaveTime = timeBetweenWaves + Time.time;
    }
}
