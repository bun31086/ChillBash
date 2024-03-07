// ---------------------------------------------------------  
// #SCRIPTNAME#.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PauseScript : MonoBehaviour
{

    #region 変数  
    [SerializeField] private Canvas _PauseCanvas; 
    private bool _isPause = false;
    //オプション閉じ音
    [SerializeField] private AudioClip _cancelSound;
    [SerializeField] private Image _arrowup;
    [SerializeField] private Image _arrowmid;
    [SerializeField] private Image _arrowdown;
    [SerializeField] private Image _arrowrit;
    private bool _isarrowUPChange;
    private bool _isarrowMiddleChange;
    private bool _isarrowRetryChange;
    private bool _isarrowDownChange;
    
    /// <summary>
    /// スティック操作判定
    /// </summary>
    private float _horizontal;
    private float _vertical;
    private float _angle = -1f;
    private float _angleReset = 360f;
    //音量調整の値
    private float _volRange = 0.001f;
    [SerializeField] Slider _soundSlider;
    //レバーの値縦上
    private float _leverUp = 90f;
    //レバーの値縦下
    private float _leverDown = 270f;
    //レバーの値縦左
    private float _leverLeft = 180f;
    //レバーの値縦右
    private float _leverRight = 0f;
    //デフォルトできないとき用
    private int _resetNumber = 0;
    //Lever入力時に遅らせる時間(title)
    private float _delayArrowTitleTime = 0.3f;
    private float _delayButtonTime = 0.2f;
    //レバー初期値
    private float _everLever = -1;
    //音声再生用
    [SerializeField] private AudioSource _soundSource;

    [SerializeField] private GameObject _player;

    public bool IsPause { get => _isPause; set => _isPause = value; }
    #endregion

    #region プロパティ  
    private void Start()
    {
        _PauseCanvas.enabled = false;
        _isarrowUPChange = true;
        _isarrowMiddleChange = false;
        _isarrowRetryChange = false;
        _isarrowDownChange = false;
    }
    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update()
    {
        
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _angle = _everLever;
        //何も入力していないとき
        if (_vertical != default || _horizontal != default)
        {
            //座標をラジアンから度数を絶対値として取得
            _angle = Mathf.Atan2(_vertical, _horizontal) * Mathf.Rad2Deg;
            if (_angle < _resetNumber)
            {
                _angle += _angleReset;
            }
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.Escape))
        {
            IsPause = true;
            _player.tag = "muteki";
            _PauseCanvas.GetComponent<Canvas>().enabled = true;
            
        }


        if (IsPause&&(_angle == _leverUp && _isarrowMiddleChange))
        {
            Invoke("UPARROW", _delayArrowTitleTime);
        }

        if (_arrowup)
        {
            if (_angle == _leverLeft && IsPause)
            {

                SceneChange._vol -= _volRange;
            }
            if (_angle == _leverRight && IsPause)
            {
                SceneChange._vol += _volRange;
            }
            _soundSlider.value = SceneChange._vol;
            SceneChange._vol = _soundSlider.value;
            _soundSource.volume = SceneChange._vol;

            _arrowup.GetComponent<Image>().enabled = true;
            _arrowmid.GetComponent<Image>().enabled = false;
            _arrowdown.GetComponent<Image>().enabled = false;
            _arrowrit.GetComponent<Image>().enabled = false;

        }

        if ((_angle == _leverUp && _isarrowRetryChange)
            )
        {
            Invoke("MIDDLEARROW", _delayButtonTime);

        }

        if (IsPause&&(_angle == _leverDown && _isarrowUPChange)
            )
        {
            Invoke("MIDDLEARROW", _delayButtonTime);

        }

        if (_isarrowMiddleChange)
        {
            _arrowup.GetComponent<Image>().enabled = false;
            _arrowmid.GetComponent<Image>().enabled = true;
            _arrowdown.GetComponent<Image>().enabled = false;
            _arrowrit.GetComponent<Image>().enabled = false;
            if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Return))
            {
                GAMEBACK();
            }
        }

        if (IsPause&&
            (_angle == _leverDown && _isarrowMiddleChange))
        {
            Invoke("LicARROW", _delayButtonTime);

        }

        if (IsPause &&
            (_angle == _leverUp && _isarrowDownChange))
        {
            Invoke("LicARROW", _delayButtonTime);

        }

        if (_isarrowRetryChange)
        {
            _arrowup.GetComponent<Image>().enabled = false;
            _arrowmid.GetComponent<Image>().enabled = false;
            _arrowrit.GetComponent<Image>().enabled = true;
            _arrowdown.GetComponent<Image>().enabled = false;
            if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Return))
            {
                RETRY();
            }
        }

        if (IsPause &&
            _angle == _leverDown && _isarrowRetryChange
           )
        {
            Invoke("DOWNARROW", _delayButtonTime);
        }

        if (_isarrowDownChange)
        {
            _arrowup.GetComponent<Image>().enabled = false;
            _arrowmid.GetComponent<Image>().enabled = false;
            _arrowdown.GetComponent<Image>().enabled = true;
            _arrowrit.GetComponent<Image>().enabled = false;
            if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Return))
            {
                GAMEEND();
            }
        }
    }
    #endregion

    #region メソッド  
    public void UPARROW()
    {
        _isarrowUPChange = true;
        _isarrowMiddleChange = false;
        _isarrowRetryChange = false;
        _isarrowDownChange = false;
    }
    public void MIDDLEARROW()
    {
        _isarrowUPChange = false;
        _isarrowMiddleChange = true;
        _isarrowRetryChange = false;
        _isarrowDownChange = false;
    }
    public void DOWNARROW()
    {
        _isarrowUPChange = false;
        _isarrowMiddleChange = false;
        _isarrowRetryChange = false;
        _isarrowDownChange = true;
    }
    public void LicARROW()
    {
        _isarrowUPChange = false;
        _isarrowMiddleChange = false;
        _isarrowRetryChange = true;
        _isarrowDownChange = false;
    }

    public void GAMEBACK()
    {
        Time.timeScale = 1;
        _PauseCanvas.enabled = false;
        IsPause = false;
        _player.tag = "Player";
    }

    public void RETRY()
    {
        SceneManager.LoadScene("Test");
        Debug.Log("aaaaaaaaaaaaaaaa");
    }

    public void GAMEEND()
    {
        Application.Quit();
        Debug.Log("sssssssssssssssssssssssssssssss");
    }
    #endregion
}
