using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform playerHandTransform;
    [SerializeField] CardController cardPrefab;

    void Start()
    {
        CreateCard(playerHandTransform);
    }

    void CreateCard(Transform hand)
    {
        CardController card = Instantiate(cardPrefab, playerHandTransform, false);
        card.Init(1);
    }

}
