using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDataBase : MonoBehaviour
{

    //　アイテムの種類
    public enum ItemType
    {
        Skill,
        Other
    }
    //　アイテムデータのリスト
    public List<ItemData> itemDataList = new List<ItemData>();

    void Start()
    {
        //　アイテムの全情報を作成
        itemDataList.Add(new ItemData(Resources.Load("mahoujin-remove-back", typeof(Sprite)) as Sprite, "skill1", ItemType.Skill, "炎・水・地：弾を複製する"));
        itemDataList.Add(new ItemData(Resources.Load("mahoujin-remove-back", typeof(Sprite)) as Sprite, "skill2", ItemType.Skill, "炎・炎・炎：パドルのスピードが早くなる"));
        itemDataList.Add(new ItemData(Resources.Load("mahoujin-remove-back", typeof(Sprite)) as Sprite, "skill3", ItemType.Skill, "水・水・水：パドルが長くなる"));
        itemDataList.Add(new ItemData(Resources.Load("mahoujin-remove-back", typeof(Sprite)) as Sprite, "skill4", ItemType.Skill, "地・地・地：パドルのスピードが早くなる"));
        itemDataList.Add(new ItemData(Resources.Load("mahoujin-remove-back", typeof(Sprite)) as Sprite, "skill5", ItemType.Skill, "炎・炎・水：パドルのスピードが早くなる"));
        itemDataList.Add(new ItemData(Resources.Load("mahoujin-remove-back", typeof(Sprite)) as Sprite, "skill6", ItemType.Skill, "炎・炎・地：パドルのスピードが早くなる"));
        itemDataList.Add(new ItemData(Resources.Load("mahoujin-remove-back", typeof(Sprite)) as Sprite, "skill7", ItemType.Skill, "水・水・地：パドルのスピードが早くなる"));
        itemDataList.Add(new ItemData(Resources.Load("mahoujin-remove-back", typeof(Sprite)) as Sprite, "skill8", ItemType.Skill, "水・水・炎：パドルのスピードが早くなる"));
        itemDataList.Add(new ItemData(Resources.Load("mahoujin-remove-back", typeof(Sprite)) as Sprite, "skill9", ItemType.Skill, "地・地・炎：パドルのスピードが早くなる"));
        itemDataList.Add(new ItemData(Resources.Load("mahoujin-remove-back", typeof(Sprite)) as Sprite, "skill10", ItemType.Skill, "地・地・水：パドルのスピードが早くなる"));

    }
    //　全アイテムデータを返す
    public List<ItemData> GetItemDataList()
    {
        return itemDataList;
    }
    //　個々のアイテムデータを返す
    public ItemData GetItemData(string itemName)
    {
        foreach (var item in itemDataList)
        {
            if (item.GetItemName() == itemName)
            {
                return item;
            }
        }
        return null;
    }
}
