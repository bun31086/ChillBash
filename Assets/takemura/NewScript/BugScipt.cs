using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugScipt : MonoBehaviour
{
    private Vector3 _playerLeftDefaultPosition = new Vector3(-1.75f,1,0);
    private Vector3 _playerRightDefaultPosition = new Vector3(1.75f,0,0);

    [SerializeField]private GameObject _player = default;
    [SerializeField]private GameObject _playerLeftArea = default;
    [SerializeField] private GameObject _playerRightArea = default;

    private Rigidbody2D _playerRigid = default;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigid = _player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _player.transform.rotation = default;
            _playerRigid.gravityScale = 3;
            _playerLeftArea.transform.localPosition = _playerLeftDefaultPosition;
            _playerRightArea.transform.localPosition = _playerRightDefaultPosition;
        }
    }
}
