using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform playerHandTransform;
    [SerializeField] GameObject cardPrefab;

    void Start()
    {
        CreateCard(playerHandTransform);
    }

    void CreateCard(Transform hand)
    {
        Instantiate(cardPrefab, playerHandTransform, false);
    }

}
