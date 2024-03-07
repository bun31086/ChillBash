using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{

    private BossManagement _bossMove1;
    //[SerializeField]
    private GameObject target;

    //private BossMove1 bossMove1;
    //private bool _ground;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(_bossMove1);

        //_ground = bossMove1._isGround;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = GameObject.Find("Boss");
            BossManagement _bossMove1 = target.GetComponent<BossManagement>();
            Debug.Log(_bossMove1);
            _bossMove1.Beginning();  //取得したスクリプトのメソッドの実行
            this.gameObject.SetActive(false);
        }
    }
}
