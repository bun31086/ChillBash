using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Concurrent;

/// <summary>
/// �v���C���[�Ƃ̊Ԃɏ�Q����������ԋ߂��G��T������
/// </summary>
public class GetTargetRay : MonoBehaviour
{
    #region �ϐ�
    /// <summary>
    /// Float,int�ꗗ
    /// </summary>
    private float _rayLength = 30;                         //ray�̒���
    [SerializeField]private float _minValue = 100f;        //��ԋ߂��G���擾����ۂɎg�������̏����l
    private const float CONST_KILL_EREA = 7;  //�L���\����

    /// <summary>
    /// Ray�ꗗ
    /// </summary>
    private int _layerMask = 1 << 6;�@�@�@�@�@�@�@�@�@�@�@//layer6��������������
    private RaycastHit2D _censorRay = default;           //�Z���T�[�ƂȂ�Ray���o��

    /// <summary>
    /// Vecter�ꗗ
    /// </summary>

    /// <summary>
    /// GameObject�ꗗ
    /// </summary>
    private GameObject _player = default;
    [SerializeField]private GameObject _nearEnemy = default;�@
    [SerializeField] private List<GameObject> _inEnemyList = new List<GameObject>(); //�G������z��
    [SerializeField] private GameObject _aimObj = default;

    /// <summary>
    /// Compornent�ꗗ
    /// </summary>
    private SpriteRenderer _spriteRenderer = default;
    private Attack _attackScript = default;
    private KnockBack _knockBack = default;

    /// <summary>
    /// Sprite�ꗗ
    /// </summary>
    [SerializeField] private Sprite _aimRed = default;
    [SerializeField] private Sprite _aimBlue = default;

    /// <summary>
    /// bool�ꗗ
    /// </summary>
    //private bool _iskill = false;
    private bool _isReset = false;

    #endregion
    #region �{��

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
            //�L�����s�\�ɂȂ�
            KillModeOFF();

            //�Ə����ǂ����֔�΂�
            _aimObj.transform.position = new Vector2(20, 80);

        }
    }

    public void EnemySearch()
    {
        //�z��ɓG�������Ă���ꍇ
        if (_inEnemyList.Count != 0)
        {
            //Ray�ƓG�Ƃ̋�����������
            _rayLength = 30f;
            _minValue = 100f;

            //�t���O������
            _isReset = false;

            //�z��ɓ����Ă���G�̐��A�J��Ԃ�
            foreach (GameObject Enemy in _inEnemyList)
            {
                //Ray���o��
                _censorRay = Physics2D.Raycast(_player.transform.position, Enemy.transform.position - _player.transform.position, _rayLength + 0.2f, _layerMask);
                //Debug.DrawRay(_player.transform.position, (Enemy.transform.position - _player.transform.position) * (_rayLength + 0.2f));

                //Ray��Enemy���������Ă��āA_minValue�����������߂��ꍇ
                if (_censorRay.collider != null)
                {
                    if (_minValue > Vector2.Distance(Enemy.transform.position, _player.transform.position) && (_censorRay.collider.tag == "Enemy" || _censorRay.collider.tag == "BossEnemy" || _censorRay.collider.tag == "Stone"))
                    {
                        //foreach�̒��ň��ł��G���擾�����Ƃ�
                        _isReset = true;

                        //���̓G��_nearEnemy�Ɋi�[���A�������X�V
                        _nearEnemy = Enemy;
                        _minValue = Vector2.Distance(Enemy.transform.position, _player.transform.position);

                        //������KILL_EREA���߂��ꍇ
                        if (_minValue < CONST_KILL_EREA)
                        {
                            //�L�����\�ɂȂ�
                            KillModeON();
                        }
                        //������KILL_EREA��艓���ꍇ
                        else
                        {
                            //�L�����s�\�ɂȂ�
                            KillModeOFF();
                        }
                    }
                    //Floor�^�O��Object�ɓ�����A�܂������G���擾���Ă��Ȃ��ꍇ
                    else if (_censorRay.collider.tag == "Floor" && !_isReset)
                    {
                        //_nearEnemy����ɂ���
                        _nearEnemy = null;
                    }
                }
            }
            //��ԋ߂��G������ꍇ
            if (_nearEnemy != null)
            {
                //�Ə�����ԋ߂��G�ɍ��킹��
                _aimObj.transform.position = _nearEnemy.transform.position;

                //�L���\��Ԃ̂Ƃ�
                if (_attackScript.IsKill)
                {
                    //Chase���s�\�ɂȂ�
                    _attackScript.IsChase = false;

                    //�v���C���[�̃g���K�[���I�t�ɂ���
                    _attackScript.IsTrigger = false;
                }
                //�L���\��Ԃł͖����Ƃ�
                else
                {
                    //Chase���\�ɂȂ�
                    _attackScript.IsChase = true;
                }
            }
            //��ԋ߂��G�����Ȃ��ꍇ
            else
            {
                //Chase���s�\�ɂȂ�
                _attackScript.IsChase = false;

                //�Ə����ǂ����֔�΂�
                _aimObj.transform.position = new Vector2(20, 20);
            }
        }
        //�z��ɓG�������Ă��Ȃ��ꍇ
        else
        {
            //�L�����s�\�ɂȂ�
            KillModeOFF();

            //_nearEnemy�̒��g����ɂ���
            _nearEnemy = null;

            //�Ə����ǂ����֔�΂�
            _aimObj.transform.position = new Vector2(-200, 50);

            //Chase���s�\�ɂȂ�
            _attackScript.IsChase = false;
        }
        _attackScript.Enemy = _nearEnemy;
    }

    /// <summary>
    /// �L�����\�ȂƂ�
    /// </summary>
    private void KillModeON()
    {
        //Chase���s�\�ɂȂ�
        _attackScript.IsChase = false;
        
        //�U���\�t���OON
        _attackScript.IsKill = true;

        //�Ə���ԂɕύX
        _spriteRenderer.sprite = _aimRed;
    }

    /// <summary>
    /// �L�����s�\�ȂƂ�
    /// </summary>
    private void KillModeOFF()
    {
        //�U���\�t���OOFF
        _attackScript.IsKill = false;

        //�Ə���ɕύX
        _spriteRenderer.sprite = _aimBlue;
    }

    /// <summary>
    /// �g���K�[�ɓ������G��z��ɓ���鏈��
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�g���K�[�ɓ������I�u�W�F�N�g�̃^�O��Enemy�������ꍇ
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy" || collision.gameObject.tag == "Stone")
        {
            //�z��̂��̃I�u�W�F�N�g��ǉ�
            _inEnemyList.Add(collision.gameObject);
        }
    }

    /// <summary>
    /// �g���K�[���甲�����G��z�񂩂甲������
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        //�g���K�[���甲�����I�u�W�F�N�g�̃^�O��AirEnemy�������ꍇ
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy" || collision.gameObject.tag == "Stone")
        {
            //�z��̒��ɂ���I�u�W�F�N�g�ƏƂ炵���킹�鏈��
            foreach (GameObject search in _inEnemyList)
            {
                //�g���K�[���甲�����I�u�W�F�N�g���z��ɂ���ꍇ
                if(search == collision.gameObject)
                {
                    //�z�񂩂炻�̃I�u�W�F�N�g���폜
                    _inEnemyList.Remove(search);
                    return;
                }
            }
        }
    }
    #endregion
}
