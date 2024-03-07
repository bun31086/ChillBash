using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemy : MonoBehaviour
{
    private GameObject playerobj;
    private Vector2 pos;

    private EnemysMove _enemysMove;
    [SerializeField] private GameObject _pauseObj = default;
    private PauseScript _pauseScript = default;

    void Start()
    {
        _enemysMove = GetComponent<EnemysMove>();
        playerobj = GameObject.FindGameObjectWithTag("Player");
        pos = transform.position;

        _pauseScript = _pauseObj.GetComponent<PauseScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_enemysMove.IsDamage && !_pauseScript.IsPause)
        {
            _enemysMove.VerticalEnemyMove();
        }

        if (playerobj.transform.position.x < this.transform.position.x)
        {
            _enemysMove.Right();
        }
        if (playerobj.transform.position.x > this.transform.position.x)
        {
            _enemysMove.Left();
        }

    }
}
