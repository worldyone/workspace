using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カードデータとその処理
public class CardModel : MonoBehaviour
{
    public string name;
    public int hp;
    public int at;
    public int cost;
    public Sprite icon;
    public ABILITY ability;
    public bool isAlive;
    public bool canAttack;
    public bool isFieldCard;
    public bool isPlayerCard;

    public CardModel(int cardID, bool isPlayer)
    {
        CardEntity cardEntity = Resources.Load<CardEntity>("CardEntityList/Card" + cardID);
        name = cardEntity.name;
        hp = cardEntity.hp;
        at = cardEntity.at;
        cost = cardEntity.cost;
        icon = cardEntity.icon;
        ability = cardEntity.ability;
        isAlive = true;
        isPlayerCard = isPlayer;
    }

    void Damage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            hp = 0;
            isAlive = false;
        }
    }

    public void Attack(CardController card)
    {
        card.model.Damage(at);
    }
}
