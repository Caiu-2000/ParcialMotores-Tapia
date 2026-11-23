using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


[RequireComponent (typeof(Slider))]
public class SoundSettingSlider : MonoBehaviour
{

    Slider slider
    {
        get { return GetComponent<Slider>(); }
    }

    public string mixerGroup;
    [SerializeField] private AudioMixerGroup _AudioMixerG;
    private SoundManager SoundM;
    bool WasSFX = false;
    private void Start()
    {
        if(!SoundManager.instance) { print("Te olvidaste el manager"); }
        else
        {
            if (mixerGroup == "SFX") WasSFX = true;

            SoundM = SoundManager.instance;
            mixerGroup = _AudioMixerG.name;
            if (!SoundM.mixerValue.ContainsKey(mixerGroup))
            {
                SoundM.mixerValue[mixerGroup] = slider.value;
            }
            SetVolumeValues();
        }
    }
    public void SetVolume(float vol)
    {
        print(_AudioMixerG.audioMixer.outputAudioMixerGroup + "   " + mixerGroup);


        if (WasSFX) _AudioMixerG.audioMixer.SetFloat("SFX", Mathf.Log(vol) * 20f);

        else _AudioMixerG.audioMixer.SetFloat(mixerGroup, Mathf.Log(vol) * 20f);
    }

    public void SetVolumeValues()
    {
        float value = Mathf.Max(slider.value, 0.0001f);

        SoundM.mixerValue[mixerGroup] = value;
        SetVolume(value);
    }
    public void LoadVolumeValues()
    {
        slider.value = SoundM.mixerValue[mixerGroup];
        _AudioMixerG.audioMixer.SetFloat(mixerGroup , Mathf.Log(slider.value) * 20f);
    }

    private void OnDisable()
    {
        SetVolumeValues();
    }

}
