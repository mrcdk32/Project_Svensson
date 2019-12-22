using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public bool primary;
    public bool secondary;
    public bool isYellowKey;
    public bool isBlueKey;
    public bool isRedKey;
    public bool isGreenKey;
}
