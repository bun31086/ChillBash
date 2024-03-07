// ---------------------------------------------------------  
// SceneChange.cs  
//   
// �쐬��:  2023/6/10
// �쐬��:  Takeuchi Shinnosuke
// ---------------------------------------------------------  

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class SceneChange : MonoBehaviour
{
    #region �ϐ�
    //�^�C�g����ʂ̃L�����o�X
    [SerializeField] private Canvas _titleCanvas;
    //�I�v�V������ʂ̃L�����o�X
    [SerializeField] private Canvas _optionCanvas;
    //[SerializeField] private AudioClip _seCanvas;
    //�I�v�V�������艹
    [SerializeField] private AudioClip _successSound;
    //�I�v�V��������
    [SerializeField] private AudioClip _cancelSound;
    //�����Đ��p
    [SerializeField] private AudioSource _soundSource;
    //��ʗh��button
    [SerializeField] private Button _shakeButton;
    //�h��OK�摜
    [SerializeField] private Sprite _shakeOKIMG;
    //�h��NG�摜
    [SerializeField] private Sprite _shakeNGIMG;
    //��ʗh��{�^��ONOFF����
    private bool _isShakeButton = true;
    //���摜
    [SerializeField] private Image _arrowUp;
    [SerializeField] private Image _arrowRight;
    [SerializeField] private Image _arrowLeft;
    [SerializeField] private Image _arrowOptionUp;
    [SerializeField] private Image _arrowOptionMiddle;
    [SerializeField] private Image _arrowOptionDown;
    [SerializeField] private Image _arrowOptionLicense;
    //��ʓ��̖���ς������Ƃ����o������
    private bool _isarrowOptionUPChange = true;
    private bool _isarrowOptionMiddleChange = false;
    private bool _isarrowOptionDownChange = false;
    private bool _isarrowOptionLicenseChange = false;
    private bool _isarrowChange = false;
    private bool _isExit = false;
    private bool _isStartArrow = true;
    private bool _isLicense = false;
    //�I�v�V������ʂ�button�����������ǂ���
    private bool _isButton = false;
    [SerializeField] private Image _ShakeBackGround;
    /// <summary>
    /// �X�e�B�b�N���씻��
    /// </summary>
    private float _horizontal;
    private float _vertical;
    private float _angle = -1f;
    private float _angleReset = 360f;
    //���y�X���C�_�[
    public static float _vol = 0.5f;
    //���ʒ����̒l
    private float _volRange = 0.08f;
    [SerializeField] Slider _soundSlider;
    //�I�v�V������ʂ̔w�i
    [SerializeField] private SpriteRenderer _optionVideo;
    //�N���W�b�g���
    [SerializeField] private Image _licenseTexture;
    //���o�[�̒l�c��
    private float _leverLeft = 180f;
    //���o�[�̒l�c�E
    private float _leverRight = 0f;
    //���o�[�̒l�c��
    private float _leverUp = 90f;
    //���o�[�̒l�c��
    private float _leverDown = 270f;
    //�f�t�H���g�ł��Ȃ��Ƃ��p
    private int _resetNumber = 0;
    //Lever���͎��ɒx�点�鎞��(title)
    private float _delayArrowTitleTime = 0.3f;
    //Lever���͎��ɒx�点�鎞��(option)
    private float _delayButtonTime = 0.2f;
    //Lever���͎��ɒx�点�鎞��(gameend)
    private float _delayByeTime = 0.5f;
    //���o�[�����l
    private float _everLever = -1;
    #endregion
    #region �{��
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

        //�������͂��Ă��Ȃ��Ƃ�
        if (_vertical != default || _horizontal != default)
        {
            //���W�����W�A������x�����Βl�Ƃ��Ď擾
            _angle = Mathf.Atan2(_vertical, _horizontal) * Mathf.Rad2Deg;
            if (_angle < _resetNumber)
            {
                _angle += _angleReset;
            }
        }
        ///<summary>
        ///�^�C�g����ʂ̐ݒ�
        /// </summary>
        //�E
        if (_titleCanvas.GetComponent<Canvas>().enabled == true && _angle == _leverRight && _isStartArrow)
        {
            Invoke("TITLERIGHTARROWCHANGE", _delayArrowTitleTime);
        }
        //�Q�[���X�^�[�g�ɖ�󂪌������
        if (_titleCanvas.GetComponent<Canvas>().enabled == true && _angle == _leverLeft && _isarrowChange)
        {
            Invoke("TITLEUPARROWCHANGE", _delayArrowTitleTime);
        }

        if (_titleCanvas.GetComponent<Canvas>().enabled == true && _angle == _leverRight && _isExit)
        {
            Invoke("TITLEUPARROWCHANGE", _delayArrowTitleTime);
        }

        //��
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
        ///�I�v�V������ʂ̖��
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
    #region ���\�b�h
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

    //�Q�[���X�^�[�gbutton�������ꂽ��
    public void StartGameOnButton()
    {
        SceneManager.LoadScene("");
    }
    //�Q�[���I��button�������ꂽ�痎�����
    public void EndGameOnButton()
    {
        Application.Quit();
    }
    //�I�v�V����button�������ꂽ��I������炵�ăI�v�V������ʊJ����
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
    //�I�v�V������button�������ꂽ��I�v�V������ʂ������
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

    //��ʗh��{�^�����N���b�N�����Ƃ�
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