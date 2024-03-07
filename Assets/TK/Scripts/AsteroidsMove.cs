using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 隕石を移動させるスクリプト
/// </summary>

public class AsteroidsMove : MonoBehaviour
{
    //ゲームオブジェクト、変数等の格納
    [SerializeField] private float _asteroidsMoveSpeed = default;
    [SerializeField] private GameObject _asteroids;
    [SerializeField] private GameObject _asteroids_Halo;
    [SerializeField] private GameObject _asteroids_Trajectory;
    [SerializeField] private GameObject _blast;
    [SerializeField] private GameObject _smoke;

    void Update()
    {
        //"transform.right"方向に格納した値と"Time.deltaTime"をかけた値を入れる
        transform.position += _asteroidsMoveSpeed * Time.deltaTime * transform.right;
    }
}
