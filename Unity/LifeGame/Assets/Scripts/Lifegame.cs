using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifegame : MonoBehaviour
{
    public Material Living;
    public Material Dead;
    public GameObject Cell;

    // MeshRenderer配列の横・縦の数
    int Width = 80;
    int Height = 50;

    // 参照用、作業用セルの横・縦サイズ
    int RefWidth;
    int RefHeight;

    MeshRenderer[] CellsMR;
    bool[] RefCells;
    bool[] WCells;

    // Start is called before the first frame update
    void Start()
    {
        this.AllocArray();
        this.InitArray();

        StartCoroutine("GameStart");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.RandomDot();
        }
    }

    /// ランダムにドットを打つ
    private void RandomDot()
    {
        // ドット数
        int dotNum = (this.Width * this.Height) / 4;

        for (int n = 0; n < dotNum; n++)
        {
            // 横と縦の座標をランダムで取得
            int px = UnityEngine.Random.Range(0, this.Width);
            int py = UnityEngine.Random.Range(0, this.Height);

            // 参照用セルに合うように位置を計算
            int p = (py + 1) * this.RefWidth + (px + 1);

            // 参照用セルを更新(既にtrueでも上書きする)
            this.RefCells[p] = true;
        }

        // 表示を更新
        this.RenewCells();

    }

    /// 配列の確保
    private void AllocArray()
    {
        // MeshRenderer配列の総数
        int size = this.Width * this.Height;

        // 門番
        this.RefWidth = this.Width + 2;
        this.RefHeight = this.Height + 2;

        // 参照用セルの総数
        int refsize = this.RefWidth * this.RefHeight;

        // MeshRenderer、参照用、作業用の配列を確保
        this.CellsMR = new MeshRenderer[size];
        this.RefCells = new bool[refsize];
        this.WCells = new bool[refsize];
    }

    /// 配列の初期化
    private void InitArray()
    {
        // prefabのMeshRendererをMeshRenderer配列に格納する
        for (int h = 0; h < this.Height; h++)
        {
            for (int w = 0; w < this.Width; w++)
            {
                // MeshRenderer配列の位置
                int n = h * this.Width + w;

                // Prefabの座標 0.5 を足してるのはCubeの角を頂点に合わせるため
                Vector3 position = new Vector3(w + 0.5f, 0, h + 0.5f);

                // MeshRenderer配列に、複製したPrefabのMeshRendererを格納
                this.CellsMR[n] = ((GameObject)Instantiate(this.Cell, position, Quaternion.identity)).GetComponent<MeshRenderer>();
            }
        }
    }

    /// ライフゲームの開始
    public IEnumerator GameStart()
    {
        while (true)
        {
            // 次世代を計算
            this.Next();
            // 表示を更新
            this.RenewCells();
            // 指定秒数の間、処理を中断する
            yield return new WaitForSeconds(0.5f);
        }
    }

    /// 参照用セルを参照して表示を更新
    private void RenewCells()
    {
        for (int h = 0; h < this.Height; h++)
        {
            for (int w = 0; w < this.Width; w++)
            {
                // MeshRenderer配列の位置
                int n = h * this.Width + w;

                // 参照用セル配列の位置
                int p = (h + 1) * this.RefWidth + (w + 1);

                // trueなら生、falseなら死
                if (this.RefCells[p])
                {
                    this.CellsMR[n].material = this.Living;
                }
                else
                {
                    this.CellsMR[n].material = this.Dead;
                }
            }
        }
    }

    private void Next()
    {
        for (int h = 1; h < this.RefHeight - 1; h++)
        {
            for (int w = 1; w < this.RefWidth - 1; w++)
            {
                // 参照用セルの位置
                int pos = h * this.RefWidth + w;
                // 座標(w,h)周囲の生きているセルの数を得る
                int sum = this.SumAround(w, h);

                if (sum == 3)
                {
                    // 誕生
                    this.WCells[pos] = true;
                }
                else if (sum == 2)
                {
                    // 維持
                    this.WCells[pos] = this.RefCells[pos];
                }
                else
                {
                    // 死亡
                    // sum <= 2 || sum >= 4
                    this.WCells[pos] = false;
                }
            }
        }

        // 作業用セルの内容を参照用セルにコピーする
        this.WCells.CopyTo(this.RefCells, 0);

        // 作業用セルの配列を規定値にする
        System.Array.Clear(this.WCells, 0, this.WCells.Length);
    }

    /// 周囲8近傍の生きているセルの数を求める
    private int SumAround(int w, int h)
    {
        int sum = 0;

        int up = (h + 1) * this.RefWidth;
        int ce = h * this.RefWidth;
        int bo = (h - 1) * this.RefWidth;

        // 8近傍を調べる
        sum += this.BoolToInto(this.RefCells[up + (w - 1)]);
        sum += this.BoolToInto(this.RefCells[up + w]);
        sum += this.BoolToInto(this.RefCells[up + (w + 1)]);
        sum += this.BoolToInto(this.RefCells[ce + (w - 1)]);
        sum += this.BoolToInto(this.RefCells[ce + (w + 1)]);
        sum += this.BoolToInto(this.RefCells[bo + (w - 1)]);
        sum += this.BoolToInto(this.RefCells[bo + w]);
        sum += this.BoolToInto(this.RefCells[bo + (w + 1)]);

        return sum;
    }

    /// trueなら1を、falseなら0を返す
    private int BoolToInto(bool b)
    {
        if (b) return 1;
        else return 0;
    }
}
