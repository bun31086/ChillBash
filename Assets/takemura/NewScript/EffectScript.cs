using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    private GameObject _player = default;
    [SerializeField]private GameObject _bashArea = default;
    [SerializeField] private GameObject _killEffect = default;
    [SerializeField] private GameObject _bashEffect = default;
    [SerializeField] private GameObject _auraEffect = default;

    private Attack _attack = default;

    private ParticleSystem.MainModule _auraColor;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _attack = _player.GetComponent<Attack>();
        _auraColor = _auraEffect.GetComponent<ParticleSystem>().main;              //オーラのParticleSystemのmainを取得
    }
    private void Update()
    {
        PlayerAuraChange();
    }

    public void Explosion()
    {
        Instantiate(_killEffect);
    }

    public void Bash()
    {
        _bashEffect.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y - 1.7f);
        Instantiate(_bashEffect);
    }
    /// <summary>
    /// オーラを変更する処理
    /// </summary>
    private void PlayerAuraChange()
    {
        if (_attack.EnemyCombo == 0)
        {
            _auraColor.startColor = Color.black;
            _bashArea.transform.localScale = new Vector3(0, 0, 0);
        }

        else if (_attack.EnemyCombo >= 1 && _attack.EnemyCombo < 3)
        {
            _auraColor.startColor = Color.green;
            _bashArea.transform.localPosition = new Vector3(0, 0.07f, 0);
            _bashArea.transform.localScale = new Vector3(1f, 0.76f, 1);
            _bashEffect.transform.localScale = new Vector3(0.1f, 0.1f, 0.2f);
        }
        else if (_attack.EnemyCombo >= 3 && _attack.EnemyCombo < 5)
        {
            _auraColor.startColor = Color.red;
            _bashArea.transform.localPosition = new Vector3(0, 0.34f, 0);
            _bashArea.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            //_bashEffect.transform.localScale = new Vector3(0.15f, 0.15f, 0.25f);
            _bashEffect.transform.localScale = new Vector3(0.2f, 0.2f, 0.3f);
        }
        else if (_attack.EnemyCombo >= 5 && _attack.EnemyCombo < 7)
        {
            _auraColor.startColor = Color.cyan;
            _bashArea.transform.localPosition = new Vector3(0, 0.58f, 0);
            _bashArea.transform.localScale = new Vector3(2f, 2f, 1);
            _bashEffect.transform.localScale = new Vector3(0.2f, 0.2f, 0.3f);
        }
        else if (_attack.EnemyCombo >= 7)
        {
            _auraColor.startColor = Color.yellow;
            _bashArea.transform.localPosition = new Vector3(0, 1f, 0);
            _bashArea.transform.localScale = new Vector3(3f, 2.5f, 1);
            _bashEffect.transform.localScale = new Vector3(0.25f, 0.25f, 0.35f);
        }
    }
}
