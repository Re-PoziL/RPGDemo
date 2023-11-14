using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item",menuName ="Item/Item")]
public class Item : ScriptableObject
{
    public int itemId;
    public string itemName;
    [SerializeField]
    [TextArea]public string description;
    public Sprite itemSprite;
}
