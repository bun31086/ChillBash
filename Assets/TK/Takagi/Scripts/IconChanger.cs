using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconChanger : MonoBehaviour
{
    #region �ϐ�

    /**"SerializeField" Inspector���瑀�삪�\,
     * ���̃N���X����̏���������h�~
     * 
     **/
    [SerializeField] private GameObject _icon;

    // LifeManagerScript�p�̕ϐ�



    #endregion


    void Start()
    {
        
    }

    public void UpdateIcon()
    {
        
            _icon.SetActive(true);
      

    }
}
