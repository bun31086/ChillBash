using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelEnemy : MonoBehaviour
{
    private EnemysMove _enemysMove;
    private GameObject playerobj;
    private Vector2 pos;

    [SerializeField] private GameObject _pauseObj = default;
    private PauseScript _pauseScript = default;


    // Start is called before the first frame update
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
        if (_enemysMove.IsDamage == false && !_pauseScript.IsPause)
        {
            _enemysMove.HorizontalEnemyMove();
        }
    }
}
