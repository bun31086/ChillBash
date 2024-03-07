// --------------------------------------------------------- 
// PlayerLife.cs 
// 
// 作成日: 6/28
// 作成者: 髙木光汰
// ---------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    #region 変数

    /**"SerializeField" Inspectorから操作が可能,
     * 他のクラスからの書き換えを防止
     * 
     **/
    [SerializeField] private GameObject _lifePanel;

    // LifeManagerScript用の変数
    private LifeManager _lifeManager;

    #endregion

    #region メソッド

    void Start()
    {
        // _lifePanelの中にあるLifeManagerScriptを取得して変数に格納する
        _lifeManager = _lifePanel.GetComponent<LifeManager>();
    }

    public void LiftManager()
    {            
        // LifeManagerScriptのUpdateLife()を実行
        _lifeManager.UpdateLife();
    }
    #endregion
}
