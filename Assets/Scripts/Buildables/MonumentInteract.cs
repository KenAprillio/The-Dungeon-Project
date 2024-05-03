using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    private EnemySpawner _enemySpawner;
    private TimeScaler _canvas;
    [SerializeField] private int _currentWave = 1;
    public bool IsEnabled = true;

    public string InteractionPrompt => _prompt;
    public bool isEnabled => IsEnabled;

    private void Start()
    {
        _enemySpawner = EnemySpawner.Instance;
        _canvas = TimeScaler.Instance;
    }

    public bool Interact(Interactor interactor)
    {
        if (_enemySpawner.IsWaveOngoing)
        {
            Debug.Log("Wave is still running!");
            return false;
        }
        else
        {
            _enemySpawner.SpawnEnemies(_currentWave, 3);
            _enemySpawner.IsWaveOngoing = true;
            Debug.Log("Start Wave!");

            string mainText = "Gelombang " + _currentWave + " sedang berlangsung!";
            float amountOfEnemies = _enemySpawner.Waves[_currentWave - 1].RangeSpawnAmount + _enemySpawner.Waves[_currentWave - 1].TankSpawnAmount + _enemySpawner.Waves[_currentWave - 1].FighterSpawnAmount;
            string subText = "Kalahkan " + amountOfEnemies + " musuh!";
            _canvas.UpdateMainObjective(mainText, subText);

            _currentWave++;
            IsEnabled = false;
            return true;
        }
    }
}
