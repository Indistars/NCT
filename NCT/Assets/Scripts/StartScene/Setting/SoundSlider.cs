using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;
    [SerializeField] string parameterName = "";

    private void Start()
    {
        volumeSlider = gameObject.GetComponent<Slider>();
    }

    public void OnValueChanged()
    {
        audioMixer.SetFloat(parameterName,
        (volumeSlider.value <= volumeSlider.minValue) ? -80f : volumeSlider.value);
    }
}

