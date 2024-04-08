using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoonButton : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text _boonName;
    [SerializeField] private TMP_Text _boonDesc;

    [Header("Boon Stuff")]
    [SerializeField] private BoonSelector _boonSelector;
    public Boons CurrentBoon;

    // Update Boon Button Texts
    public void UpdateButton()
    {
        _boonName.text = CurrentBoon.BoonName;
        _boonDesc.text = CurrentBoon.BoonDescription;
    }

    // Apply Boon to Player when clicked
    public void ApplyBoon()
    {
        _boonSelector.Holder.BoonsList.Add(CurrentBoon);
        CurrentBoon.ActivateOnStart(_boonSelector.Holder.gameObject);

        _boonSelector.DeleteBoonFromList(CurrentBoon);
    }
}
