// --------------------------------------------------------- 
// Count_Timer.cs 
// 
// 作成日: 7/13
// 作成者: 髙木光汰
// ---------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Count_Timer : MonoBehaviour
{
    #region 変数

    /**"SerializeField" Inspectorから操作が可能,
     * 他のクラスからの書き換えを防止
     * 
     **/

    // タイマー表示用テキスト変数
    [SerializeField] private TMPro.TMP_Text _timeText;

    // 前のUpdateの時の秒数
    public float _oldSeconds = default;

    // 制限時間の秒数
    public static float _countdownSeconds = default;


    private PauseScript _pauseScript = default;
    [SerializeField] private GameObject _pauseObj = default;

    private bool _timerEnd = false;

    public bool TimerEnd { get => _timerEnd; set => _timerEnd = value; }

    #endregion

    #region メソッド
    void Start()
    {
        // 制限時間となる秒数を格納
        _countdownSeconds = 180;

        /** 比較用の秒数を格納
         *  ※同じ値を格納すること
         **/
        _oldSeconds = 180f;

        // テキストコンポーネントの取得
        _timeText = GetComponent<TMP_Text>();

        _pauseScript = _pauseObj.GetComponent<PauseScript>();
    }


    void Update()
    {
        // 制限時間が0秒になるまで実行
        if (_countdownSeconds >= 0f)
        {

            if (!_pauseScript.IsPause)
            {
                //　カウントダウンさせる
                _countdownSeconds -= Time.deltaTime;
            }
        }

        // 値が変わった時、テキストUIを都度更新
        if ((int)_countdownSeconds != (int)_oldSeconds)
        {

            // 秒数の表示
            _timeText.text = ((int)_countdownSeconds).ToString("000");
        }

        // 比較対象の更新
        _oldSeconds = _countdownSeconds;

        // 秒数が0秒になるまで実行
        if (_countdownSeconds <= 0f)
        {
            TimerEnd = true;
        }

    }
    #endregion
}
