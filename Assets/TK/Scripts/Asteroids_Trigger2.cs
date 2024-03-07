using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがエリア内に入ると隕石を落とさせるスクリプト
/// </summary>

public class Asteroids_Trigger2 : MonoBehaviour
{
    //ゲームオブジェクトの格納
    [SerializeField] private GameObject _asteroids;
    [SerializeField] private GameObject _asteroids2;
    [SerializeField] private GameObject _asteroids3;
    [SerializeField] private GameObject _asteroids4;

    /*  OnTriggerEnter,物体がすり抜けた瞬間の一度きりのみ呼ばれる。
 *  物体をすり抜けさせるには
 * 「BoxCollider」の「Is Trigger」にチェックを入れる。
 */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Playerタグが触れたときに実行
        if (collision.CompareTag("Player"))
        {
            //隕石の表示
            _asteroids.SetActive(true);
            _asteroids2.SetActive(true);
            _asteroids3.SetActive(true);
            _asteroids4.SetActive(true);
        }    
    }


    /*OnTriggerExit,物体が通り抜け終えた時に呼ばれる。
 * 「BoxCollider」の「Is Trigger」にチェックを入れる。
 */
    void OnTriggerExit2D(Collider2D other)
    {
        //エリア外に出たらエリア自体を非表示
        this.gameObject.SetActive(false);
    }

}
