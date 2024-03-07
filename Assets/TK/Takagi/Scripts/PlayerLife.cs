// --------------------------------------------------------- 
// PlayerLife.cs 
// 
// �쐬��: 6/28
// �쐬��: ���،���
// ---------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    #region �ϐ�

    /**"SerializeField" Inspector���瑀�삪�\,
     * ���̃N���X����̏���������h�~
     * 
     **/
    [SerializeField] private GameObject _lifePanel;

    // LifeManagerScript�p�̕ϐ�
    private LifeManager _lifeManager;

    #endregion

    #region ���\�b�h

    void Start()
    {
        // _lifePanel�̒��ɂ���LifeManagerScript���擾���ĕϐ��Ɋi�[����
        _lifeManager = _lifePanel.GetComponent<LifeManager>();
    }

    public void LiftManager()
    {            
        // LifeManagerScript��UpdateLife()�����s
        _lifeManager.UpdateLife();
    }
    #endregion
}
