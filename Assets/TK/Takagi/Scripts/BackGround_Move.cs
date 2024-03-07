// --------------------------------------------------------- 
// BackGround_Move.cs 
// 
// 作成日: 6/28
// 作成者: 髙木光汰
// ---------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 背景を移動させるスクリプト
/// </summary>

public class BackGround_Move: MonoBehaviour
{
	#region 変数

	/**"SerializeField" Inspectorから操作が可能,
	* 他のクラスからの書き換えを防止
	* 
	**/
	// ゲームオブジェクト、変数等の格納
	[SerializeField] private GameObject _player;
	[SerializeField] private const float RATE = 0.12f;
	[SerializeField] private Vector3 _startPlayerOffset;
	[SerializeField] private Vector3 _startCameraPos;

	#endregion

	#region メソッド

	private void Start()
	{
		// プレイヤーのスタート位置の取得
		_startPlayerOffset = _player.transform.position;
		// カメラのスタート位置の取得
		_startCameraPos = this.transform.position;
	}

	private void Update()
	{
		// プレイヤーに合わせて動かす処理
		Vector3 v = (_player.transform.position - _startPlayerOffset) * RATE;
		this.transform.position = _startCameraPos + v;
	}

	#endregion
}

