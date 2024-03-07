using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroy : MonoBehaviour
{
    private int _clearCount = 15;

    [SerializeField]private GameObject _bossManeOBJ = default;
    private BossManagement _bossManagement;

    private GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        _bossManagement = _bossManeOBJ.GetComponent<BossManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = _target.transform.position;
        if (_bossManagement.IsClear)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Return))
            {
                _clearCount -= 1;
            }

            if (_clearCount == 0)
            {
                SceneManager.LoadScene("ClearScene");
            }
        }
    }
}
