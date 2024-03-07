using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの横移動＆ジャンプ
/// </summary>
public class PlayerMove : MonoBehaviour
{
    #region 変数

    /// <summary>
    /// Float一覧
    /// </summary>
    private float _horizontal = default;               //x入力
    [SerializeField] private float _velocityMax = default;//最高移動速度
    [SerializeField] private float _moveSpeed = default;　//player移動速度
    private const float CONST_FRICTION = 2; //プレイヤーの摩擦用定数(マジックナンバー)
    private const float CONST_BOXCAST_Y_POSITION = 1.17f; //BoxCastのy座標ずらし用(マジックナンバー)
    private const float CONST_BOXCAST_X_SCALE = 2.857f;//BoxCastのScale変更用(マジックナンバー)
    private const float CONST_BOXCAST_Y_SCALE = 30;//BoxCastのScale変更用(マジックナンバー)

    /// <summary>
    /// GameObject一覧
    /// </summary>
    private GameObject _player = default;//playerObj
    [SerializeField]private GameObject _leftErea = default;
    [SerializeField]private GameObject _rightErea = default;
    [SerializeField] private GameObject _bashErea = default;
    [SerializeField] private GameObject _bashUnder = default;
    [SerializeField] private GameObject _pauseObj = default;

    [SerializeField] private GameObject _leftFoot = default;
    [SerializeField] private GameObject _rightFoot = default;

    /// <summary>
    /// Component一覧
    /// </summary>
    private Rigidbody2D _playerRigid = default;//playerのRigidBody
    private SpriteRenderer _playerSprite = default;//playerのRenderer
    private Attack _attackScript = default;
    private Animator _animator = default;                                     //Animatorを取得
    private KnockBack _knockBack = default;
    private Bash _bash = default;
    private BoxCollider2D _playerBox = default;
    private PlayerLife _playerLife = default;
    private EffectScript _effectScript = default;
    private PauseScript _pauseScript = default;

    private BoxCollider2D _boxCollider = default;

    /// <summary>
    /// Bool一覧
    /// </summary>
    [SerializeField] private bool _isController = false;//コントローラーが接続されている = true
    [SerializeField] private bool _isGround = false; //地面についているか
    private bool _isBashEffect = default;//バッシュエフェクトが出ているか

    /// <summary>
    /// Ray一覧
    /// </summary>
    private RaycastHit2D _boxDownRay = default;

    /// <summary>
    /// Vecter一覧
    /// </summary>
    private Vector2 _jumpVecter = new Vector2(0, 2300);

    /// <summary>
    /// PhysicsMaterial一覧
    /// </summary>
    [SerializeField] private PhysicsMaterial2D _physicsMaterialZero = default;

    /// <summary>
    /// Property一覧
    /// </summary>
    public bool IsGround { get => _isGround; set => _isGround = value; }


    #endregion
    
    
    #region 本編

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRigid = _player.GetComponent<Rigidbody2D>();
        _playerSprite = _player.GetComponent<SpriteRenderer>();
        _attackScript = _player.GetComponent<Attack>();
        _animator = _player.GetComponent<Animator>();                                            //Animatorを取得
        _knockBack = _player.GetComponent<KnockBack>();
        _bash = _player.GetComponent<Bash>();
        _playerBox = _player.GetComponent<BoxCollider2D>();
        _playerLife = _player.GetComponent<PlayerLife>();
        _effectScript = _player.GetComponent<EffectScript>();
        _pauseScript = _pauseObj.GetComponent<PauseScript>();

        _leftFoot.SetActive(false);
        _rightFoot.SetActive(false);

        //_boxCollider = _player.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        _boxDownRay = Physics2D.BoxCast(_player.transform.position, new Vector2(5 * 0.35f, 0.1f), _player.transform.rotation.z, -transform.up, 2.2f);
        if (_boxDownRay.collider != null)
        {
            if (_boxDownRay.collider.tag == "Floor" && _bash.IsBash)
            {
                _bashUnder.SetActive(false);
                if (!_isBashEffect)
                {
                    _isBashEffect = true;
                    _effectScript.Bash();
                }
                _animator.SetBool("isBashGround", true);
                _animator.SetBool("isBash", false);
                _bashErea.SetActive(true);
            }
            else if (_boxDownRay.collider.tag == "Floor")
            {
                _attackScript.EnemyCombo = default;
                IsGround = true;
                _animator.SetBool("isGround", true);
                //_knockBack.IsKnockBack = false;
                _playerBox.enabled = true;
            }
            else
            {
                IsGround = false;
                _animator.SetBool("isGround", false);
            }
            if (_boxDownRay.collider != null && (_boxDownRay.collider.tag == "Enemy" || _boxDownRay.collider.tag == "BossEnemy" || _boxDownRay.collider.tag == "Stone" || _boxDownRay.collider.tag == "Boss") && !_knockBack.IsKnockBack && !_bash.IsBash)
            {
                _playerLife.LiftManager();
                _knockBack.IsKnockBack = true;
                _playerRigid.velocity = Vector2.zero;
                print("DOWN");
                _playerBox.enabled = false;
                _playerRigid.gravityScale = 3;
                _playerRigid.AddForce(new Vector2(0, 100));
                _player.transform.rotation = default;
                _animator.SetBool("isChase", false);
                _animator.SetBool("isKnockBack", true);
                _attackScript.IsChasing = false;

                //次に近い敵を探す
                if (_playerSprite.flipX)
                {
                    _rightErea.transform.localPosition = new Vector2(_rightErea.transform.localPosition.x, 1);
                }
                else
                {
                    _leftErea.transform.localPosition = new Vector2(_leftErea.transform.localPosition.x, 1);
                }
            }
        }
        else
        {
            IsGround = false;
            _animator.SetBool("isGround", false);
        }

