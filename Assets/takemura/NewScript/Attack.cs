using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの攻撃
/// </summary>
public class Attack : MonoBehaviour
{
    #region 変数

    /// <summary>
    /// Float,int一覧
    /// </summary>
    private const float _chasePower = 1500;
    [SerializeField] public static int _enemyKillCount = default;
    [SerializeField] private int _enemyCombo = default;


    /// <summary>
    /// GameObject一覧
    /// </summary>
    private GameObject _player = default;
    private GameObject _enemy = default;
    [SerializeField] private GameObject _leftErea = default;
    [SerializeField] private GameObject _rightErea = default;
    [SerializeField] private GameObject _enemyExplosionEffect = default;

    [SerializeField] private GameObject _leftFoot = default;
    [SerializeField] private GameObject _rightFoot = default;


    /// <summary>
    /// Component一覧
    /// </summary>
    private Rigidbody2D _playerRigid = default;
    private SpriteRenderer _playerSprite = default;
    private Transform _transform = default;
    private BoxCollider2D _playerBox = default;
    private GetTargetRay _getTargetRayLeft = default;
    private GetTargetRay _getTargetRayRight = default;
    private Animator _animator;                                     //Animatorを取得
    private KnockBack _knockBack = default;

    /// <summary>
    /// Bool一覧
    /// </summary>
    [SerializeField] private bool _isChase = false;
    [SerializeField] private bool _isKill = false;
    [SerializeField] private bool _isTrigger = false;
    [SerializeField] private bool _isChasing = false;

    /// <summary>
    /// Property一覧
    /// </summary>
    public bool IsKill { get => _isKill; set => _isKill = value; }
    public bool IsChase { get => _isChase; set => _isChase = value; }
    public bool IsTrigger { get => _isTrigger; set => _isTrigger = value; }
    public bool IsChasing { get => _isChasing; set => _isChasing = value; }
    public GameObject Enemy { get => _enemy; set => _enemy = value; }
    //public int EnemyKillCount { get => _enemyKillCount; set => _enemyKillCount = value; }
    public int EnemyCombo { get => _enemyCombo; set => _enemyCombo = value; }

    /// <summary>
    /// Ray一覧
    /// </summary>


    /// <summary>
    /// Vecter一覧
    /// </summary>
    private Vector3 _playerDir = default;


    #endregion
    #region 本編


    void Start()
    {
        // transformに毎回アクセスすると重いので、キャッシュしておく
        _transform = transform;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRigid = _player.GetComponent<Rigidbody2D>();
        _playerSprite = _player.GetComponent<SpriteRenderer>();
        _playerBox = _player.GetComponent<BoxCollider2D>();
        _getTargetRayRight = _rightErea.GetComponent<GetTargetRay>();
        _getTargetRayLeft = _leftErea.GetComponent<GetTargetRay>();
        _animator = GetComponent<Animator>();                                            //Animatorを取得
        _knockBack = _player.GetComponent<KnockBack>();
    }


    void Update()
    {
        if (!_isChasing)
        {
            _playerRigid.AddForce(new Vector2(0, -200 * Time.deltaTime));
        }
        //_isChaseがtrueの場合
        if (_isChase && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            //敵を追いかける
            EnemyChase();

            //追跡中フラグをオンにする
            IsChasing = true;
        }
        //キルが可能状態のとき
        if (_isKill && !_knockBack.IsKnockBack)
        {
            //プレイヤーの当たり判定を戻す
            _playerBox.enabled = true;

            //エンターキーが押されたら
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                //追跡中フラグをオフにする
                IsChasing = false;

                //一番近い敵をキルする
                EnemyKill();
            }
        }
    }

    /// <summary>
    /// 敵に追従する処理
    /// </summary>
    private void EnemyChase()
    {
        _leftFoot.SetActive(true);
        _rightFoot.SetActive(true);


        _playerRigid.velocity = Vector2.zero;

        // 向きたい方向を計算
        _playerDir = (_enemy.transform.position - _player.transform.position);

        //プレイヤーの向きによって敵に向く方向を変える
        if (_playerSprite.flipX)
        {
            //エリアの位置を調整
            _rightErea.transform.localPosition = new Vector2(_rightErea.transform.localPosition.x, 0);

            //プレイヤーの向きを変更
            _player.transform.rotation = Quaternion.FromToRotation(Vector3.right, _playerDir);

            //アニメーションを変更
            _animator.SetBool("isChase", true);

            //プレイヤーの当たり判定を一時的に消す
            //_playerBox.enabled = false;

            //プレイヤーの向いている方向に飛ばす
            _playerRigid.velocity = Vector2.zero;
            _playerRigid.gravityScale = default;
            _playerRigid.AddForce(transform.right * _chasePower);
        }
        else
        {
            //エリアの位置を調整
            _leftErea.transform.localPosition = new Vector2(_leftErea.transform.localPosition.x, 0);

            //プレイヤーの向きを変更
            _player.transform.rotation = Quaternion.FromToRotation(Vector3.left, _playerDir);

            //アニメーションを変更
            _animator.SetBool("isChase", true);

            //プレイヤーの当たり判定を一時的に消す
            _playerBox.enabled = false;

            //プレイヤーの向いている方向に飛ばす
            _playerRigid.velocity = Vector2.zero;
            _playerRigid.gravityScale = default;
            _playerRigid.AddForce(-transform.right * _chasePower); 
        }
    }

    /// <summary>
    /// 敵を倒す処理
    /// </summary>
    private void EnemyKill()
    {
        //エフェクトの位置を倒す敵の位置に変更
        _enemyExplosionEffect.transform.position = _enemy.transform.position;

        //エフェクトを生成
        Instantiate(_enemyExplosionEffect);


        //キル数を増やす
        EnemyCombo++;
        if (_enemy.tag == "Enemy" || _enemy.tag == "Stone")
        {
            _enemyKillCount++;
        }

        //_enemyに格納されている敵を消す
        _enemy.SetActive(false);

        //次に近い敵を探す
        if (_playerSprite.flipX)
        {
            _rightErea.transform.localPosition = new Vector2(_rightErea.transform.localPosition.x, 1f);
            _getTargetRayRight.EnemySearch();
        }
        else
        {
            _leftErea.transform.localPosition = new Vector2(_leftErea.transform.localPosition.x, 1f);
            _getTargetRayLeft.EnemySearch();
        }

        //近い敵がいた場合
        if (_enemy != null)
        {
            //その敵を追跡する
            EnemyChase();

            //追跡中フラグをオンにする
            IsChasing = true;
        }
        //近い敵がいない場合
        else
        {
            //アニメーションを変更
            _animator.SetBool("isChase", false);
            _animator.SetBool("isFall", true);

            //重力を戻す
            _playerRigid.gravityScale = 3;

            //プレイヤーの向きを直す
            _player.transform.rotation = default;

            //プレイヤーの当たり判定を戻す
            _playerBox.enabled = true;
        }
    }

    /// <summary>
    /// 追跡中に壁に当たると止まる処理
    /// </summary>

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && IsChasing)
        {
            IsChasing = false;

            //プレイヤーの当たり判定を戻す
            _playerBox.enabled = true;

            //重力をもどす
            _playerRigid.gravityScale = 3;

            //アニメーション
            _animator.SetBool("isChase",false);

            //角度を戻す
            _player.transform.rotation = default;
        }
    }

    #endregion
}
