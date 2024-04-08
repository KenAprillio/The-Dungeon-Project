using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonsHolder : MonoBehaviour
{
    [Header("List to store player acquired boons")]
    public List<Boons> BoonsList;

    [Header("UI Things")]
    [SerializeField] private GameObject _boonSelector;

    private bool _isNearBoon;

}
