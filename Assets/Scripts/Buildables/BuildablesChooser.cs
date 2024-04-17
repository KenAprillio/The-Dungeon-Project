using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildablesChooser : MonoBehaviour
{
    [Header("Buildables Pricing")]
    public int TurretPrice;
    public int WallPrice;

    [Header("UI Stuff")]
    [SerializeField] private Button _turretButton;
    [SerializeField] private TMP_Text _turretPriceText;
    [SerializeField] private Button _wallButton;
    [SerializeField] private TMP_Text _wallPriceText;
    [SerializeField] private Animator _animator;

    [Header("Buildables Location")]
    public GameObject BuildablesLocation;

    [Header("Player Reference")]
    [SerializeField] private PlayerHealthManager _playerHealthManager;
    private void OnEnable()
    {
        CheckPrice();
        _animator.SetBool("IsChoosingBuildables", true);
    }

    private void CheckPrice()
    {
        int playerKredits = _playerHealthManager.PlayerKredits;

        bool ableToBuyTurret = (playerKredits >= TurretPrice) ? true : false;
        bool ableToBuyWall = (playerKredits >= WallPrice) ? true : false;

        UpdateBuildablesButton(ableToBuyTurret, ableToBuyWall);
    }

    private void UpdateBuildablesButton(bool buyTurret, bool buyWall)
    {
        _turretPriceText.text = "<b>"+ TurretPrice +"</b> Kredits";
        _wallPriceText.text = "<b>"+ WallPrice +"</b> Kredits";

        _turretButton.interactable = buyTurret;
        _wallButton.interactable = buyWall;
    }

    public void ChooseWalls()
    {
        // Grab player kredits here
        _playerHealthManager.PlayerKredits -= WallPrice;
        _playerHealthManager.UpdateKredits();

        // Spawn walls on the selected spot
        BuildablesLocation.transform.Find("Wall").gameObject.SetActive(true);
        BuildablesLocation.transform.Find("Cube").gameObject.SetActive(false);
        _animator.SetBool("IsChoosingBuildables", false);

        StartCoroutine(nameof(WaitBeforeDeactivating));
    }

    public void ChooseTurret()
    {
        // Grab player kredits here
        _playerHealthManager.PlayerKredits -= TurretPrice;
        _playerHealthManager.UpdateKredits();

        // Spawn turret on the selected spot
        BuildablesLocation.transform.Find("Turret").gameObject.SetActive(true);
        BuildablesLocation.transform.Find("Cube").gameObject.SetActive(false);
        _animator.SetBool("IsChoosingBuildables", false);

        StartCoroutine(nameof(WaitBeforeDeactivating));
    }

    IEnumerator WaitBeforeDeactivating()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    }
}
