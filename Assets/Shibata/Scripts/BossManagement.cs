using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManagement : MonoBehaviour
{
    /// <summary>
    /// Component
    /// </summary>
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCol;
    private SpriteRenderer _sp;


    /// <summary>
    /// bool
    /// </summary>
    private bool _isJump = false; // falseの場合左向き　trueの場合右向き
    private bool _isMove = true; //行動しているかどうか
    private bool _isGround = true; //着地しているかどうか
    private bool _isDamage; //ダメージを受けたかどうか
    private bool _isRay = false;
    private bool _isReset = false;
    private bool _isRandom = false;
    private bool _isClear = false;

    /// <summary>
    /// int
    /// </summary>
    private int _rnd;
    private int _deadCount = 1;

    /// <summary>
    /// スクリプト取得
    /// </summary>
    private BossEnemy _bossEnemy; // スクリプト取得用の変数作成
    private PlayerMove _playerMove;
    private Bash _bash;
    private BossMoving _bossMoving;


    /// <summary>
    /// GameObject
    /// </summary>
    private GameObject[] _enemys;
    private GameObject playerobj;
    private GameObject _destroyObj = default;
    private GameObject _boss = default;

    public bool IsClear { get => _isClear; set => _isClear = value; }

    void Start()
    {
        _bossMoving = GetComponent<BossMoving>();
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        playerobj = GameObject.FindGameObjectWithTag("Player");
        _bash = playerobj.GetComponent<Bash>();
        _destroyObj = GameObject.FindGameObjectWithTag("Destroy");
        _boss = GameObject.FindGameObjectWithTag("Boss");
        _rb = _boss.GetComponent<Rigidbody2D>();
    }

    public void Beginning()
    {
        _isReset = true;
        _destroyObj.SetActive(false);
        _bossMoving.Reseting();
        Invoke("StartMove", 3);

    }

    public void StartMove()
    {
        _bossMoving.MoveStart();
        _isMove = false;
        _isDamage = false;

    }

    void Update()
    {
        switch (_bossMoving.Rnd)
        {
            case 1:
                if (_isMove == false && _isDamage == false)// && _isReset == true
                {
                    _isMove = true;
                    _bossMoving.Increase();
                    Invoke("StartMove", 5);
                }
                break;
            case 2:
                if (_isMove == false && _isDamage == false)// && _isReset == true
                {
                    _isMove = true;
                    _bossMoving.Dash();
                    Invoke("StartMove", 5);
                }
                break;
            case 3:
                if (_isMove == false && _isDamage == false)// && _isReset == true
                {
                    _isMove = true;
                    _bossMoving.Increase();
                    Invoke("StartMove", 5);
                }
                break;
            case 4:
                if (_isMove == false && _isDamage == false)// && _isReset == true
                {
                    _isMove = true;
                    _bossMoving.Dash();
                    Invoke("StartMove", 5);
                }
                break;
        }

        if (playerobj.transform.position.x < this.transform.position.x)
        {
            _bossMoving.Right();
        }
        if (playerobj.transform.position.x > this.transform.position.x)
        {
            _bossMoving.Left();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Player" && _bash.IsBash == true)//_Bash._isBash == true
        {
            _isDamage = true;
            _isReset = false;
            _deadCount = _deadCount - 1;
            CancelInvoke("StartMove");
            _bossMoving.EnemyDead();
            _bossMoving.Jump();
        }

        if (_deadCount == 0)
        {
            //_bossMoving.Dead();
            _rb.bodyType = RigidbodyType2D.Static;
            IsClear = true;
            _destroyObj.transform.position = new Vector2(_boss.transform.position.x, _boss.transform.position.y + 2);
            _destroyObj.SetActive(true);
        }

    }

}
