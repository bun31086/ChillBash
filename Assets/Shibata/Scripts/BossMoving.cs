using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMoving : MonoBehaviour
{
    /// <summary>
    /// Bossの動きの制御
    /// </summary>
    /// 
    [SerializeField] private float _jumpPower = 500f;
    [SerializeField] private float _moveSpeed = 300f;
    [SerializeField] private float _dashSpeed = default;
    [SerializeField] private float _dashJumpSpeed = default;


    /// <summary>
    /// Component
    /// </summary>
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCol;
    private SpriteRenderer _sp;

    /// <summary>
    /// GameObject
    /// </summary>
    private GameObject[] _enemys;
    private GameObject playerobj;

    /// <summary>
    /// スクリプト取得
    /// </summary>
    private BossEnemy _bossEnemy; // スクリプト取得用の変数作成
    private PlayerMove _playerMove;
    private BossManagement _bossManagement;

    /// <summary>
    /// Vector
    /// </summary>
    private Vector2 _force;
    private Vector2 _reForce;
    private Vector2 _dashForce;


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

    /// <summary>
    /// int
    /// </summary>
    private int _rnd;

    public int Rnd { get => _rnd; set => _rnd = value; }

    private int _deadCount = 3;


    /// <summary>
    /// Rayの制御
    /// </summary>
    [SerializeField]
    private float _startRay = 1.16f;
    [SerializeField]
    private float _rayLength = 0.3f;
    private RaycastHit2D _hit;
    [SerializeField]
    private float _startRayRight = 6f;
    [SerializeField]
    private float _startRayLeft = 1.16f;
    private RaycastHit2D _hitRight;
    private RaycastHit2D _hitLeft;

    void Start()
    {
        //Debug.Log("Start");
        _rb = this.GetComponent<Rigidbody2D>();
        _boxCol = this.gameObject.GetComponent<BoxCollider2D>();
        _force = new Vector2(_moveSpeed * Time.deltaTime, _jumpPower * Time.deltaTime);
        _reForce = new Vector2(-_moveSpeed * Time.deltaTime, _jumpPower * Time.deltaTime);
        _dashForce = new Vector2(_dashSpeed * Time.deltaTime, _dashJumpSpeed * Time.deltaTime);
        _enemys = GameObject.FindGameObjectsWithTag("BossEnemy");
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        _bossManagement = GetComponent<BossManagement>();
        playerobj = GameObject.FindGameObjectWithTag("Player");
        _sp = GetComponent<SpriteRenderer>();
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

    }

    public void MoveStart()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        Rnd = Random.Range(1, 5); // ※ 1～2の範囲でランダムな整数値が返る
        Debug.Log(Rnd + "Rnd");
        _isRandom = true;
    }

    void Update()
    {
        if(_isRay == true)
        {
            print("Raycast");
            _hit = Physics2D.Raycast
            (this.transform.position + Vector3.down * _startRay, Vector2.down, _rayLength);
            Debug.DrawRay(this.transform.position + Vector3.down * _startRay, Vector2.down * _rayLength, Color.red);
            _hitRight = Physics2D.Raycast
                (this.transform.position + Vector3.right * _startRayRight, Vector2.right, _rayLength);
            Debug.DrawRay(this.transform.position + Vector3.right * _startRayRight, Vector2.right * _rayLength, Color.red);
            _hitLeft = Physics2D.Raycast
                (this.transform.position + Vector3.left * _startRayLeft, Vector2.left, _rayLength);
            Debug.DrawRay(this.transform.position + Vector3.left * _startRayLeft, Vector2.left * _rayLength, Color.red);

            if (_hit.collider != null)
            {
                if (_hit.collider.CompareTag("Floor") && _isGround == false)
                {
                    print("Hit");
                    _boxCol.enabled = true;
                    _isGround = true;
                    _isDamage = false;
                    _isRay = false;
                    _isReset = true;
                    Invoke("ReMove", 3);
                }
            }
            if (_hitRight.collider != null)
            {
                if (_hitRight.collider.CompareTag("Floor") && _isGround == false)
                {
                    print("Hit");
                    _boxCol.enabled = true;
                    _isGround = true;
                    _isDamage = false;
                    _isRay = false;
                    _isReset = true;
                    Invoke("ReMove", 3);
                }
            }
            if (_hitLeft.collider != null)
            {
                if (_hitLeft.collider.CompareTag("Floor") && _isGround == false)
                {
                    print("Hit");
                    _boxCol.enabled = true;
                    _isGround = true;
                    _isDamage = false;
                    _isRay = false;
                    _isReset = true;
                    Invoke("ReMove", 3);
                }
            }
        }
    }

    private void ReMove()
    {
        _bossManagement.StartMove();
    }



    public void Increase()
    {
        foreach (GameObject enemy in _enemys)
        {
            enemy.SetActive(true);
        }
    }

    public void Reseting()
    {
        print("Reseting");
        foreach (GameObject enemy in _enemys)
        {
            enemy.SetActive(false);
        }
    }
    public void Jump()
    {
        print("Jump");
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _isGround = false;
        _boxCol.enabled = false;
        if (_sp.flipX == false)// && _isDamage == true
        {
            _rb.AddForce(_reForce);
            print(_reForce);
            print("reForce");
            Invoke("Raycast", 0.3f);
        }
        else if (_sp.flipX == true)// && _isDamage == true
        {
            _rb.AddForce(_force);
            print("force");
            Invoke("Raycast", 0.3f);
        }

    }

    public void Dash()
    {
        print("Dash");
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (playerobj.transform.position.x < this.transform.position.x)
        {
            _rb.AddForce(-_dashForce);
        }
        if (playerobj.transform.position.x > this.transform.position.x)
        {
            _rb.AddForce(_dashForce);
        }
    }


    private void Raycast()
    {
        _isRay = true;
    }

    public void Dead()
    {
        print("Dead");
        Destroy(this.gameObject);
        SceneManager.LoadScene("ClearScene");
    }


    public void EnemyDead()
    {
        foreach (GameObject enemy in _enemys)
        {
            enemy.SetActive(false);
        }
    }

    public void Right()
    {
        _sp.flipX = false;
    }

    public void Left()
    {
        _sp.flipX = true;
    }


}
