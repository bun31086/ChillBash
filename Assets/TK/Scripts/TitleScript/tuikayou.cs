using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tuikayou : MonoBehaviour
{
    [SerializeField]private GameObject _timer = default;
    private Count_Timer _countTimer = default;

    // Start is called before the first frame update
    void Start()
    {
        _countTimer = _timer.GetComponent<Count_Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        //���Z�b�g�p
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            SceneManager.LoadScene("Test");
        }

        //0�b�ŃQ�[���I�[�o�[
        if (_countTimer.TimerEnd)
        {
            SceneManager.LoadScene("OverScene");
        }
    }
}
