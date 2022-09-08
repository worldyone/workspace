using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform defaultParent;
    public bool isDraggable;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // カードのコストとPlayerのManaCostを比較
        CardController card = GetComponent<CardController>();

        // 自ターンで、かつ、
        // 自カードで、かつ、
        // 手札でかつコストが足りる、または
        // フィールドカードでかつ攻撃可能な場合、
        // ドラッグで移動可能
        isDraggable =
            (GameManager.instance.isPlayerTurn
            && card.model.isPlayerCard
            && (!card.model.isFieldCard && card.model.cost <= GameManager.instance.playerManaCost)
            || (card.model.isFieldCard && card.model.canAttack));

        if (!isDraggable)
        {
            return;
        }

        defaultParent = transform.parent;
        transform.SetParent(defaultParent.parent, false);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;

        transform.SetParent(defaultParent, false);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public IEnumerator MoveToField(Transform field)
    {
        // 一度、親をCanvasに変更する
        transform.SetParent(defaultParent.parent);

        // DOTweenでカードをフィールドに移動
        transform.DOMove(field.position, 0.25f);

        yield return new WaitForSeconds(0.25f);

        defaultParent = field;
        transform.SetParent(defaultParent);
    }

    public IEnumerator MoveToTarget(Transform target)
    {
        // 現在の位置と並びを取得
        Vector3 currentPosition = transform.position;
        int siblingIndex = transform.GetSiblingIndex();

        // 一度、親をCanvasに変更する
        transform.SetParent(defaultParent.parent);

        // DOTweenでカードをtargetに移動
        transform.DOMove(target.position, 0.25f);
        yield return new WaitForSeconds(0.25f);

        // 元の場所に戻る
        transform.DOMove(currentPosition, 0.25f);
        yield return new WaitForSeconds(0.25f);
        transform.SetParent(defaultParent);
        transform.SetSiblingIndex(siblingIndex);
    }

    void Start()
    {
        defaultParent = transform.parent;
    }
}
