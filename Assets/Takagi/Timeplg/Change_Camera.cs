// --------------------------------------------------------- 
// Change_Camera.cs 
// 
// 作成日: 7/10
// 作成者: 髙木光汰
// ---------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Camera : MonoBehaviour
{
    #region 変数

    /**"SerializeField" Inspectorから操作が可能,
     * 他のクラスからの書き換えを防止
     * 
     **/
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _camera2;

    #endregion

    #region メソッド
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Playerタグが領域内に侵入したときに実行
        if (collision.CompareTag("Player"))
        {
            // カメラの表示、非表示
            _camera.SetActive(false);
            _camera2.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Playerタグが領域内に侵入したときに実行
        if (collision.CompareTag("Player"))
        {
            // カメラの表示、非表示
            _camera.SetActive(true);
            _camera2.SetActive(false);
        }
    }

    #endregion
}
