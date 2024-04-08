using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoonSelector : MonoBehaviour
{
    public BoonsHolder Holder;
    [SerializeField] private BoonButton firstBoonButton;
    [SerializeField] private BoonButton secondBoonButton;

    public List<Boons> ObtainableBoons;
    private int _totalBoonWeight = 0;
    private int _tempBoonNumber = -1;

    [Header("Canvas Animator Stuff")]
    [SerializeField] private Animator _animator;

    [SerializeField] private TimeScaler _timeScaler;


    private void Start()
    {
        // Get all the boon weights and sum it all
        for (int i = 0; i < ObtainableBoons.Count; i++)
        {
            _totalBoonWeight += ObtainableBoons[i].Weight;
        }
    }

    private void OnEnable()
    {
        // Pause the game when choosing boons
        _timeScaler.IsPaused = true;

        _animator.SetBool("IsChoosingBoons", true);

        // Pick first boon to show
        Boons firstBoon = PickRandomBoon();
        firstBoonButton.CurrentBoon = firstBoon;

        // Pick second boon to show, if same as first boon, pick again
        Boons secondBoon = PickRandomBoon();
        secondBoonButton.CurrentBoon = secondBoon;

        firstBoonButton.UpdateButton();
        secondBoonButton.UpdateButton();

    }

    public void SelectedBoon()
    {
        // Trigger UI Animation, reset timescale back to 1, reset boon temp number, and enables the monument to interactable
        _animator.SetBool("IsChoosingBoons", false);
        _timeScaler.IsPaused = false;
        _tempBoonNumber = -1;
        EnemySpawner.Instance.IsWaveOngoing = false;

        StartCoroutine(nameof(DisableThis));
    }

    // Timer to deactivate gameobject after choosing boon
    IEnumerator DisableThis()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }


    private Boons PickRandomBoon()
    {
        // Pick a random number to calculate the boon weight
        int randomPickWeight = Random.Range(0, _totalBoonWeight);
        int totalBoons = ObtainableBoons.Count;
        for (int i = 0; i < totalBoons; i++)
        {
            int randomBoon = Random.Range(0, totalBoons);
            // Check if the choosen boon is already picked
            if (randomBoon != _tempBoonNumber)
            {
                // Calculate the boon weight
                randomPickWeight -= ObtainableBoons[randomBoon].Weight;
                if (randomPickWeight < 0)
                {
                    // if calculated boon weight is below 0, then pick that boon
                    _tempBoonNumber = randomBoon;
                    return ObtainableBoons[randomBoon];
                }
            }
        }

        return ObtainableBoons[0];
    }

    // Remove boon to prevent being picked again in the future
    public void DeleteBoonFromList(Boons boonToDelete)
    {
        ObtainableBoons.Remove(boonToDelete);
    }
}
