using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int WaveNumber;
        public float FighterSpawnAmount;
        public float RangeSpawnAmount;
        public float TankSpawnAmount;
    }
    
    public List<Wave> waves;
    public Transform[] waveSource;
    [SerializeField] EnemyPooler _enemyPooler;

    private IEnumerator coroutine;

    private void Start()
    {
        _enemyPooler = EnemyPooler.Instance;

    }


    private void OnEnable()
    {
        SpawnEnemies(1, 3);

    }


    public void SpawnEnemies(int waveNumber, float spawnSpeed)
    {
        foreach (Wave wave in waves)
        {
            if (wave.WaveNumber == waveNumber)
            {
                Debug.Log("Spawn!");

                List<GameObject> waveEnemies = new List<GameObject>();

                for (int i = 0; i < wave.FighterSpawnAmount; i++)
                {
                    GameObject obj = _enemyPooler.SpawnFromPool("Fighters", waveSource[Random.Range(0, waveSource.Length)].position, Quaternion.identity, false);
                    waveEnemies.Add(obj);
                }

                for (int i = 0; i < wave.RangeSpawnAmount; i++)
                {
                    GameObject obj = _enemyPooler.SpawnFromPool("Ranges", waveSource[Random.Range(0, waveSource.Length)].position, Quaternion.identity, false);
                    waveEnemies.Add(obj);
                }

                for (int i = 0; i < wave.TankSpawnAmount; i++)
                {
                    GameObject obj = _enemyPooler.SpawnFromPool("Tanks", waveSource[Random.Range(0, waveSource.Length)].position, Quaternion.identity, false);
                    waveEnemies.Add(obj);
                }

                coroutine = SpawnPeriodically(waveEnemies, spawnSpeed);
                StartCoroutine(coroutine);
            }
        }
    }

    public IEnumerator SpawnPeriodically(List<GameObject> enemiesToSpawn, float spawnSpeed)
    {
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            yield return new WaitForSeconds(spawnSpeed);

            int randomSpawn = Random.Range(0, enemiesToSpawn.Count);
            enemiesToSpawn[randomSpawn].SetActive(true);
            enemiesToSpawn.RemoveAt(randomSpawn);
        }

    }
}
