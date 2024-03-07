using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField] private GameObject _beforeCamera = default;
    [SerializeField] private GameObject _afterCamera = default;

    private BoxCollider2D _boxCollider2D = default;

    private bool _changeOne = false;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!_changeOne && collision.gameObject.tag == "Player")
        {
            _beforeCamera.SetActive(false);
            _afterCamera.SetActive(true);
            _boxCollider2D.isTrigger = false;
            _changeOne = true;
            
        }
    }
}
