using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boons : ScriptableObject
{
    public string BoonName;
    public string BoonDescription;
    public int Weight;
    public Sprite BoonImage;

    public virtual void ActivateOnStart(GameObject parent) { }
    public virtual void Activate(GameObject parent) { }
}
