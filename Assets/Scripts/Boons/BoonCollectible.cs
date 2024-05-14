using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonCollectible : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private bool _isEnabled;
    private Animator _animator;

    public bool isEnabled => _isEnabled;
    public string InteractionPrompt => _prompt;

    [SerializeField] private GameObject _boonChooser;
    [SerializeField] private GameObject _boonObject;

    [SerializeField] private bool _isFirstTime;
    private TimeScaler _timeScaler;

    GameObject _player;

    private void Awake()
    {
        _timeScaler = TimeScaler.Instance;
        _animator = GetComponent<Animator>();
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Choose a boon!");

        _animator.SetBool("isOpen", true);
        StartCoroutine(ChooseBool());
        //gameObject.SetActive(false);

        _player = interactor.gameObject;
        _player.GetComponent<PlayerStateMachine>().enabled = false;

        


        return true;
    }

    IEnumerator ChooseBool()
    {
        yield return new WaitForSeconds(1.5f);
        _timeScaler.ActivateBoonsUI();
    }

    private void OnEnable()
    {
        _animator.SetBool("isOpen", false);
        LeanTween.moveLocal(_boonObject, new Vector3(0, 0, 0), 1.5f).setEaseOutBounce();

    }

    private void OnDisable()
    {
        _boonObject.transform.localPosition = new Vector3(0, 35, 0);

        _player.GetComponent<PlayerStateMachine>().enabled = true;

        if (_isFirstTime)
        {
            Fungus.Flowchart.BroadcastFungusMessage("StartingWave");
            _isFirstTime = false;
        }
    }
}

    
