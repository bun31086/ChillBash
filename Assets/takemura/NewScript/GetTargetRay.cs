using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Concurrent;

/// <summary>
/// プレイヤーとの間に障害物が無い一番近い敵を探す処理
/// </summary>
public class GetTargetRay : MonoBehaviour
{
    #region 変数
    /// <summary>
    /// Float,int一覧
    /// </summary>
    private float _rayLength = 30;                         //rayの長さ
    [SerializeField]private float _minValue = 100f;        //一番近い敵を取得する際に使う距離の初期値
    private const float CONST_KILL_EREA = 7;  //キル可能距離

    /// <summary>
    /// Ray一覧
    /// </summary>
    private int _layerMask = 1 << 6;　　　　　　　　　　　//layer6だけ反応させる
    private RaycastHit2D _censorRay = default;           //センサーとなるRayを出す

    /// <summary>
    /// Vecter一覧
    /// </summary>

    /// <summary>
    /// GameObject一覧
    /// </summary>
    private GameObject _player = default;
    [SerializeField]private GameObject _nearEnemy = default;　
    [SerializeField] private List<GameObject> _inEnemyList = new List<GameObject>(); //敵を入れる配列
    [SerializeField] private GameObject _aimObj = default;

    /// <summary>
    /// Compornent一覧
    /// </summary>
    private SpriteRenderer _spriteRenderer = default;
    private Attack _attackScript = default;
    private KnockBack _knockBack = default;

    /// <summary>
    /// Sprite一覧
    /// </summary>
    [SerializeField] private Sprite _aimRed = default;
    [SerializeField] private Sprite _aimBlue = default;

    /// <summary>
    /// bool一覧
    /// </summary>
    //private bool _iskill = false;
    private bool _isReset = false;

    #endregion
    #region 本編

    // Start is called before the first frame update
    void Start()
    {
        _player = transform.parent.gameObject;
        _spriteRenderer = _aimObj.GetComponent<SpriteRenderer>();
        _attackScript = _player.GetComponent<Attack>();
        _knockBack = _player.GetComponent<KnockBack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_knockBack.IsKnockBack)
        {
            EnemySearch();
        }
        else
        {
            //キルが不可能になる
            KillModeOFF();

            //照準をどこかへ飛ばす
            _aimObj.transform.position = new Vector2(20, 80);

        }
    }

    public void EnemySearch()
    {
        //配列に敵が入っている場合
        if (_inEnemyList.Count != 0)
        {
            //Rayと敵との距離を初期化
            _rayLength = 30f;
            _minValue = 100f;

            //フラグ初期化
            _isReset = false;

            //配列に入っている敵の数、繰り返す
            foreach (GameObject Enemy in _inEnemyList)
            {
                //Rayを出力
                _censorRay = Physics2D.Raycast(_player.transform.position, Enemy.transform.position - _player.transform.position, _rayLength + 0.2f, _layerMask);
                //Debug.DrawRay(_player.transform.position, (Enemy.transform.position - _player.transform.position) * (_rayLength + 0.2f));

                //RayにEnemyが当たっていて、_minValueよりも距離が近い場合
                if (_censorRay.collider != null)
                {
                    if (_minValue > Vector2.Distance(Enemy.transform.position, _player.transform.position) && (_censorRay.collider.tag == "Enemy" || _censorRay.collider.tag == "BossEnemy" || _censorRay.collider.tag == "Stone"))
                    {
                        //foreachの中で一回でも敵を取得したとき
                        _isReset = true;

                        //その敵を_nearEnemyに格納し、距離を更新
                        _nearEnemy = Enemy;
                        _minValue = Vector2.Distance(Enemy.transform.position, _player.transform.position);

                        //距離がKILL_EREAより近い場合
                        if (_minValue < CONST_KILL_EREA)
                        {
                            //キルが可能になる
                            KillModeON();
                        }
                        //距離がKILL_EREAより遠い場合
                        else
                        {
                            //キルが不可能になる
                            KillModeOFF();
                        }
                    }
                    //FloorタグのObjectに当たり、まだ一回も敵を取得していない場合
                    else if (_censorRay.collider.tag == "Floor" && !_isReset)
                    {
                        //_nearEnemyを空にする
                        _nearEnemy = null;
                    }
                }
            }
            //一番近い敵がいる場合
            if (_nearEnemy != null)
            {
                //照準を一番近い敵に合わせる
                _aimObj.transform.position = _nearEnemy.transform.position;

                //キル可能状態のとき
                if (_attackScript.IsKill)
                {
                    //Chaseが不可能になる
                    _attackScript.IsChase = false;

                    //プレイヤーのトリガーをオフにする
                    _attackScript.IsTrigger = false;
                }
                //キル可能状態では無いとき
                else
                {
                    //Chaseが可能になる
                    _attackScript.IsChase = true;
                }
            }
            //一番近い敵がいない場合
            else
            {
                //Chaseが不可能になる
                _attackScript.IsChase = false;

                //照準をどこかへ飛ばす
                _aimObj.transform.position = new Vector2(20, 20);
            }
        }
        //配列に敵が入っていない場合
        else
        {
            //キルが不可能になる
            KillModeOFF();

            //_nearEnemyの中身を空にする
            _nearEnemy = null;

            //照準をどこかへ飛ばす
            _aimObj.transform.position = new Vector2(-200, 50);

            //Chaseが不可能になる
            _attackScript.IsChase = false;
        }
        _attackScript.Enemy = _nearEnemy;
    }

    /// <summary>
    /// キルが可能なとき
    /// </summary>
    private void KillModeON()
    {
        //Chaseが不可能になる
        _attackScript.IsChase = false;
        
        //攻撃可能フラグON
        _attackScript.IsKill = true;

        //照準を赤に変更
        _spriteRenderer.sprite = _aimRed;
    }

    /// <summary>
    /// キルが不可能なとき
    /// </summary>
    private void KillModeOFF()
    {
        //攻撃可能フラグOFF
        _attackScript.IsKill = false;

        //照準を青に変更
        _spriteRenderer.sprite = _aimBlue;
    }

    /// <summary>
    /// トリガーに入った敵を配列に入れる処理
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //トリガーに入ったオブジェクトのタグがEnemyだった場合
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy" || collision.gameObject.tag == "Stone")
        {
            //配列のそのオブジェクトを追加
            _inEnemyList.Add(collision.gameObject);
        }
    }

    /// <summary>
    /// トリガーから抜けた敵を配列から抜く処理
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        //トリガーから抜けたオブジェクトのタグがAirEnemyだった場合
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy" || collision.gameObject.tag == "Stone")
        {
            //配列の中にあるオブジェクトと照らし合わせる処理
            foreach (GameObject search in _inEnemyList)
            {
                //トリガーから抜けたオブジェクトが配列にある場合
                if(search == collision.gameObject)
                {
                    //配列からそのオブジェクトを削除
                    _inEnemyList.Remove(search);
                    return;
                }
            }
        }
    }
    #endregion
}
