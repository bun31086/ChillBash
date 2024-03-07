using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysMove : MonoBehaviour
{
    private bool _isDamage = false;

    /// <summary>
    /// Enemy�S�̂̕ϐ�
    /// </summary>
    private float _enemySpeed = 2f; // Enemy�̃X�s�[�h�ݒ�
    private SpriteRenderer _sp;
    private GameObject playerobj;
    private Vector2 _player;

    /// <summary>
    /// �ǔ��̓G�p
    /// </summary>
    private Vector2 _enemyReset;


    /// <summary>
    /// VerticalEnemy�p
    /// </summary>
    [SerializeField] float _yLimit;
    [SerializeField] float _VerticalLimit;
    private int _reversal = -1;


    /// <summary>
    /// ParallelEnemy�p
    /// </summary>
    [SerializeField] float _xLimit;
    [SerializeField] float _parallelLimit;

    public bool IsDamage { get => _isDamage; set => _isDamage = value; }


    /// <summary>
    /// Enemy�S�̂̏����ϐ�
    /// </summary>
    void Start()
    {
        //Enemy�S��
        _sp = GetComponent<SpriteRenderer>();
        playerobj = GameObject.FindGameObjectWithTag("Player");

        //�ǔ��̓G�p
        _enemyReset = this.transform.position;

        //VerticalEnemy�p
        _yLimit = transform.position.y + 4;
        _VerticalLimit = transform.position.y - 4;


        //ParallelEnemy�p
        _xLimit = transform.position.x + 4;
        _parallelLimit = transform.position.x - 4;


    }

    /// <summary>
    /// Enemy�S�̂̓���
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
    /// VerticalEnemy�p
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
    /// ParallelEnemy�p
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
    /// �ǔ��̓G�p
    /// </summary>
    public void Controller()
    {
        // Enemy��Player��ǂ�����
        this.transform.position = Vector2.MoveTowards(transform.position, _player, _enemySpeed * Time.deltaTime);
        print("Control");
    }

    public void GoBack()
    {
        // Enemy�������̈ʒu��
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
