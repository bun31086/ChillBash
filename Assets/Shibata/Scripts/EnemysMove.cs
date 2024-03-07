using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysMove : MonoBehaviour
{
    private bool _isDamage = false;

    /// <summary>
    /// Enemy全体の変数
    /// </summary>
    private float _enemySpeed = 2f; // Enemyのスピード設定
    private SpriteRenderer _sp;
    private GameObject playerobj;
    private Vector2 _player;

    /// <summary>
    /// 追尾の敵用
    /// </summary>
    private Vector2 _enemyReset;


    /// <summary>
    /// VerticalEnemy用
    /// </summary>
    [SerializeField] float _yLimit;
    [SerializeField] float _VerticalLimit;
    private int _reversal = -1;


    /// <summary>
    /// ParallelEnemy用
    /// </summary>
    [SerializeField] float _xLimit;
    [SerializeField] float _parallelLimit;

    public bool IsDamage { get => _isDamage; set => _isDamage = value; }


    /// <summary>
    /// Enemy全体の初期変数
    /// </summary>
    void Start()
    {
        //Enemy全体
        _sp = GetComponent<SpriteRenderer>();
        playerobj = GameObject.FindGameObjectWithTag("Player");

        //追尾の敵用
        _enemyReset = this.transform.position;

        //VerticalEnemy用
        _yLimit = transform.position.y + 4;
        _VerticalLimit = transform.position.y - 4;


        //ParallelEnemy用
        _xLimit = transform.position.x + 4;
        _parallelLimit = transform.position.x - 4;


    }

    /// <summary>
    /// Enemy全体の動き
    /// </summary>
    private void Update()
    {
        _player = playerobj.transform.position;
    }
    public void Right()
    {
        _sp.flipX = true;
    }

    public void Left()
    {
        _sp.flipX = false;
    }


    /// <summary>
    /// VerticalEnemy用
    /// </summary>
    public void VerticalEnemyMove()
    {
        float y = 1f;
        transform.Translate(new Vector3(0, y * _enemySpeed * Time.deltaTime));

        Vector3 currentPos = transform.position;

        currentPos.y = Mathf.Clamp(currentPos.y, _VerticalLimit, _yLimit);

        transform.position = currentPos;
        if (currentPos.y == _yLimit)
        {
            _enemySpeed *= _reversal;
        }

        if (currentPos.y == _VerticalLimit)
        {
            _enemySpeed *= _reversal;
        }
    }


    /// <summary>
    /// ParallelEnemy用
    /// </summary>
    public void HorizontalEnemyMove()
    {
        float x = 1f;
        transform.Translate(new Vector3(x * _enemySpeed * Time.deltaTime, 0));

        Vector3 currentPos = transform.position;

        currentPos.x = Mathf.Clamp(currentPos.x, _parallelLimit, _xLimit);

        transform.position = currentPos;
        if (currentPos.x == _xLimit)
        {
            _sp.flipX = true;
            _enemySpeed *= -1;
        }

        if (currentPos.x == _parallelLimit)
        {
            _sp.flipX = false;
            _enemySpeed *= -1;
        }
    }


    /// <summary>
    /// 追尾の敵用
    /// </summary>
    public void Controller()
    {
        // EnemyがPlayerを追う処理
        this.transform.position = Vector2.MoveTowards(transform.position, _player, _enemySpeed * Time.deltaTime);
        print("Control");
    }

    public void GoBack()
    {
        // Enemyを初期の位置へ
        this.transform.position = Vector2.MoveTowards(transform.position, _enemyReset, _enemySpeed * Time.deltaTime);
    }
    public void PlayerHit()
    {
            IsDamage = true;
            EnemyStop();
    }


    public void EnemyStop()
    {
        Invoke("ResetEnemy", 4);
    }

    private void ResetEnemy()
    {
        IsDamage = false;
    }
}
