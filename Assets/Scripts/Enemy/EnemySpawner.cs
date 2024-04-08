using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    // Make a class of waves with the number of enemies spawn in each wave
    [System.Serializable]
    public class Wave
    {
        public int WaveNumber;
        public float FighterSpawnAmount;
        public float RangeSpawnAmount;
        public float TankSpawnAmount;
    }
    public bool IsWaveOngoing;
    [SerializeField] private GameObject _boonObject;

    [Header("Wave Stuff")]
    public List<Wave> Waves;
    public Transform[] WaveSource;
    [SerializeField] EnemyPooler _enemyPooler;

    [Header("List of Enemies")]
    // list of enemies spawned in this wave
    [SerializeField] List<GameObject> _enemiesToSpawn;
    [SerializeField] List<GameObject> _enemiesLeftInWave;

    private IEnumerator coroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Get Enemy Pooler Instance
        _enemyPooler = EnemyPooler.Instance;
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        //SpawnEnemies(1, 3);
    }

    // Spawn Enemies with the corresponding wave
    public void SpawnEnemies(int waveNumber, float spawnSpeed)
    {
        foreach (Wave wave in Waves)
        {
            if (wave.WaveNumber == waveNumber)
            {
                Debug.Log("Spawn!");

                // Add Fighter type enemies
                for (int i = 0; i < wave.FighterSpawnAmount; i++)
                {
                    GameObject obj = _enemyPooler.SpawnFromPool("Fighters", WaveSource[Random.Range(0, WaveSource.Length)].position, Quaternion.identity, false);
                    _enemiesToSpawn.Add(obj);
                }

                // Add Ranged type enemies
                for (int i = 0; i < wave.RangeSpawnAmount; i++)
                {
                    GameObject obj = _enemyPooler.SpawnFromPool("Ranges", WaveSource[Random.Range(0, WaveSource.Length)].position, Quaternion.identity, false);
                    _enemiesToSpawn.Add(obj);
                }

                // Add Tank type enemies
                for (int i = 0; i < wave.TankSpawnAmount; i++)
                {
                    GameObject obj = _enemyPooler.SpawnFromPool("Tanks", WaveSource[Random.Range(0, WaveSource.Length)].position, Quaternion.identity, false);
                    _enemiesToSpawn.Add(obj);
                }

                _enemiesLeftInWave.AddRange(_enemiesToSpawn);

                // invoke periodic spawn
                coroutine = SpawnPeriodically(_enemiesToSpawn, spawnSpeed);
                StartCoroutine(coroutine);
            }
        }
    }

    // As the name suggest (if you dont see it), it spawns enemies periodically not instantly
    public IEnumerator SpawnPeriodically(List<GameObject> enemiesToSpawn, float spawnSpeed)
    {
        int totalEnemies = enemiesToSpawn.Count;
        for (int i = 0; i < totalEnemies ; i++)
        {
            yield return new WaitForSeconds(spawnSpeed);

            // Spawn enemies randomly from the list of enemies to be spawned
            int randomSpawn = Random.Range(0, enemiesToSpawn.Count);
            enemiesToSpawn[randomSpawn].SetActive(true);
            enemiesToSpawn.RemoveAt(randomSpawn);
        }
    }

    void CheckWaveStatus()
    {
        // Check if there is still enemies, if not then the wave is done
        if (_enemiesLeftInWave.Count == 0)
        {
            _boonObject.SetActive(true);

            IsWaveOngoing = false;
            GetComponent<MonumentInteract>().IsEnabled = true;
        }
        else
        {
            IsWaveOngoing = true;
        }
    }

    public void RemoveEnemiesFromWave(GameObject enemy)
    {
        Debug.Log("Removing enemy");
        _enemiesLeftInWave.Remove(enemy);
        CheckWaveStatus();
    }
}
