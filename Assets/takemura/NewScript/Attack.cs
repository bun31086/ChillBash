using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̍U��
/// </summary>
public class Attack : MonoBehaviour
{
    #region �ϐ�

    /// <summary>
    /// Float,int�ꗗ
    /// </summary>
    private const float _chasePower = 1500;
    [SerializeField] public static int _enemyKillCount = default;
    [SerializeField] private int _enemyCombo = default;


    /// <summary>
    /// GameObject�ꗗ
    /// </summary>
    private GameObject _player = default;
    private GameObject _enemy = default;
    [SerializeField] private GameObject _leftErea = default;
    [SerializeField] private GameObject _rightErea = default;
    [SerializeField] private GameObject _enemyExplosionEffect = default;

    [SerializeField] private GameObject _leftFoot = default;
    [SerializeField] private GameObject _rightFoot = default;


    /// <summary>
    /// Component�ꗗ
    /// </summary>
    private Rigidbody2D _playerRigid = default;
    private SpriteRenderer _playerSprite = default;
    private Transform _transform = default;
    private BoxCollider2D _playerBox = default;
    private GetTargetRay _getTargetRayLeft = default;
    private GetTargetRay _getTargetRayRight = default;
    private Animator _animator;                                     //Animator���擾
    private KnockBack _knockBack = default;

    /// <summary>
    /// Bool�ꗗ
    /// </summary>
    [SerializeField] private bool _isChase = false;
    [SerializeField] private bool _isKill = false;
    [SerializeField] private bool _isTrigger = false;
    [SerializeField] private bool _isChasing = false;

    /// <summary>
    /// Property�ꗗ
    /// </summary>
    public bool IsKill { get => _isKill; set => _isKill = value; }
    public bool IsChase { get => _isChase; set => _isChase = value; }
    public bool IsTrigger { get => _isTrigger; set => _isTrigger = value; }
    public bool IsChasing { get => _isChasing; set => _isChasing = value; }
    public GameObject Enemy { get => _enemy; set => _enemy = value; }
    //public int EnemyKillCount { get => _enemyKillCount; set => _enemyKillCount = value; }
    public int EnemyCombo { get => _enemyCombo; set => _enemyCombo = value; }

    /// <summary>
    /// Ray�ꗗ
    /// </summary>


    /// <summary>
    /// Vecter�ꗗ
    /// </summary>
    private Vector3 _playerDir = default;


    #endregion
    #region �{��


    void Start()
    {
        // transform�ɖ���A�N�Z�X����Əd���̂ŁA�L���b�V�����Ă���
        _transform = transform;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRigid = _player.GetComponent<Rigidbody2D>();
        _playerSprite = _player.GetComponent<SpriteRenderer>();
        _playerBox = _player.GetComponent<BoxCollider2D>();
        _getTargetRayRight = _rightErea.GetComponent<GetTargetRay>();
        _getTargetRayLeft = _leftErea.GetComponent<GetTargetRay>();
        _animator = GetComponent<Animator>();                                            //Animator���擾
        _knockBack = _player.GetComponent<KnockBack>();
    }


    void Update()
    {
        if (!_isChasing)
        {
            _playerRigid.AddForce(new Vector2(0, -200 * Time.deltaTime));
        }
        //_isChase��true�̏ꍇ
        if (_isChase && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            //�G��ǂ�������
            EnemyChase();

            //�ǐՒ��t���O���I���ɂ���
            IsChasing = true;
        }
        //�L�����\��Ԃ̂Ƃ�
        if (_isKill && !_knockBack.IsKnockBack)
        {
            //�v���C���[�̓����蔻���߂�
            _playerBox.enabled = true;

            //�G���^�[�L�[�������ꂽ��
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                //�ǐՒ��t���O���I�t�ɂ���
                IsChasing = false;

                //��ԋ߂��G���L������
                EnemyKill();
            }
        }
    }

    /// <summary>
    /// �G�ɒǏ]���鏈��
    /// </summary>
    private void EnemyChase()
    {
        _leftFoot.SetActive(true);
        _rightFoot.SetActive(true);


        _playerRigid.velocity = Vector2.zero;

        // ���������������v�Z
        _playerDir = (_enemy.transform.position - _player.transform.position);

        //�v���C���[�̌����ɂ���ēG�Ɍ���������ς���
        if (_playerSprite.flipX)
        {
            //�G���A�̈ʒu�𒲐�
            _rightErea.transform.localPosition = new Vector2(_rightErea.transform.localPosition.x, 0);

            //�v���C���[�̌�����ύX
            _player.transform.rotation = Quaternion.FromToRotation(Vector3.right, _playerDir);

            //�A�j���[�V������ύX
            _animator.SetBool("isChase", true);

            //�v���C���[�̓����蔻����ꎞ�I�ɏ���
            //_playerBox.enabled = false;

            //�v���C���[�̌����Ă�������ɔ�΂�
            _playerRigid.velocity = Vector2.zero;
            _playerRigid.gravityScale = default;
            _playerRigid.AddForce(transform.right * _chasePower);
        }
        else
        {
            //�G���A�̈ʒu�𒲐�
            _leftErea.transform.localPosition = new Vector2(_leftErea.transform.localPosition.x, 0);

            //�v���C���[�̌�����ύX
            _player.transform.rotation = Quaternion.FromToRotation(Vector3.left, _playerDir);

            //�A�j���[�V������ύX
            _animator.SetBool("isChase", true);

            //�v���C���[�̓����蔻����ꎞ�I�ɏ���
            _playerBox.enabled = false;

            //�v���C���[�̌����Ă�������ɔ�΂�
            _playerRigid.velocity = Vector2.zero;
            _playerRigid.gravityScale = default;
            _playerRigid.AddForce(-transform.right * _chasePower); 
        }
    }

    /// <summary>
    /// �G��|������
    /// </summary>
    private void EnemyKill()
    {
        //�G�t�F�N�g�̈ʒu��|���G�̈ʒu�ɕύX
        _enemyExplosionEffect.transform.position = _enemy.transform.position;

        //�G�t�F�N�g�𐶐�
        Instantiate(_enemyExplosionEffect);


        //�L�����𑝂₷
        EnemyCombo++;
        if (_enemy.tag == "Enemy" || _enemy.tag == "Stone")
        {
            _enemyKillCount++;
        }

        //_enemy�Ɋi�[����Ă���G������
        _enemy.SetActive(false);

        //���ɋ߂��G��T��
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

        //�߂��G�������ꍇ
        if (_enemy != null)
        {
            //���̓G��ǐՂ���
            EnemyChase();

            //�ǐՒ��t���O���I���ɂ���
            IsChasing = true;
        }
        //�߂��G�����Ȃ��ꍇ
        else
        {
            //�A�j���[�V������ύX
            _animator.SetBool("isChase", false);
            _animator.SetBool("isFall", true);

            //�d�͂�߂�
            _playerRigid.gravityScale = 3;

            //�v���C���[�̌����𒼂�
            _player.transform.rotation = default;

            //�v���C���[�̓����蔻���߂�
            _playerBox.enabled = true;
        }
    }

    /// <summary>
    /// �ǐՒ��ɕǂɓ�����Ǝ~�܂鏈��
    /// </summary>

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && IsChasing)
        {
            IsChasing = false;

            //�v���C���[�̓����蔻���߂�
            _playerBox.enabled = true;

            //�d�͂����ǂ�
            _playerRigid.gravityScale = 3;

            //�A�j���[�V����
            _animator.SetBool("isChase",false);

            //�p�x��߂�
            _player.transform.rotation = default;
        }
    }

    #endregion
}
