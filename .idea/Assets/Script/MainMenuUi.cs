using UnityEngine;

public class MainMenuUi : UIWindow
{

   public void Start()
   {
      UIManager.Instance.ShowUI(("MainMenuUI"));
  
   }
   public void OnSelectLevel()
   {
      //ir a seleccion de nivel
      
      UIManager.Instance.ShowUI(("SelectLevelUI"));
      UIManager.Instance.HideUI("MainMenuUI");
   }
   
   public void OnSettings()
   {
      UIManager.Instance.ShowUI("SettingsUI");
      UIManager.Instance.HideUI("MainMenuUI");
   }

   public void OnQuit()
   {
      Application.Quit();
   }
}
