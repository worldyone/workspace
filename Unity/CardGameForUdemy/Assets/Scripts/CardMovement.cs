using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform defaultParent;
    public bool isDraggable;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // カードのコストとPlayerのManaCostを比較
        CardController card = GetComponent<CardController>();

        // 手札でかつコストが足りる場合 or フィールドカードでかつ攻撃可能な場合、
        // ドラッグで移動可能
        isDraggable =
            (!card.model.isFieldCard && card.model.cost <= GameManager.instance.playerManaCost)
            || (card.model.isFieldCard && card.model.canAttack);

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

    public void SetCardTransform(Transform parentTransform)
    {
        defaultParent = parentTransform;
        transform.SetParent(defaultParent);
    }
}
