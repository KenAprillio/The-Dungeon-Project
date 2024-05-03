using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    public static TimeScaler Instance;

    [Header("Component References")]
    private Animator _animator;

    [Header("Buildables UI Reference")]
    [SerializeField] private GameObject _buildablesUI;
    public bool IsBuildablesUIActive;
    [SerializeField] private GameObject _boonsUI;
    public bool IsBoonsUIActive;

    [Header("Objective UI Reference")]
    [SerializeField] private RectTransform _mainTextTransform;
    [SerializeField] private TMP_Text _mainText;
    [SerializeField] private RectTransform _subTextTransform;
    [SerializeField] private TMP_Text _subText;

    [Header("Pause Menu Reference")]
    [SerializeField] private GameObject _pauseMenu;

    [Header("Pause Time")]
    public bool IsPaused = false;
    public float SlowMoTransitionTime;

    [Header("Cameras References")]
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _storyCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void PauseGame()
    {
        IsPaused = !IsPaused;
        _pauseMenu.SetActive(IsPaused);
    }

    // ====================== OBJECTIVE UI ANIMATOR
    #region Objective UI

    public void ShowMainObjective(string text, string subText)
    {
        SetMainText(text);
        SetSubText(subText);
        LeanTween.move(_mainTextTransform, new Vector3(-276, -133, 0), .5f).setEaseOutQuart().setDelay(.5f);
    }

    public void HideMainObjective()
    {
        LeanTween.move(_mainTextTransform, new Vector3(282, -133, 0), .5f);
    }

    public void ShowSubObjective()
    {

    }

    public void HideSubObjective()
    {

    }

    public void SetMainText(string text)
    {
        _mainText.text = text;
    }

    public void SetSubText(string text)
    {
        _subText.text = text;
    }

    public void UpdateMainObjective(string text, string subText)
    {
        Action action = () => ShowMainObjective(text, subText);
        LeanTween.move(_mainTextTransform, new Vector3(282, -133, 0), .5f).setEaseInQuart().setOnComplete(action);
    }

    #endregion



    // ====================== BUILDABLES UI ANIMATOR
    #region BuildablesUI
    public void ActivateBuildablesUI()
    {
        StopCoroutine(nameof(DeactivateUI));

        _buildablesUI.SetActive(true);
        IsBuildablesUIActive = true;
    }

    public void DeactivateBuildablesUI()
    {
        _animator.SetBool("IsChoosingBuildables", false);
        IEnumerator coroutine = DeactivateUI(_buildablesUI);
        IsBuildablesUIActive = false;

        StartCoroutine(coroutine);
    }
    #endregion



    // ====================== BOONS UI ANIMATOR
    #region BoonsUI
    public void ActivateBoonsUI()
    {
        StopCoroutine(nameof(DeactivateUI));

        _boonsUI.SetActive(true);
        IsBoonsUIActive = true;
    }

    public void DeactivateBoonsUI()
    {
        _animator.SetBool("IsChoosingBoons", false);
        IEnumerator coroutine = DeactivateUI(_boonsUI);
        IsBoonsUIActive = false;

        StartCoroutine(coroutine);
    }

    #endregion

    // ====================== CAMERA OPERATOR
    #region Cameras
    public void ActivateStoryCamera()
    {

    }
    #endregion

    IEnumerator DeactivateUI(GameObject uiObject)
    {
        yield return new WaitForSeconds(.5f);
        uiObject.SetActive(false);
    }
}
