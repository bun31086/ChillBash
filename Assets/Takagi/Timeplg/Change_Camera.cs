// --------------------------------------------------------- 
// Change_Camera.cs 
// 
// �쐬��: 7/10
// �쐬��: ���،���
// ---------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Camera : MonoBehaviour
{
    #region �ϐ�

    /**"SerializeField" Inspector���瑀�삪�\,
     * ���̃N���X����̏���������h�~
     * 
     **/
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _camera2;

    #endregion

    #region ���\�b�h
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player�^�O���̈���ɐN�������Ƃ��Ɏ��s
        if (collision.CompareTag("Player"))
        {
            // �J�����̕\���A��\��
            _camera.SetActive(false);
            _camera2.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Player�^�O���̈���ɐN�������Ƃ��Ɏ��s
        if (collision.CompareTag("Player"))
        {
            // �J�����̕\���A��\��
            _camera.SetActive(true);
            _camera2.SetActive(false);
        }
    }

    #endregion
}
