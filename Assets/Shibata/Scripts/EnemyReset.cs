using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReset : MonoBehaviour
{    
    private bool _reset = false;�@// Enemy�����̈ʒu�֖߂�p�̏���

    private GameObject _Enemy;�@//�@Enemy�ϐ�

    private Vector2 _enemyReset;�@// Enemy�̏����ʒu��ݒ肷�邽�߂̕ϐ�

    private EnemysMove _enemyMove;�@// �X�N���v�g�擾�p�̕ϐ��쐬

    private GameObject playerobj; // Player�擾�p�̕ϐ�

    [SerializeField] private GameObject _pauseObj = default;
    private PauseScript _pauseScript = default;

    void Start()
    {
        _enemyReset = this.transform.position;�@// Enemy�̏����ʒu�ݒ�
        _enemyMove = transform.root.gameObject.GetComponent<EnemysMove>();�@// �X�N���v�g�擾
        playerobj = GameObject.FindGameObjectWithTag("Player"); //�@Player�擾

        _pauseScript = _pauseObj.GetComponent<PauseScript>();
    }

    void Update()
    {
        // ������false�ɂȂ��Ă���ꍇ���̈ʒu��
        if (_reset == false)
        {
            Invoke("Back", 3);
        }


    }
    private void Back()
    {
        _enemyMove.GoBack();
    }


    private void OnTriggerStay2D(Collider2D collider2D)
    {
        // Player������Enemy��x����Ƃ��Ăǂ���ɂ���̂��ɂ���ĕ����]��
        if(playerobj.transform.position.x < this.transform.position.x)
        {
            _enemyMove.Right();
        }
        if (playerobj.transform.position.x > this.transform.position.x)
        {
            _enemyMove.Left();
        }


        //Player���͈͂ɓ����Ă���ꍇEnemyMove��Controller�X�N���v�g���Ăяo��
        if (collider2D.CompareTag("Player") && !_enemyMove.IsDamage && !_pauseScript.IsPause)
        {
            print("trigger");
            CancelInvoke();
            _enemyMove.Controller();
        }
    }
    private void OnTriggerExit2D(Collider2D collider2D)
    {

        // Player���O�ɏo���������false�ɐݒ�
        if (collider2D.CompareTag("Player"))
        {
            _reset = false;
        }
    }

}
