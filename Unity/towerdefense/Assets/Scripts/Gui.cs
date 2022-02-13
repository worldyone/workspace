using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gui
{
    // Wave数テキスト
    TextObj _txtWave;
    // 所持金テキスト
    TextObj _txtMoney;
    // コストテキスト
    TextObj _txtCost;
    // 購入ボタン
    ButtonObj _btnBuy;

    /// コンストラクタ
    public Gui()
    {
        // Wave数
        _txtWave = MyCanvas.Find<TextObj>("TextWave");
        // 所持金テキスト
        _txtMoney = MyCanvas.Find<TextObj>("TextMoney");
        // コストテキスト
        _txtCost = MyCanvas.Find<TextObj>("TextCost");
        _txtCost.Label = "";
        // 購入ボタン
        _btnBuy = MyCanvas.Find<ButtonObj>("ButtonBuy");
    }

    public void Update(GameMgr.eSelMode selMode)
    {
        _txtWave.SetLabelFormat("Wave: {0}", Global.Wave);
        _txtMoney.SetLabelFormat("Money: ${0}", Global.Money);

        // 生産コストを取得する
        int cost = Cost.TowerProduction();
        _txtCost.Label = "";
        if (selMode == GameMgr.eSelMode.Buy)
        {
            // 購入モードのみテキストを設定する
            _txtCost.SetLabelFormat("(cost ${0})", cost);
        }

        // 購入ボタンを押せるかどうかチェック
        _btnBuy.Enabled = (Global.Money >= cost);

        // 購入コストを表示する
        _btnBuy.FormatLabel("Buy (${0})", cost);

        // ライフ表示
        for (int i = 0; i < Global.LIFE_MAX; i++)
        {
            bool b = (Global.Life > i);
            MyCanvas.SetActive("ImageLife" + i, b);
        }
    }
}
