using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    CardView view; // 見かけに関することを操作(view)
    public CardModel model; // データに関することを操作(model)
    public CardMovement movement; // 移動に関することを操作(movement)

    void Awake()
    {
        view = GetComponent<CardView>();
        movement = GetComponent<CardMovement>();
    }

    public void Init(int cardID, bool isPlayer)
    {
        model = new CardModel(cardID, isPlayer);
        view.Show(model);
    }

    public void CheckAlive()
    {
        if (model.isAlive)
        {
            view.Refresh(model);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Attack(CardController enemyCard)
    {
        model.Attack(enemyCard);
        SetCanAttack(false);
    }

    public void SetCanAttack(bool canAttack)
    {
        model.canAttack = canAttack;
        view.SetActiveSelectablePanel(canAttack);
    }

    public void OnField(bool isPlayer)
    {
        GameManager.instance.ReduceManaCost(model.cost, isPlayer);
        model.isFieldCard = true;
        if (model.ability == ABILITY.INIT_ATTACKABLE)
        {
            SetCanAttack(true);
        }

    }

}
