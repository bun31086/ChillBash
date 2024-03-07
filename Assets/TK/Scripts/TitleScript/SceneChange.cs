// ---------------------------------------------------------  
// SceneChange.cs  
//   
// 作成日:  2023/6/10
// 作成者:  Takeuchi Shinnosuke
// ---------------------------------------------------------  

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class SceneChange : MonoBehaviour
{
    #region 変数
    //タイトル画面のキャンバス
    [SerializeField] private Canvas _titleCanvas;
    //オプション画面のキャンバス
    [SerializeField] private Canvas _optionCanvas;
    //[SerializeField] private AudioClip _seCanvas;
    //オプション決定音
    [SerializeField] private AudioClip _successSound;
    //オプション閉じ音
    [SerializeField] private AudioClip _cancelSound;
    //音声再生用
    [SerializeField] private AudioSource _soundSource;
    //画面揺れbutton
    [SerializeField] private Button _shakeButton;
    //揺れOK画像
    [SerializeField] private Sprite _shakeOKIMG;
    //揺れNG画像
    [SerializeField] private Sprite _shakeNGIMG;
    //画面揺れボタンONOFF判定
    private bool _isShakeButton = true;
    //矢印画像
    [SerializeField] private Image _arrowUp;
    [SerializeField] private Image _arrowRight;
    [SerializeField] private Image _arrowLeft;
    [SerializeField] private Image _arrowOptionUp;
    [SerializeField] private Image _arrowOptionMiddle;
    [SerializeField] private Image _arrowOptionDown;
    [SerializeField] private Image _arrowOptionLicense;
    //画面内の矢印を変えたことを検出するやつ
    private bool _isarrowOptionUPChange = true;
    private bool _isarrowOptionMiddleChange = false;
    private bool _isarrowOptionDownChange = false;
    private bool _isarrowOptionLicenseChange = false;
    private bool _isarrowChange = false;
    private bool _isExit = false;
    private bool _isStartArrow = true;
    private bool _isLicense = false;
    //オプション画面のbuttonを押したかどうか
    private bool _isButton = false;
    [SerializeField] private Image _ShakeBackGround;
    /// <summary>
    /// スティック操作判定
    /// </summary>
    private float _horizontal;
    private float _vertical;
    private float _angle = -1f;
    private float _angleReset = 360f;
    //音楽スライダー
    public static float _vol = 0.5f;
    //音量調整の値
    private float _volRange = 0.08f;
    [SerializeField] Slider _soundSlider;
    //オプション画面の背景
    [SerializeField] private SpriteRenderer _optionVideo;
    //クレジット画面
    [SerializeField] private Image _licenseTexture;
    //レバーの値縦左
    private float _leverLeft = 180f;
    //レバーの値縦右
    private float _leverRight = 0f;
    //レバーの値縦上
    private float _leverUp = 90f;
    //レバーの値縦下
    private float _leverDown = 270f;
    //デフォルトできないとき用
    private int _resetNumber = 0;
    //Lever入力時に遅らせる時間(title)
    private float _delayArrowTitleTime = 0.3f;
    //Lever入力時に遅らせる時間(option)
    private float _delayButtonTime = 0.2f;
    //Lever入力時に遅らせる時間(gameend)
    private float _delayByeTime = 0.5f;
    //レバー初期値
    private float _everLever = -1;
    #endregion
    #region 本編
    public void Start()
    {
        _optionCanvas.GetComponent<Canvas>().enabled = false;
        _licenseTexture.enabled = false;
    }
    public void Update()
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
        ///<summary>
        ///タイトル画面の設定
        /// </summary>
        //右
        if (_titleCanvas.GetComponent<Canvas>().enabled == true && _angle == _leverRight && _isStartArrow)
        {
            Invoke("TITLERIGHTARROWCHANGE", _delayArrowTitleTime);
        }
        //ゲームスタートに矢印が向くやつ
        if (_titleCanvas.GetComponent<Canvas>().enabled == true && _angle == _leverLeft && _isarrowChange)
        {
            Invoke("TITLEUPARROWCHANGE", _delayArrowTitleTime);
        }

        if (_titleCanvas.GetComponent<Canvas>().enabled == true && _angle == _leverRight && _isExit)
        {
            Invoke("TITLEUPARROWCHANGE", _delayArrowTitleTime);
        }

        //左
        if (_titleCanvas.GetComponent<Canvas>().enabled == true && _angle == _leverLeft && _isStartArrow)
        {
            Invoke("TITLELEFTARROWCHANGE", _delayArrowTitleTime);

        }

        if (_isExit)
        {
            Invoke("TITLELEFTARROW", _delayButtonTime);
            if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Return))
            {
                EndGameOnButton();
            }
        }

        if (_isarrowChange)
        {
            Invoke("TITLERIGHTARROW", _delayButtonTime);
            if ((Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Return)) && _isButton == false)
            {

                StartOptionOnClick();
            }
        }

        if (_isStartArrow)
        {
            Invoke("TITLEUPARROW", _delayButtonTime);
            if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Test");
            }
        }


        ///<summary>
        ///オプション画面の矢印
        ///</summary>

        if (_isarrowChange && _optionCanvas.GetComponent<Canvas>().enabled == true &&
            (_angle == _leverUp && _arrowOptionMiddle.GetComponent<Image>().enabled == true)
            && _isLicense == false)
        {
            Invoke("UPARROW", _delayButtonTime);
        }

        if (_isarrowOptionUPChange)
        {
            if (_angle == _leverLeft && _optionCanvas.GetComponent<Canvas>().enabled == true)
            {

                _vol -= _volRange * Time.deltaTime;
            }
            if (_angle == _leverRight && _optionCanvas.GetComponent<Canvas>().enabled == true)
            {
                _vol += _volRange * Time.deltaTime;
            }
            _soundSlider.value = _vol;
            _vol = _soundSlider.value;
            _soundSource.volume = _vol;

            _arrowOptionUp.GetComponent<Image>().enabled = true;
            _arrowOptionMiddle.GetComponent<Image>().enabled = false;
            _arrowOptionDown.GetComponent<Image>().enabled = false;
            _arrowOptionLicense.GetComponent<Image>().enabled = false;

        }

        if (_isarrowChange && _optionCanvas.GetComponent<Canvas>().enabled == true &&
            (_angle == _leverUp && _arrowOptionLicense.GetComponent<Image>().enabled == true)
            && _isLicense == false)
        {
            Invoke("MIDDLEARROW", _delayButtonTime);

        }

        if (_isarrowChange && _optionCanvas.GetComponent<Canvas>().enabled == true &&
            (_angle == _leverDown && _arrowOptionUp.GetComponent<Image>().enabled == true)
            && _isLicense == false)
        {
            Invoke("MIDDLEARROW", _delayButtonTime);

        }

        if (_isarrowOptionLicenseChange)
        {
            _arrowOptionUp.GetComponent<Image>().enabled = false;
            _arrowOptionMiddle.GetComponent<Image>().enabled = false;
            _arrowOptionDown.GetComponent<Image>().enabled = false;
            _arrowOptionLicense.GetComponent<Image>().enabled = true;
            if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Return))
            {
                LicenseOnClick();
            }
        }

        if (_isarrowChange && _optionCanvas.GetComponent<Canvas>().enabled == true &&
            (_angle == _leverDown && _arrowOptionMiddle.GetComponent<Image>().enabled == true))
        {
            Invoke("LicARROW", _delayButtonTime);

        }

        if (_isarrowChange && _optionCanvas.GetComponent<Canvas>().enabled == true &&
            (_angle == _leverUp && _arrowOptionDown.GetComponent<Image>().enabled == true))
        {
            Invoke("LicARROW", _delayButtonTime);

        }

        if (_isarrowOptionMiddleChange)
        {
            _arrowOptionUp.GetComponent<Image>().enabled = false;
            _arrowOptionMiddle.GetComponent<Image>().enabled = true;
            _arrowOptionDown.GetComponent<Image>().enabled = false;
            _arrowOptionLicense.GetComponent<Image>().enabled = false;
            if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Return))
            {
                ShakeOnClick();
            }
        }

        if (_isarrowChange && _optionCanvas.GetComponent<Canvas>().enabled == true &&
            _angle == _leverDown && _arrowOptionLicense.GetComponent<Image>().enabled == true
            && _isLicense == false)
        {
            Invoke("DOWNARROW", _delayButtonTime);
        }

        if (_isarrowOptionDownChange)
        {
            _arrowOptionUp.GetComponent<Image>().enabled = false;
            _arrowOptionMiddle.GetComponent<Image>().enabled = false;
            _arrowOptionDown.GetComponent<Image>().enabled = true;
            _arrowOptionLicense.GetComponent<Image>().enabled = false;
            if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Return))
            {
                EndOptionOnClick();
            }
        }


    }
    #endregion
    #region メソッド
    private void TITLEUPARROW()
    {
        _arrowUp.GetComponent<Image>().enabled = true;
        _arrowRight.GetComponent<Image>().enabled = false;
        _arrowLeft.GetComponent<Image>().enabled = false;
    }
    private void TITLERIGHTARROW()
    {
        _arrowUp.GetComponent<Image>().enabled = false;
        _arrowRight.GetComponent<Image>().enabled = true;
        _arrowLeft.GetComponent<Image>().enabled = false;
    }
    private void TITLELEFTARROW()
    {
        _arrowUp.GetComponent<Image>().enabled = false;
        _arrowRight.GetComponent<Image>().enabled = false;
        _arrowLeft.GetComponent<Image>().enabled = true;
    }
    private void TITLEUPARROWCHANGE()
    {
        _isarrowChange = false;
        _isExit = false;
        _isStartArrow = true;
    }
    private void TITLELEFTARROWCHANGE()
    {
        _isarrowChange = false;
        _isExit = true;
        _isStartArrow = false;
    }
    private void TITLERIGHTARROWCHANGE()
    {
        _isarrowChange = true;
        _isExit = false;
        _isStartArrow = false;
    }

    public void UPARROW()
    {
        _isarrowOptionUPChange = true;
        _isarrowOptionMiddleChange = false;
        _isarrowOptionDownChange = false;
        _isarrowOptionLicenseChange = false;
    }
    public void MIDDLEARROW()
    {
        _isarrowOptionUPChange = false;
        _isarrowOptionMiddleChange = true;
        _isarrowOptionDownChange = false;
        _isarrowOptionLicenseChange = false;
    }
    public void DOWNARROW()
    {
        _isarrowOptionUPChange = false;
        _isarrowOptionMiddleChange = false;
        _isarrowOptionDownChange = true;
        _isarrowOptionLicenseChange = false;
    }
    public void LicARROW()
    {
        _isarrowOptionUPChange = false;
        _isarrowOptionMiddleChange = false;
        _isarrowOptionDownChange = false;
        _isarrowOptionLicenseChange = true;
    }

    //ゲームスタートbuttonが押されたら
    public void StartGameOnButton()
    {
        SceneManager.LoadScene("");
    }
    //ゲーム終了buttonが押されたら落ちるよ
    public void EndGameOnButton()
    {
        Application.Quit();
    }
    //オプションbuttonが押されたら選択音を鳴らしてオプション画面開くよ
    public void StartOptionOnClick()
    {
        _isButton = true;
        if (_isButton == true && _optionCanvas.GetComponent<Canvas>().enabled == false)
        {
            _soundSource.PlayOneShot(_successSound);
        }

        _optionVideo.enabled = true;
        _titleCanvas.GetComponent<Canvas>().enabled = false;
        _optionCanvas.GetComponent<Canvas>().enabled = true;

    }
    //オプション閉じbuttonが押されたらオプション画面が閉じるよ
    public void EndOptionOnClick()
    {
        _isButton = false;
        _isarrowOptionUPChange = true;
        _isarrowOptionMiddleChange = false;
        _isarrowOptionDownChange = false;
        _soundSource.PlayOneShot(_cancelSound);
        _optionVideo.enabled = false;
        _titleCanvas.GetComponent<Canvas>().enabled = true;
        _optionCanvas.GetComponent<Canvas>().enabled = false;
    }

    //画面揺れボタンをクリックしたとき
    public void ShakeOnClick()
    {
        if (_isShakeButton)
        {
            _soundSource.PlayOneShot(_successSound);
            _isShakeButton = false;
            _shakeButton.image.sprite = _shakeNGIMG;
            _ShakeBackGround.enabled = false;
        }
        else
        {
            _soundSource.PlayOneShot(_successSound);
            _isShakeButton = true;
            _shakeButton.image.sprite = _shakeOKIMG;
            _ShakeBackGround.enabled = true;
        }
    }

    public void LicenseOnClick()
    {
        if (_isLicense)
        {
            _soundSource.PlayOneShot(_successSound);
            _isLicense = false;
            _licenseTexture.enabled = false;
        }
        else
        {
            _soundSource.PlayOneShot(_successSound);
            _isLicense = true;
            _licenseTexture.enabled = true;
        }
    }
    /*
    public void SEOnClick() {
        _soundSource.PlayOneShot(_seCanvas);
        _soundSource.pitch += 0.1f;
    }
    */
    #endregion
}