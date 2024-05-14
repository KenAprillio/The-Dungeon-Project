using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentInteract : MonoBehaviour, IInteractable, ISecondInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _secondPrompt;

    private EnemySpawner _enemySpawner;
    private TimeScaler _canvas;
    private MonumentHealthScript _monumentHealthScript;
    private AudioManager _audioManager;

    [SerializeField] private int _currentWave = 1;
    public bool IsEnabled = true;
    [SerializeField] private bool _isFirstTime = true;

    public string InteractionPrompt => _prompt;
    public string SecondInteractionPrompt => UpdateCost();
    public bool isEnabled => IsEnabled;


    private void Start()
    {
        _enemySpawner = EnemySpawner.Instance;
        _canvas = TimeScaler.Instance;
        _monumentHealthScript = GetComponent<MonumentHealthScript>();
        _audioManager = AudioManager.Instance;
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
            _enemySpawner.SpawnEnemies(_currentWave);
            _enemySpawner.IsWaveOngoing = true;
            Debug.Log("Start Wave!");

            string mainText = "Gelombang " + _currentWave + " sedang berlangsung!";
            float amountOfEnemies = _enemySpawner.Waves[_currentWave - 1].RangeSpawnAmount + _enemySpawner.Waves[_currentWave - 1].TankSpawnAmount + _enemySpawner.Waves[_currentWave - 1].FighterSpawnAmount;
            string subText = "Kalahkan " + amountOfEnemies + " musuh!";
            _canvas.UpdateMainObjective(mainText, subText);

            if (_isFirstTime)
            {
                _canvas.ShowAttackTutor();
                _isFirstTime = false;
            }

            _audioManager.PlayMusic(_audioManager.mainMusic);

            _currentWave++;
            IsEnabled = false;
            return true;
        }
    }

    public bool SecondInteract(Interactor interactor)
    {
        var player = interactor.GetComponent<PlayerHealthManager>();

        if (player.PlayerKredits < _monumentHealthScript.FinalFixCost)
        {
            return false;
        }
        else
        {
            bool isFixable = _monumentHealthScript.MonumentHealthFix();
            if (isFixable)
            {
                player.PlayerKredits -= _monumentHealthScript.FinalFixCost;
                player.UpdateKredits();
            }
            Debug.Log("Is the monument fixable? " + isFixable);
            return isFixable;
        }
    }

    string UpdateCost()
    {
        string newPrompt = _secondPrompt.Replace(
            "FIX_AMOUNT", _monumentHealthScript.UpdateCost().ToString());
        return newPrompt;
    }
}
