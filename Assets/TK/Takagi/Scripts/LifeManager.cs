// --------------------------------------------------------- 
// LifeManager.cs 
// 
// 作成日: 6/28
// 作成者: 髙木光汰
// ---------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    #region 変数 
    /** "SerializeField" Inspectorから操作が可能,
     * 他のクラスからの書き換えを防止
     * 
     **/

    [SerializeField] private GameObject _icon;
    // ライフポイント用の配列
    [SerializeField] private GameObject[] _hearts;
    // ライフポイントの数用の変数
    [SerializeField] private int _hpValue = default;

    /** 定数とは、値が固定され
     * 変更することができない値
     **/

    // カウント用の定数
    private const int MAX_COUNT = 2;
    #endregion

    #region メソッド

    /// <summary>
    /// <para>UpdateLife</para>
    /// <para>ライフポイント減少処理</para>
    /// </summary>
    /// 
    public void UpdateLife() 
    {
        // 配列の中のライフポイントを非表示
        _hearts[_hpValue].SetActive(false);

        /** 
         * ライフポイントの現在格納されている場所 < ライフポイントの合計数
         * ライフポイントの現在格納されている場所 = 配列番号(0,1,2)
         * ライフポイントの合計数 = 3
         * 
         **/

        if (_hpValue < _hearts.Length) 
        {
            // ライフポイントの配列を動かす
            _hpValue++;

            /** 
             * ライフポイントの現在格納されている場所 < MAX_COUNT(2)
             * ライフポイントの現在格納されている場所 = 配列番号(0,1,2)
             * 
             **/


            if (_hpValue == MAX_COUNT)
            {
                _icon.SetActive(true);
            }
            if (_hpValue > MAX_COUNT) 
            {
                // ゲームオーバーシーンに遷移
                SceneManager.LoadScene("OverScene");
            }
        } 

    }

    #endregion
}
