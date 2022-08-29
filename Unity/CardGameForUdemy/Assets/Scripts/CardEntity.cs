using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEntity", menuName = "Create CardEntity")]
// カードデータそのもの
public class CardEntity : ScriptableObject
{
    public string cardName;
    public int hp;
    public int at;
    public int cost;
    public Sprite icon;
}
