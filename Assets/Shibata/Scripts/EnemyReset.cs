using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReset : MonoBehaviour
{    
    private bool _reset = false;　// Enemyが元の位置へ戻る用の条件

    private GameObject _Enemy;　//　Enemy変数

    private Vector2 _enemyReset;　// Enemyの初期位置を設定するための変数

    private EnemysMove _enemyMove;　// スクリプト取得用の変数作成

    private GameObject playerobj; // Player取得用の変数

    [SerializeField] private GameObject _pauseObj = default;
    private PauseScript _pauseScript = default;

    void Start()
    {
        _enemyReset = this.transform.position;　// Enemyの初期位置設定
        _enemyMove = transform.root.gameObject.GetComponent<EnemysMove>();　// スクリプト取得
        playerobj = GameObject.FindGameObjectWithTag("Player"); //　Player取得

        _pauseScript = _pauseObj.GetComponent<PauseScript>();
    }

    void Update()
    {
        // 条件がfalseになっている場合元の位置へ
        if (_reset == false)
        {
            Invoke("Back", 3);
        }


    }
    private void Back()
    {
        _enemyMove.GoBack();
    }


    private void OnTriggerStay2D(Collider2D collider2D)
    {
        // PlayerがこのEnemyのxを基準としてどちらにいるのかによって方向転換
        if(playerobj.transform.position.x < this.transform.position.x)
        {
            _enemyMove.Right();
        }
        if (playerobj.transform.position.x > this.transform.position.x)
        {
            _enemyMove.Left();
        }


        //Playerが範囲に入っている場合EnemyMoveのControllerスクリプトを呼び出す
        if (collider2D.CompareTag("Player") && !_enemyMove.IsDamage && !_pauseScript.IsPause)
        {
            print("trigger");
            CancelInvoke();
            _enemyMove.Controller();
        }
    }
    private void OnTriggerExit2D(Collider2D collider2D)
    {

        // Playerが外に出たら条件をfalseに設定
        if (collider2D.CompareTag("Player"))
        {
            _reset = false;
        }
    }

}
