// --------------------------------------------------------- 
// Count_Timer.cs 
// 
// �쐬��: 7/13
// �쐬��: ���،���
// ---------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Count_Timer : MonoBehaviour
{
    #region �ϐ�

    /**"SerializeField" Inspector���瑀�삪�\,
     * ���̃N���X����̏���������h�~
     * 
     **/

    // �^�C�}�[�\���p�e�L�X�g�ϐ�
    [SerializeField] private TMPro.TMP_Text _timeText;

    // �O��Update�̎��̕b��
    public float _oldSeconds = default;

    // �������Ԃ̕b��
    public static float _countdownSeconds = default;


    private PauseScript _pauseScript = default;
    [SerializeField] private GameObject _pauseObj = default;

    private bool _timerEnd = false;

    public bool TimerEnd { get => _timerEnd; set => _timerEnd = value; }

    #endregion

    #region ���\�b�h
    void Start()
    {
        // �������ԂƂȂ�b�����i�[
        _countdownSeconds = 180;

        /** ��r�p�̕b�����i�[
         *  �������l���i�[���邱��
         **/
        _oldSeconds = 180f;

        // �e�L�X�g�R���|�[�l���g�̎擾
        _timeText = GetComponent<TMP_Text>();

        _pauseScript = _pauseObj.GetComponent<PauseScript>();
    }


    void Update()
    {
        // �������Ԃ�0�b�ɂȂ�܂Ŏ��s
        if (_countdownSeconds >= 0f)
        {

            if (!_pauseScript.IsPause)
            {
                //�@�J�E���g�_�E��������
                _countdownSeconds -= Time.deltaTime;
            }
        }

        // �l���ς�������A�e�L�X�gUI��s�x�X�V
        if ((int)_countdownSeconds != (int)_oldSeconds)
        {

            // �b���̕\��
            _timeText.text = ((int)_countdownSeconds).ToString("000");
        }

        // ��r�Ώۂ̍X�V
        _oldSeconds = _countdownSeconds;

        // �b����0�b�ɂȂ�܂Ŏ��s
        if (_countdownSeconds <= 0f)
        {
            TimerEnd = true;
        }

    }
    #endregion
}
