using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    #region 変数


    /// <summary>
    /// Float一覧
    /// </summary>
    [SerializeField] private float CONST_BOXCAST_X_POSITION = 0.57f; //BoxCastのy座標ずらし用(マジックナンバー)
    private const float CONST_BOXCAST_Y_POSITION = 1.17f; //BoxCastのy座標ずらし用(マジックナンバー)
    [SerializeField] private float CONST_BOXCAST_X_SCALE = 2.857f;//BoxCastのScale変更用(マジックナンバー)
    [SerializeField] private float CONST_BOXCAST_Y_SCALE = 30;//BoxCastのScale変更用(マジックナンバー)

    /// <summary>
    /// GameObject一覧
    /// </summary>
    private GameObject _player = default;
    [SerializeField] private GameObject _leftErea = default;
    [SerializeField] private GameObject _rightErea = default;

    /// <summary>
    /// Component一覧
    /// </summary>
    private Rigidbody2D _playerRigid = default;//playerのRigidBody
    private BoxCollider2D _playerBox = default;
    private Animator _animator = default;                                     //Animatorを取得
    private Attack _attackScript = default;
    private SpriteRenderer _playerSprite = default;
    private PlayerLife _playerLife = default;

    /// <summary>
    /// Ray一覧
    /// </summary>
    private RaycastHit2D _boxLeftRay = default;
    private RaycastHit2D _boxRightRay = default;
    private RaycastHit2D _boxUpRay = default;

    /// <summary>
    /// bool一覧
    /// </summary>
    [SerializeField] private bool _isKnockBack = false;

    /// <summary>
    /// Property一覧
    /// </summary>
    public bool IsKnockBack { get => _isKnockBack; set => _isKnockBack = value; }


    #endregion
    #region 本編

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRigid = _player.GetComponent<Rigidbody2D>();
        _playerBox = _player.GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();                                            //Animatorを取得
        _attackScript = _player.GetComponent<Attack>();
        _playerSprite = _player.GetComponent<SpriteRenderer>();
        _playerLife = _player.GetComponent<PlayerLife>();
    }

    // Update is called once per frame
    void Update()
    {
        //_boxUpRay = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y + CONST_BOXCAST_Y_POSITION), new Vector2(transform.localScale.x - 1.9f, transform.localScale.y / CONST_BOXCAST_Y_SCALE), default, Vector2.zero);
        _boxLeftRay = Physics2D.BoxCast(_player.transform.position, new Vector2(0.1f, 5 * 0.74f), _player.transform.rotation.z, -transform.right, 1.2f);
        _boxRightRay = Physics2D.BoxCast(_player.transform.position, new Vector2(0.1f, 5 * 0.74f), _player.transform.rotation.z, transform.right,1.2f);
        _boxUpRay = Physics2D.BoxCast(_player.transform.position, new Vector2(5 * 0.35f, 0.1f), _player.transform.rotation.z, transform.up, 2.2f);

        //左向き
        if (_boxLeftRay.collider != null && (_boxLeftRay.collider.tag == "Enemy" || _boxLeftRay.collider.tag == "Stone" || _boxLeftRay.collider.tag == "Boss" || _boxLeftRay.collider.tag == "BossEnemy") && !_isKnockBack)
        {
            if (_boxLeftRay.collider.tag == "Enemy" || _boxLeftRay.collider.tag == "BossEnemy")
            {
                _boxLeftRay.collider.GetComponent<EnemysMove>().PlayerHit();
            }
            _attackScript.EnemyCombo = default;
            _playerLife.LiftManager();
            _isKnockBack = true;
            _playerRigid.velocity = Vector2.zero;
            _playerBox.enabled = false;
            _playerRigid.gravityScale = 3;
            _playerRigid.AddForce(new Vector2(200, 0));
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
        //右向き
        if (_boxRightRay.collider != null && (_boxRightRay.collider.tag == "Enemy" || _boxRightRay.collider.tag == "Stone" || _boxRightRay.collider.tag == "Boss" || _boxRightRay.collider.tag == "BossEnemy") && !_isKnockBack)
        {
            if (_boxRightRay.collider.tag == "Enemy" || _boxRightRay.collider.tag == "BossEnemy")
            {
                _boxRightRay.collider.GetComponent<EnemysMove>().PlayerHit();
            }
            _attackScript.EnemyCombo = default;
            _playerLife.LiftManager();
            _animator.SetBool("isKnockBack", true);
            _isKnockBack = true;
            _playerRigid.velocity = Vector2.zero;
            _playerBox.enabled = false;
            _playerRigid.gravityScale = 3;
            _playerRigid.AddForce(new Vector2(-200, 0));
            _player.transform.rotation = default;
            _animator.SetBool("isChase", false);
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
        //上向き
        if (_boxUpRay.collider != null && (_boxUpRay.collider.tag == "Enemy" || _boxUpRay.collider.tag == "Stone" || _boxUpRay.collider.tag == "Boss" || _boxUpRay.collider.tag == "BossEnemy") && !_isKnockBack)
        {
            if (_boxUpRay.collider.tag == "Enemy" || _boxUpRay.collider.tag == "BossEnemy")
            {
                _boxUpRay.collider.GetComponent<EnemysMove>().PlayerHit();
            }
            _attackScript.EnemyCombo = default;
            _playerLife.LiftManager();
            _isKnockBack = true;
            _playerRigid.velocity = Vector2.zero;
            _playerBox.enabled = false;
            _playerRigid.gravityScale = 3;
            //_playerRigid.AddForce(new Vector2(0, 200));
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

    private void KnockBackOn()
    {
        _isKnockBack = false;
        _animator.SetBool("isKnockBack", false);
        _attackScript.EnemyCombo = default;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + CONST_BOXCAST_Y_POSITION), new Vector2(transform.localScale.x - 1.9f, transform.localScale.y / CONST_BOXCAST_Y_SCALE));
        //Gizmos.DrawWireCube(new Vector2(transform.position.x - CONST_BOXCAST_X_POSITION, transform.position.y), new Vector2(transform.localScale.x / CONST_BOXCAST_X_SCALE, transform.localScale.y - 0.6f));
        //Gizmos.DrawWireCube(new Vector2(transform.position.x + CONST_BOXCAST_X_POSITION, transform.position.y), new Vector2(transform.localScale.x / CONST_BOXCAST_X_SCALE, transform.localScale.y - 0.6f));


        Gizmos.DrawWireCube(_boxRightRay.point, new Vector2(0.1f, 3 * 0.74f));
        Gizmos.DrawWireCube(_boxLeftRay.point, new Vector2(0.1f, 3 * 0.74f));

    }
    #endregion
}
