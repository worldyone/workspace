using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProcessingSlot : MonoBehaviour
{

    //　アイテム情報を表示するテキストUI
    private Text informationText;
    //　アイテムの名前を表示するテキストUIプレハブ
    [SerializeField]
    private GameObject itemSlotTitleUI;
    //　アイテム名を表示するテキストUIをインスタンス化した物を入れておく
    private GameObject itemSlotTitleUIInstance;
    //　自身のアイテムデータを入れておく
    public ItemData myItemData;

    //　スロットが非アクティブになったら削除
    void OnDisable()
    {
        if (itemSlotTitleUIInstance != null)
        {
            Destroy(itemSlotTitleUIInstance);
        }
        Destroy(gameObject);
    }

    //　アイテムのデータをセット
    public void SetItemData(ItemData itemData)
    {
        myItemData = itemData;
        //　アイテムのスプライトを設定
        transform.GetChild(0).GetComponent<Image>().sprite = myItemData.GetItemSprite();
    }

    void Start()
    {
        //　アイテムスロットの親の親からInformationゲームオブジェクトを探しTextコンポーネントを取得する
        informationText = transform.parent.parent.Find("Information").GetChild(0).GetComponent<Text>();
    }

    public void MouseOver()
    {
        if (itemSlotTitleUIInstance != null)
        {
            Destroy(itemSlotTitleUIInstance);
        }
        //　アイテムの名前を表示するUIを位置を調整してインスタンス化
        itemSlotTitleUIInstance = Instantiate<GameObject>(itemSlotTitleUI, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, transform.parent.parent);
        //　情報表示テキストに自身のアイテムの情報を表示
        informationText.text = myItemData.GetItemInformation();
    }

    public void MouseExit()
    {
        //　マウスポインタがアイテムスロットを出たらアイテム表示UIを削除
        if (itemSlotTitleUIInstance != null)
        {
            informationText.text = "";
            Destroy(itemSlotTitleUIInstance);
        }
    }
}
