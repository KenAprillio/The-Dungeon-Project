using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    private EnemySpawner _enemySpawner;
    [SerializeField] private int _currentWave = 1;
    public bool IsEnabled = true;

    public string InteractionPrompt => _prompt;
    public bool isEnabled => IsEnabled;

    private void Start()
    {
        _enemySpawner = EnemySpawner.Instance;
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
            _currentWave++;
            IsEnabled = false;
            return true;
        }
    }
}
