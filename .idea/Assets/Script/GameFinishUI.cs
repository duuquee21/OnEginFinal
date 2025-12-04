using UnityEngine;

public class GameFinishUI : UIWindow
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
            
        UIManager.Instance.HideUI("GameFinishUI");
        
    }

    public void OnSelectLevel1()
    {
        //ir a seleccion de nivel
      
        UIManager.Instance.ShowUI("MainMenuUI");
        UIManager.Instance.HideUI("GameFinishUI");
        UIManager.Instance.HideUI("GameUI");
        
    }
    
    public void OnQuit()
    {
        Application.Quit();
    }
}
