using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    // 見かけに関することを操作(view)
    CardModel model; // データに関することを操作(model)

    public void Init(int cardID)
    {
        model = new CardModel(cardID);
    }

}
