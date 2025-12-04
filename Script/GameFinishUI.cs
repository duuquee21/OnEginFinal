using UnityEngine;

public class GameFinishUI : UIWindow
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
            
        UIManager.Instance.HideUI("GameFinishUI");
        
    }

    public  void OnSelectLevel1()
    {
        // 1. ¡IMPORTANTÍSIMO! Reactivar el tiempo
        Time.timeScale = 1f; 

        // 2. Ir a la pantalla correcta
        // Asegúrate de que "SelectLevelUI" es el nombre exacto (ID) 
        // que tiene tu Canvas de Selección de Nivel en el UIManager.
        UIManager.Instance.ShowUI("SelectLevelUI"); // <--- CAMBIA ESTO si tu ID es diferente
        
        // 3. Ocultar las pantallas de juego
        UIManager.Instance.HideUI("GameFinishUI"); // Ocultar pantalla de ganar/perder
        UIManager.Instance.HideUI("GameUI");       // Ocultar el juego (barra, clientes, etc.)
    }
    
    public void OnQuit()
    {
        Application.Quit();
    }
}
