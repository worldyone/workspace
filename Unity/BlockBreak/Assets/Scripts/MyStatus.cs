using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStatus : MonoBehaviour
{

    //　アイテムを持っているかどうかのフラグ
    private Dictionary<string, bool> itemFlags = new Dictionary<string, bool>();
    //　アイテムデータベース
    [SerializeField]
    private ItemDataBase itemDataBase;

    private void Start()
    {
        //　とりあえず全てのアイテムのフラグを作成
        foreach (var item in itemDataBase.GetItemDataList())
        {
            itemFlags.Add(item.GetItemName(), false);
        }
        //　とりあえず適当にアイテムを持っていることにする
        itemFlags["FlashLight"] = true;
        itemFlags["BroadSword"] = true;
        itemFlags["HandGun"] = true;
    }

    //　アイテムを所持しているかどうか
    public bool GetItemFlag(string itemName)
    {
        return itemFlags[itemName];
    }
}
