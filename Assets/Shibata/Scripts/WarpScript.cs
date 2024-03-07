using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpScript : MonoBehaviour
{
    private Vector3 _warpPoint = new Vector3(840,-39,0);

    [SerializeField]private GameObject _player = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _player.transform.position = _warpPoint;
        }
    }
}
