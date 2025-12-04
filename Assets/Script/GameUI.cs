using UnityEngine;

public class GameUI : UIWindow
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Instance.HideUI("GameUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
