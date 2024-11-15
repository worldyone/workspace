using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreateSlotScript : MonoBehaviour
{

    //　アイテム情報のスロットプレハブ
    [SerializeField]
    private GameObject slot;
    //　主人公のステータス
    [SerializeField]
    private MyStatus myStatus;
    //　アイテムデータベース
    [SerializeField]
    private ItemDataBase itemDataBase;

    //　アクティブになった時
    void OnEnable()
    {
        //　アイテムデータベースに登録されているアイテム用のスロットを全作成
        CreateSlot(itemDataBase.GetItemDataList());
    }

    //　アイテムスロットの作成
    public void CreateSlot(List<ItemData> itemList)
    {

        int i = 0;

        Debug.Log(itemList.Count);

        foreach (var item in itemList)
        {
            if (myStatus.GetItemFlag(item.GetItemName()))
            {
                //　スロットのインスタンス化
                var instanceSlot = Instantiate<GameObject>(slot, transform);
                //　スロットゲームオブジェクトの名前を設定
                instanceSlot.name = "ItemSlot" + i++;
                //　Scaleを設定しないと0になるので設定
                instanceSlot.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                //　アイテム情報をスロットのProcessingSlotに設定する
                instanceSlot.GetComponent<ProcessingSlot>().SetItemData(item);
            }
        }
    }
}
