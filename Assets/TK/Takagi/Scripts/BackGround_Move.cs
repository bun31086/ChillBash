// --------------------------------------------------------- 
// BackGround_Move.cs 
// 
// �쐬��: 6/28
// �쐬��: ���،���
// ---------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// �w�i���ړ�������X�N���v�g
/// </summary>

public class BackGround_Move: MonoBehaviour
{
	#region �ϐ�

	/**"SerializeField" Inspector���瑀�삪�\,
	* ���̃N���X����̏���������h�~
	* 
	**/
	// �Q�[���I�u�W�F�N�g�A�ϐ����̊i�[
	[SerializeField] private GameObject _player;
	[SerializeField] private const float RATE = 0.12f;
	[SerializeField] private Vector3 _startPlayerOffset;
	[SerializeField] private Vector3 _startCameraPos;

	#endregion

	#region ���\�b�h

	private void Start()
	{
		// �v���C���[�̃X�^�[�g�ʒu�̎擾
		_startPlayerOffset = _player.transform.position;
		// �J�����̃X�^�[�g�ʒu�̎擾
		_startCameraPos = this.transform.position;
	}

	private void Update()
	{
		// �v���C���[�ɍ��킹�ē���������
		Vector3 v = (_player.transform.position - _startPlayerOffset) * RATE;
		this.transform.position = _startCameraPos + v;
	}

	#endregion
}

