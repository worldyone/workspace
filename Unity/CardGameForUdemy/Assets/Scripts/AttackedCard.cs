using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 攻撃される側
public class AttackedCard : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        /* 攻撃 */
        // 攻撃するカード(attacker)を選択
        CardController attacker = eventData.pointerDrag.GetComponent<CardController>();
        // 被攻撃カード(defender)を選択
        CardController defender = GetComponent<CardController>();
        // attackerとdefenderを戦わせる
        GameManager.instance.CardsBattle(attacker, defender);

    }
}
