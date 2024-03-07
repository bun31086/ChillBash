using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 隕石を爆破させるスクリプト
/// </summary>


public class AsteroidsBlast : MonoBehaviour
{
    //ゲームオブジェクト等の格納
    [SerializeField] private SpriteRenderer _asteroids_Sprite;
    [SerializeField] private CapsuleCollider2D _asteroids_Collider;
    [SerializeField] private GameObject _asteroids;
    [SerializeField] private GameObject _asteroids_Halo;
    [SerializeField] private GameObject _asteroids_Trajectory;
    [SerializeField] private GameObject _blast;
    [SerializeField] private GameObject _smoke;


   

    void Update()
    {
        //隕石のスプライトが非表示になると実行
        if (_asteroids_Sprite.enabled == false)
        {
            //隕石のコライダーを非表示化
            _asteroids_Collider.enabled = false;
            //3秒後に"Disable_Object"を実行
            Invoke(nameof(Disable_Object), 3.0f);
        }
    }


    /*  OnCollisionEnter,物体同士が衝突した瞬間の一度きりのみ呼ばれる。
 *「BoxCollider」の「Is Trigger」のチェックを外す。
 */
    private void OnCollisionEnter2D(Collision2D other)
    {
        //地面に衝突したときに実行
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Player"))
        {
            //隕石のスプライト、軌跡、後光の非表示化
            _asteroids_Sprite.enabled = false;
            _asteroids_Halo.SetActive(false);
            _asteroids_Trajectory.SetActive(false);

            //パーティクルの表示
            _blast.SetActive(true);
            _smoke.SetActive(true);
        }
    }

    //隕石のゲームオブジェクトを非表示化させるメソッド
    private void Disable_Object()
    {
        //隕石の非表示化
        _asteroids.SetActive(false);
    }
}
