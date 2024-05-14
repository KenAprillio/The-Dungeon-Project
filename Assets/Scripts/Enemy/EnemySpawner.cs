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
        public float TimeToSpawn;
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

    [Header("Cutscene Stuff")]
    [SerializeField] private GameObject _bossCutscene;

    int _waveNumber;
    TimeScaler _canvas;
    AudioManager _audioManager;

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
        _canvas = TimeScaler.Instance;
        _audioManager = AudioManager.Instance;
    }

    // Spawn Enemies with the corresponding wave
    public void SpawnEnemies(int waveNumber)
    {
        foreach (Wave wave in Waves)
        {
            if (wave.WaveNumber == waveNumber)
            {
                Debug.Log("Spawn!");
                _waveNumber = waveNumber;

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
                coroutine = SpawnPeriodically(_enemiesToSpawn, wave.TimeToSpawn);
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
            // Updates Objective UI
            string mainText = "Persiapan gelombang selanjutnya";
            string subText = "Ambil upgrade baru";
            _canvas.UpdateMainObjective(mainText, subText);

            // Activates boon object
            if(_waveNumber != 5)
            {
                _audioManager.PlayMusic(_audioManager.inbetweenMusic);
                _boonObject.SetActive(true);
            }

            IsWaveOngoing = false;
            GetComponent<MonumentInteract>().IsEnabled = true;

            // Activate corresponding dialogue in the end of certain waves
            ActivateDialogue();
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

        string subtext = "Kalahkan " + _enemiesLeftInWave.Count + " musuh!";
        _canvas.SetSubText(subtext);

        CheckWaveStatus();
    }

    void ActivateDialogue()
    {
        if (_waveNumber == 1)
        {
            Fungus.Flowchart.BroadcastFungusMessage("RepairMonumentDialogue");
        }

        if (_waveNumber == 2)
        {
            Fungus.Flowchart.BroadcastFungusMessage("BuildablesDialogue");
        }

        if (_waveNumber == 5)
        {
            Invoke(nameof(EnterBossCutscene), 2f);
        }
    }

    void EnterBossCutscene()
    {
        _bossCutscene.SetActive(true);

    }

    public void ActivateDialogue(string message)
    {
        Fungus.Flowchart.BroadcastFungusMessage(message);
    }
}
