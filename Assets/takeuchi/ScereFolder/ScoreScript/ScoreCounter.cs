// ---------------------------------------------------------  
// ScoreCounter.cs  
//   
// �쐬��:  2023/7/6
// �쐬��:  Takeuchi Shinnosuke
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class ScoreCounter : MonoBehaviour
{

    #region �ϐ�  
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timeText;
    public float _timer = 150;
    private float _nowScore = default;
    private int _lowAddScore = 10;
    private int _normalAddScore = 100;
    private int _highAddScore = 500;
    private float _totalScore = default;
    private int[] _lankScore = { 600, 450, 200, 0 };
    [SerializeField] private Image[] _lankImg = new Image[4];
    [SerializeField] private Text _timeImg;
    [SerializeField] private Text _scoreImg;

    //private Attack _attack = default;
    //private GameObject _player = default;

    #endregion

    #region �v���p�e�B  
    /// <summary>
    /// �X�V�O����
    /// </summary>
    public void Start()
    {
        _timeImg.enabled = false;
        _scoreImg.enabled = false;
        //_player = GameObject.FindGameObjectWithTag("Player");
        //_attack = _player.GetComponent<Attack>();
        _nowScore = Attack._enemyKillCount;
        _timer = Count_Timer._countdownSeconds;
    }

    /// <summary>  
    /// �X�V����  
    /// </summary>  
    void Update()
    {
        _timeText.text = String.Format("Time:{0}s", 180 - Mathf.Ceil(_timer));
        _scoreText.text = "Enemy:" + _nowScore.ToString() + "�^81";
        _totalScore = _timer + _nowScore * 10;
        Debug.Log(_timeImg.enabled);
    }

    #endregion

    #region ���\�b�h  

    private void RANKOPEN()
    {
        for (int i = 0; i < _lankScore.Length; i++)
        {
            //�����N������牺���Ă���
            if (_totalScore >= _lankScore[i])
            {
                _lankImg[i].enabled = true;
                return;
            }
        }
    }

    private void TIMEOPEN()
    {
        _timeImg.enabled = true;
    }

    private void SCOREOPEN()
    {
        _scoreImg.enabled = true;
    }

    private void RANKRESET()
    {
        for (int i = 0; i < _lankImg.Length; i++)
        {
            _lankImg[i].enabled = false;
        }
    }

    #endregion
}

