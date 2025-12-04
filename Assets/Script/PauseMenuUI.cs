using UnityEngine;

public class PauseMenuUI : UIWindow
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Instance.HideUI("PauseMenuUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
