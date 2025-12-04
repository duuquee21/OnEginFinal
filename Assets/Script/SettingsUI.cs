using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : UIWindow
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider brightnessSlider;


    private void Start()
    {
        volumeSlider.value = 0.5f;
        brightnessSlider.value = 1f;
        
        //eventos
        
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);
        UIManager.Instance.HideUI("SettingsUI");
        
    }

    private void OnVolumeChanged(float value)
    {
        volumeSlider.value = value;
    }

    private void OnBrightnessChanged(float value)
    {
        brightnessSlider.value = value;
    }

    public void OnBack()
    {
        UIManager.Instance.ShowUI("MainMenu");
        UIManager.Instance.HideUI("SettingsUI  ");
    }
}
