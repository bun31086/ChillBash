using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconChanger : MonoBehaviour
{
    #region 変数

    /**"SerializeField" Inspectorから操作が可能,
     * 他のクラスからの書き換えを防止
     * 
     **/
    [SerializeField] private GameObject _icon;

    // LifeManagerScript用の変数



    #endregion


    void Start()
    {
        
    }

    public void UpdateIcon()
    {
        
            _icon.SetActive(true);
      

    }
}
