using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̉��ړ����W�����v
/// </summary>
public class PlayerMove : MonoBehaviour
{
    #region �ϐ�

    /// <summary>
    /// Float�ꗗ
    /// </summary>
    private float _horizontal = default;               //x����
    [SerializeField] private float _velocityMax = default;//�ō��ړ����x
    [SerializeField] private float _moveSpeed = default;�@//player�ړ����x
    private const float CONST_FRICTION = 2; //�v���C���[�̖��C�p�萔(�}�W�b�N�i���o�[)
    private const float CONST_BOXCAST_Y_POSITION = 1.17f; //BoxCast��y���W���炵�p(�}�W�b�N�i���o�[)
    private const float CONST_BOXCAST_X_SCALE = 2.857f;//BoxCast��Scale�ύX�p(�}�W�b�N�i���o�[)
    private const float CONST_BOXCAST_Y_SCALE = 30;//BoxCast��Scale�ύX�p(�}�W�b�N�i���o�[)

    /// <summary>
    /// GameObject�ꗗ
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
    /// Component�ꗗ
    /// </summary>
    private Rigidbody2D _playerRigid = default;//player��RigidBody
    private SpriteRenderer _playerSprite = default;//player��Renderer
    private Attack _attackScript = default;
    private Animator _animator = default;                                     //Animator���擾
    private KnockBack _knockBack = default;
    private Bash _bash = default;
    private BoxCollider2D _playerBox = default;
    private PlayerLife _playerLife = default;
    private EffectScript _effectScript = default;
    private PauseScript _pauseScript = default;

    private BoxCollider2D _boxCollider = default;

    /// <summary>
    /// Bool�ꗗ
    /// </summary>
    [SerializeField] private bool _isController = false;//�R���g���[���[���ڑ�����Ă��� = true
    [SerializeField] private bool _isGround = false; //�n�ʂɂ��Ă��邩
    private bool _isBashEffect = default;//�o�b�V���G�t�F�N�g���o�Ă��邩

    /// <summary>
    /// Ray�ꗗ
    /// </summary>
    private RaycastHit2D _boxDownRay = default;

    /// <summary>
    /// Vecter�ꗗ
    /// </summary>
    private Vector2 _jumpVecter = new Vector2(0, 2300);

    /// <summary>
    /// PhysicsMaterial�ꗗ
    /// </summary>
    [SerializeField] private PhysicsMaterial2D _physicsMaterialZero = default;

    /// <summary>
    /// Property�ꗗ
    /// </summary>
    public bool IsGround { get => _isGround; set => _isGround = value; }


    #endregion
    
    
    #region �{��

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRigid = _player.GetComponent<Rigidbody2D>();
        _playerSprite = _player.GetComponent<SpriteRenderer>();
        _attackScript = _player.GetComponent<Attack>();
        _animator = _player.GetComponent<Animator>();                                            //Animator���擾
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

                //���ɋ߂��G��T��
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

        //���n���̂Ƃ�
        if (IsGround)
        {
            print("A");
            //�A�j���[�V����
            _animator.SetBool("isFall", false);
        }
        //�������̂Ƃ�
        if (_playerRigid.velocity.y < 0)
        {
            print("B");
            //���ւ̏d�͂����߂�
            //_playerRigid.AddForce(new Vector2(0, -400 * Time.deltaTime));
            //�A�j���[�V����
            _animator.SetBool("isJump", false);
            _animator.SetBool("isFall", true);

            _leftFoot.SetActive(true);
            _rightFoot.SetActive(true);
        }
        //�G��ǐՂ��Ă��Ȃ��Ƃ����m�b�N�o�b�N��Ԃ���Ȃ��Ƃ����o�b�V�����Ă��Ȃ��Ƃ�
        if (!_attackScript.IsChasing && !_knockBack.IsKnockBack && !_bash.IsBash && !_pauseScript.IsPause)
        {
            //���ړ����͂��󂯕t����
            Walk();
        }
        //�L�����ǐՂ��s�\�ȏꍇ
        if (/*!_attackScript.IsKill && !_attackScript.IsChase &&*/ !_knockBack.IsKnockBack && !_pauseScript.IsPause)
        {
            //�W�����v�\�ɂ���
            Jump();
        }
    }

    /// <summary>
    /// �R���g���[���[����A�L�[�{�[�h����̐؂�ւ��i�������j
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
    /// ���ړ�(����)����
    /// </summary>
    private void Walk()
    {
        //x���͎󂯎��
        _horizontal = Input.GetAxisRaw("Horizontal");
                               
        //�v���C���[�̌����ύX
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

        //���ȏ�̃X�s�[�h���o��Ɨ͂������Ȃ�����
        if (_playerRigid.velocity.x <= _velocityMax && _horizontal == 1)
        {
            _playerRigid.AddForce(new Vector2(_moveSpeed * Time.deltaTime, default));
        }
        else if (_playerRigid.velocity.x > -_velocityMax && _horizontal == -1)
        {
            _playerRigid.AddForce(new Vector2(-_moveSpeed * Time.deltaTime, default));
        }

        //���ړ��𗣂��Ǝ~�܂�₷������
        if (_horizontal == default && _boxDownRay.collider != null)
        {
            _playerRigid.velocity = new Vector2(_playerRigid.velocity.x - _playerRigid.velocity.x * CONST_FRICTION * Time.deltaTime, _playerRigid.velocity.y);
        }

        //�A�j���[�V����
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
    /// �W�����v����
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

                    //�A�j���[�V����
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
