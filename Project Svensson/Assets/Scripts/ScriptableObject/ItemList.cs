using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
[System.Serializable]
public class ItemList : ScriptableObject
{
    public List<Item> primary = new List<Item>();
}
