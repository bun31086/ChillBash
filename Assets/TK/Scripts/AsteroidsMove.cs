using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 覐΂��ړ�������X�N���v�g
/// </summary>

public class AsteroidsMove : MonoBehaviour
{
    //�Q�[���I�u�W�F�N�g�A�ϐ����̊i�[
    [SerializeField] private float _asteroidsMoveSpeed = default;
    [SerializeField] private GameObject _asteroids;
    [SerializeField] private GameObject _asteroids_Halo;
    [SerializeField] private GameObject _asteroids_Trajectory;
    [SerializeField] private GameObject _blast;
    [SerializeField] private GameObject _smoke;

    void Update()
    {
        //"transform.right"�����Ɋi�[�����l��"Time.deltaTime"���������l������
        transform.position += _asteroidsMoveSpeed * Time.deltaTime * transform.right;
    }
}
