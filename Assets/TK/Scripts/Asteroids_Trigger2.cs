using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���G���A���ɓ����覐΂𗎂Ƃ�����X�N���v�g
/// </summary>

public class Asteroids_Trigger2 : MonoBehaviour
{
    //�Q�[���I�u�W�F�N�g�̊i�[
    [SerializeField] private GameObject _asteroids;
    [SerializeField] private GameObject _asteroids2;
    [SerializeField] private GameObject _asteroids3;
    [SerializeField] private GameObject _asteroids4;

    /*  OnTriggerEnter,���̂����蔲�����u�Ԃ̈�x����̂݌Ă΂��B
 *  ���̂����蔲��������ɂ�
 * �uBoxCollider�v�́uIs Trigger�v�Ƀ`�F�b�N������B
 */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Player�^�O���G�ꂽ�Ƃ��Ɏ��s
        if (collision.CompareTag("Player"))
        {
            //覐΂̕\��
            _asteroids.SetActive(true);
            _asteroids2.SetActive(true);
            _asteroids3.SetActive(true);
            _asteroids4.SetActive(true);
        }    
    }


    /*OnTriggerExit,���̂��ʂ蔲���I�������ɌĂ΂��B
 * �uBoxCollider�v�́uIs Trigger�v�Ƀ`�F�b�N������B
 */
    void OnTriggerExit2D(Collider2D other)
    {
        //�G���A�O�ɏo����G���A���̂��\��
        this.gameObject.SetActive(false);
    }

}
