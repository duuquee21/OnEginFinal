using UnityEngine;
using NaughtyAttributes;

public class ResponsiveElement : MonoBehaviour

{
    [Header("Settings")]
    [SerializeField] private RectTransform rectTransform;
    
    [Header("Movile Anchors")]
    [SerializeField] private Vector2 movileAnchorMin = new Vector2(0,0);
    [SerializeField] private Vector2 movileAnchorMax = new Vector2(0, 0);
    
    [Header("Tablet Anchors")]
    [SerializeField] private Vector2 tabletAnchorMin = new Vector2(0, 0);
    [SerializeField] private Vector2 tabletAnchorMax = new Vector2(0, 0);
    
    ResponsiveManager _responsiveManager;

    void Start()
    {
        _responsiveManager = ResponsiveManager.Instance;
        _responsiveManager.OnScreenSizeChanged.AddListener(UpdateAnchors);
        UpdateAnchors();
    }

    public void UpdateAnchors()
    {
        if (_responsiveManager == null) return;

        if (_responsiveManager.CurrentDeviceType == DeviceType.Mobile)
        {
            SetMobileAnchors();
        }
        else if (_responsiveManager.CurrentDeviceType == DeviceType.Tablet)
        {
            SetTablebAnchors();
        }
        
    }

    private void SetMobileAnchors()
    {
        rectTransform.anchorMin = movileAnchorMin;
        rectTransform.anchorMax = movileAnchorMax;
    }

    private void SetTablebAnchors()
    {
        rectTransform.anchorMin = tabletAnchorMin;
        rectTransform.anchorMax = tabletAnchorMax;
    }
    
    [Button]
    private  void SaveMobileAnchors()
    {
        Vector2 maxAnchors = rectTransform.anchorMax;
        Vector2 minAnchors = rectTransform.anchorMin;
        
        movileAnchorMax = maxAnchors;
        movileAnchorMin = minAnchors;
    }

    [Button]
    private void SaveTabletAnchors()
    {
        Vector2 maxAnchors = rectTransform.anchorMax;
        Vector2 minAnchors = rectTransform.anchorMin;
        
        tabletAnchorMax = maxAnchors;
        tabletAnchorMin = minAnchors;
    }
}