using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : MonoBehaviour
{
    /// <summary>
    /// Float,intˆê——
    /// </summary>
    [SerializeField] private float _leftTriggerOn = default;
    [SerializeField] private float _rightTriggerOn = default;

    /// <summary>
    /// GameObjectˆê——
    /// </summary>
    private GameObject _player = default;
    [SerializeField] private GameObject _bashUnder = default;

    /// <summary>
    /// Componentˆê——
    /// </summary>
    private Rigidbody2D _playerRigid = default;
    private BoxCollider2D _playerBox = default;
    private Attack _attackScript = default;
    private Animator _animator = default;                                     //Animator‚ðŽæ“¾
    private KnockBack _knockBack = default;

    /// <summary>
    /// Boolˆê——
    /// </summary>
    [SerializeField] private bool _isBash = default;

    /// <summary>
    /// Propertyˆê——
    /// </summary>
    public bool IsBash { get => _isBash; set => _isBash = value; }






    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRigid = _player.GetComponent<Rigidbody2D>();
        _playerBox = _player.GetComponent<BoxCollider2D>();
        _attackScript = _player.GetComponent<Attack>();
        _animator = _player.GetComponent<Animator>();                                            //Animator‚ðŽæ“¾
        _knockBack = _player.GetComponent<KnockBack>();
    }

    // Update is called once per frame
    void Update()
    {
        _leftTriggerOn = Input.GetAxisRaw("LeftTrigger");
        _rightTriggerOn = Input.GetAxisRaw("RightTrigger");



        if (_attackScript.EnemyCombo > 0 && ((_leftTriggerOn == 1 && _rightTriggerOn == 1) || Input.GetKeyDown(KeyCode.S)) && !_isBash && !_knockBack.IsKnockBack)
        {
            _isBash = true;
            _player.transform.rotation = default;
            _playerRigid.velocity = Vector2.zero;
            _playerRigid.gravityScale = default;
            _animator.SetBool("isBash", true);
            _playerRigid.AddForce(new Vector2(default, -1500));
            _bashUnder.SetActive(true);
        }
    }
}
