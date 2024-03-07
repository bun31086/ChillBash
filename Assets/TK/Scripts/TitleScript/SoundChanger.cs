using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundChanger : MonoBehaviour
{
    public static float vol = 0.5f;
    [SerializeField] Slider _soundSlider;
    [SerializeField] GameObject _soundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        _soundSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        vol = _soundSlider.value;
        _soundPlayer.GetComponent<AudioSource>().volume = vol;
    }
    public static float Sounder()
    {
        return vol;
    }
}
