// --------------------------------------------------------- 
// LifeManager.cs 
// 
// �쐬��: 6/28
// �쐬��: ���،���
// ---------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    #region �ϐ� 
    /** "SerializeField" Inspector���瑀�삪�\,
     * ���̃N���X����̏���������h�~
     * 
     **/

    [SerializeField] private GameObject _icon;
    // ���C�t�|�C���g�p�̔z��
    [SerializeField] private GameObject[] _hearts;
    // ���C�t�|�C���g�̐��p�̕ϐ�
    [SerializeField] private int _hpValue = default;

    /** �萔�Ƃ́A�l���Œ肳��
     * �ύX���邱�Ƃ��ł��Ȃ��l
     **/

    // �J�E���g�p�̒萔
    private const int MAX_COUNT = 2;
    #endregion

    #region ���\�b�h

    /// <summary>
    /// <para>UpdateLife</para>
    /// <para>���C�t�|�C���g��������</para>
    /// </summary>
    /// 
    public void UpdateLife() 
    {
        // �z��̒��̃��C�t�|�C���g���\��
        _hearts[_hpValue].SetActive(false);

        /** 
         * ���C�t�|�C���g�̌��݊i�[����Ă���ꏊ < ���C�t�|�C���g�̍��v��
         * ���C�t�|�C���g�̌��݊i�[����Ă���ꏊ = �z��ԍ�(0,1,2)
         * ���C�t�|�C���g�̍��v�� = 3
         * 
         **/

        if (_hpValue < _hearts.Length) 
        {
            // ���C�t�|�C���g�̔z��𓮂���
            _hpValue++;

            /** 
             * ���C�t�|�C���g�̌��݊i�[����Ă���ꏊ < MAX_COUNT(2)
             * ���C�t�|�C���g�̌��݊i�[����Ă���ꏊ = �z��ԍ�(0,1,2)
             * 
             **/


            if (_hpValue == MAX_COUNT)
            {
                _icon.SetActive(true);
            }
            if (_hpValue > MAX_COUNT) 
            {
                // �Q�[���I�[�o�[�V�[���ɑJ��
                SceneManager.LoadScene("OverScene");
            }
        } 

    }

    #endregion
}
