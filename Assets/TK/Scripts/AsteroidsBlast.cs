using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 覐΂𔚔j������X�N���v�g
/// </summary>


public class AsteroidsBlast : MonoBehaviour
{
    //�Q�[���I�u�W�F�N�g���̊i�[
    [SerializeField] private SpriteRenderer _asteroids_Sprite;
    [SerializeField] private CapsuleCollider2D _asteroids_Collider;
    [SerializeField] private GameObject _asteroids;
    [SerializeField] private GameObject _asteroids_Halo;
    [SerializeField] private GameObject _asteroids_Trajectory;
    [SerializeField] private GameObject _blast;
    [SerializeField] private GameObject _smoke;


   

    void Update()
    {
        //覐΂̃X�v���C�g����\���ɂȂ�Ǝ��s
        if (_asteroids_Sprite.enabled == false)
        {
            //覐΂̃R���C�_�[���\����
            _asteroids_Collider.enabled = false;
            //3�b���"Disable_Object"�����s
            Invoke(nameof(Disable_Object), 3.0f);
        }
    }


    /*  OnCollisionEnter,���̓��m���Փ˂����u�Ԃ̈�x����̂݌Ă΂��B
 *�uBoxCollider�v�́uIs Trigger�v�̃`�F�b�N���O���B
 */
    private void OnCollisionEnter2D(Collision2D other)
    {
        //�n�ʂɏՓ˂����Ƃ��Ɏ��s
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Player"))
        {
            //覐΂̃X�v���C�g�A�O�ՁA����̔�\����
            _asteroids_Sprite.enabled = false;
            _asteroids_Halo.SetActive(false);
            _asteroids_Trajectory.SetActive(false);

            //�p�[�e�B�N���̕\��
            _blast.SetActive(true);
            _smoke.SetActive(true);
        }
    }

    //覐΂̃Q�[���I�u�W�F�N�g���\���������郁�\�b�h
    private void Disable_Object()
    {
        //覐΂̔�\����
        _asteroids.SetActive(false);
    }
}
