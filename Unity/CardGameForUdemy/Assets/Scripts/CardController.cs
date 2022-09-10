using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    CardView view; // 見かけに関することを操作(view)
    public CardModel model; // データに関することを操作(model)
    public CardMovement movement; // 移動に関することを操作(movement)
    GameManager gameManager;

    void Awake()
    {
        view = GetComponent<CardView>();
        movement = GetComponent<CardMovement>();
        gameManager = GameManager.instance;
    }

    public void Init(int cardID, bool isPlayer)
    {
        model = new CardModel(cardID, isPlayer);
        view.Show(model);
    }

    public void CheckAlive()
    {
        if (model.isAlive)
            RefreshView();
        else
            Destroy(this.gameObject);
    }

    public void Attack(CardController enemyCard)
    {
        model.Attack(enemyCard);
        SetCanAttack(false);
    }

    public void Heal(CardController friendCard)
    {
        model.Heal(friendCard);
        friendCard.RefreshView();
    }

    public void RefreshView()
    {
        view.Refresh(model);
    }

    public void SetCanAttack(bool canAttack)
    {
        model.canAttack = canAttack;
        view.SetActiveSelectablePanel(canAttack);
    }

    public void OnField()
    {
        GameManager.instance.ReduceManaCost(model.cost, model.isPlayerCard);
        model.isFieldCard = true;
        if (model.ability == ABILITY.INIT_ATTACKABLE)
        {
            SetCanAttack(true);
        }
    }

    public void UseSpellTo(CardController target)
    {
        switch (model.spell)
        {
            case SPELL.DAMAGE_ENEMY_CARD:
                // 特定の敵を攻撃する
                Attack(target);
                target.CheckAlive();
                break;
            case SPELL.DAMAGE_ENEMY_CARDS:
                // 相手フィールドの全てのカードに攻撃する
                CardController[] enemyCards = gameManager.GetEnemyFieldCards(this.model.isPlayerCard);
                foreach (CardController enemyCard in enemyCards)
                {
                    Attack(enemyCard);
                }
                foreach (CardController enemyCard in enemyCards)
                {
                    enemyCard.CheckAlive();
                }
                break;
            case SPELL.DAMAGE_ENEMY_HERO:
                gameManager.AttackToHero(this);
                break;
            case SPELL.HEAL_FRIEND_CARD:
                Heal(target);
                break;
            case SPELL.HEAL_FRIEND_CARDS:
                CardController[] friendCards = gameManager.GetFriendFieldCards(this.model.isPlayerCard);
                foreach (CardController friendCard in friendCards)
                {
                    Heal(friendCard);
                }
                break;
            case SPELL.HEAL_HERO_CARD:
                break;
            case SPELL.NONE:
                return;
        }

        Destroy(this.gameObject);
    }

}
