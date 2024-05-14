using Fungus;
using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Win & Lose UI References")]
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _loseUI;


    [Header("Pause Menu Reference")]
    [SerializeField] private GameObject _pauseMenu;

    [Header("Pause Time")]
    public bool IsPaused = false;
    public float SlowMoTransitionTime;

    [Header("Cameras References")]
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _storyCamera;

    [Header("Tutorial UIs")]
    [SerializeField] private GameObject _moveTutor;
    [SerializeField] private GameObject _upgradeTutor;
    [SerializeField] private GameObject _waveTutor;
    [SerializeField] private GameObject _attackTutor;
    [SerializeField] private GameObject _buildTutor;

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

    public void ShowMainObjective()
    {
        LeanTween.move(_mainTextTransform, new Vector3(-276, -133, 0), .5f).setEaseOutQuart().setDelay(.5f);
    }
    public void ShowMainObjective(string text, string subText)
    {
        SetMainText(text);
        SetSubText(subText);
        LeanTween.move(_mainTextTransform, new Vector3(-276, -133, 0), .5f).setEaseOutQuart().setDelay(.5f);
    }

    public void ShowSubObjective(string subText)
    {
        SetSubText(subText);
        LeanTween.move(_subTextTransform, new Vector3(228, -20, 0), .5f).setEaseOutQuart().setDelay(.5f);
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

    public void UpdateSubObjective(string subText)
    {
        Action action = () => ShowSubObjective(subText);
        LeanTween.move(_subTextTransform, new Vector3(781, -20, 0), .5f).setEaseInQuart().setOnComplete(action);
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

    // ====================== WIN LOSE PANEL
    #region Win Lose

    public void ActivateLosePanel()
    {
        _loseUI.SetActive(true);
    }

    public void ActivateWinPanel()
    {
        _winUI.SetActive(true);
    }

    #endregion

    // ====================== TUTORIALS PANEL
    #region Tutorials
    public void ShowMovementTutor()
    {
        IsPaused = true;
        _moveTutor.SetActive(true);
        AnimateTutorialIn(_moveTutor.GetComponent<RectTransform>());
    }

    public void ShowUpgradeTutor()
    {
        IsPaused = true;
        _upgradeTutor.SetActive(true);
        AnimateTutorialIn(_upgradeTutor.GetComponent<RectTransform>());
    }

    public void ShowWaveTutor()
    {
        IsPaused = true;
        _waveTutor.SetActive(true);
        AnimateTutorialIn(_waveTutor.GetComponent<RectTransform>());
    }

    public void ShowAttackTutor()
    {
        IsPaused = true;
        _attackTutor.SetActive(true);
        AnimateTutorialIn(_attackTutor.GetComponent<RectTransform>());
    }

    public void ShowBuildTutor()
    {
        IsPaused = true;
        _buildTutor.SetActive(true);
        AnimateTutorialIn(_buildTutor.GetComponent<RectTransform>());
    }

    void AnimateTutorialIn(RectTransform tutorialPanel)
    {
        LeanTween.move(tutorialPanel, new Vector3(0, 0, 0), .75f).setEaseOutQuart().setIgnoreTimeScale(true);
    }

    public void AnimateTutorialOut(RectTransform tutorialPanel)
    {
        IsPaused = false;
        Action action = () => DeactivateUI(tutorialPanel.gameObject);
        LeanTween.move(tutorialPanel, new Vector3(0, -1000, 0), .75f).setEaseOutQuart().setIgnoreTimeScale(true).setOnComplete(action);

    }

    #endregion


    // ====================== SCENE LOADING
    #region Scene Loading

    public void ResetScene()
    {
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void BackToMenu()
    {
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public IEnumerator LoadNextScene(int sceneIndex)
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneIndex);

        while (!loadScene.isDone)
        {
            yield return null;
        }
    }

    #endregion




    IEnumerator DeactivateUI(GameObject uiObject)
    {
        yield return new WaitForSeconds(.5f);
        uiObject.SetActive(false);
    }

}