        //着地中のとき
        if (IsGround)
        {
            print("A");
            //アニメーション
            _animator.SetBool("isFall", false);
        }
        //落下中のとき
        if (_playerRigid.velocity.y < 0)
        {
            print("B");
            //下への重力を強める
            //_playerRigid.AddForce(new Vector2(0, -400 * Time.deltaTime));
            //アニメーション
            _animator.SetBool("isJump", false);
            _animator.SetBool("isFall", true);

            _leftFoot.SetActive(true);
            _rightFoot.SetActive(true);
        }
        //敵を追跡していないときかつノックバック状態じゃないときかつバッシュしていないとき
        if (!_attackScript.IsChasing && !_knockBack.IsKnockBack && !_bash.IsBash && !_pauseScript.IsPause)
        {
            //横移動入力を受け付ける
            Walk();
        }
        //キルも追跡も不可能な場合
        if (/*!_attackScript.IsKill && !_attackScript.IsChase &&*/ !_knockBack.IsKnockBack && !_pauseScript.IsPause)
        {
            //ジャンプ可能にする
            Jump();
        }
    }

    /// <summary>
    /// コントローラー操作、キーボード操作の切り替え（未実装）
    /// </summary>
    private void ControllerChange()
    {
        string[] controllerNames = Input.GetJoystickNames();
        if (controllerNames[default] == "")
        {
            _isController = false;
        }
        else
        {
            _isController = true;
        }
    }

    /// <summary>
    /// 横移動(歩き)入力
    /// </summary>
    private void Walk()
    {
        //x入力受け取り
        _horizontal = Input.GetAxisRaw("Horizontal");
                               
        //プレイヤーの向き変更
        if(_horizontal > 0)
        {
            _playerSprite.flipX = true;
            _leftErea.SetActive(false);
            _rightErea.SetActive(true);
        }
        else if(_horizontal < 0)
        {
            _playerSprite.flipX = false;
            _leftErea.SetActive(true);
            _rightErea.SetActive(false);
        }

        //一定以上のスピードが出ると力を加わらなくする
        if (_playerRigid.velocity.x <= _velocityMax && _horizontal == 1)
        {
            _playerRigid.AddForce(new Vector2(_moveSpeed * Time.deltaTime, default));
        }
        else if (_playerRigid.velocity.x > -_velocityMax && _horizontal == -1)
        {
            _playerRigid.AddForce(new Vector2(-_moveSpeed * Time.deltaTime, default));
        }

        //横移動を離すと止まりやすくする
        if (_horizontal == default && _boxDownRay.collider != null)
        {
            _playerRigid.velocity = new Vector2(_playerRigid.velocity.x - _playerRigid.velocity.x * CONST_FRICTION * Time.deltaTime, _playerRigid.velocity.y);
        }

        //アニメーション
        if (_playerRigid.velocity.x != 0 && _isGround)
        {
            print("C");
            _animator.SetBool("isWalk", true);
        }
        else
        {
            _animator.SetBool("isWalk", false);
        }
    }

    /// <summary>
    /// ジャンプ入力
    /// </summary>
    private void Jump()
    {
        if(_boxDownRay.collider != null)
        {
            if (_boxDownRay.collider.tag == "Floor")
            {
                IsGround = true;

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    _playerRigid.AddForce(_jumpVecter);

                    _leftFoot.SetActive(true);
                    _rightFoot.SetActive(true);

                    //アニメーション
                    _animator.SetBool("isJump", true);
                }
            }
            else
            {
                IsGround = false;
            }
        }
        else
        {
            IsGround = false;
        }
    }

    private void BashEnd()
    {
        _attackScript.EnemyCombo = default;
        _bash.IsBash = false;
        _animator.SetBool("isBashGround", false);
        _playerRigid.gravityScale = 3;
        _bashErea.SetActive(false);
        _isBashEffect = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(transform.position.x,transform.position.y - CONST_BOXCAST_Y_POSITION),new Vector2(transform.localScale.x / CONST_BOXCAST_X_SCALE , transform.localScale.y / CONST_BOXCAST_Y_SCALE));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            _leftFoot.SetActive(false);
            _rightFoot.SetActive(false);
            Invoke("MasatuChange", 0.5f);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _playerRigid.sharedMaterial = _physicsMaterialZero;
        }
    }

    private void MasatuChange()
    {
        _playerRigid.sharedMaterial = default;
    }

    #endregion
}
