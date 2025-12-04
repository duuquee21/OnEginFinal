using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    [Header("Referencias a tus Canvas")]
    public GameObject canvasGame;
    public GameObject canvasWin;
    public GameObject canvasLose;
    public GameObject canvasSelectLevel;
    // Añade aquí el MainMenu si lo necesitas

    public void IrASeleccionDeNivel()
    {
        // 1. IMPORTANTE: Descongelar el tiempo
        Time.timeScale = 1f;

        // 2. Apagar lo que no queremos ver
        if (canvasGame != null) canvasGame.SetActive(false);
        if (canvasWin != null) canvasWin.SetActive(false);
        if (canvasLose != null) canvasLose.SetActive(false);

        // 3. Encender lo que SÍ queremos ver
        if (canvasSelectLevel != null) canvasSelectLevel.SetActive(true);
        
        Debug.Log("Navegando a Selección de Nivel...");
    }
    
    public void IrAlJuego()
    {
        Time.timeScale = 1f; // Asegurar tiempo normal al empezar
        
        if (canvasSelectLevel != null) canvasSelectLevel.SetActive(false);
        if (canvasGame != null) canvasGame.SetActive(true);
        
        // Opcional: Reiniciar el GameManager aquí si es necesario
        // GameManager.instance.IniciarNivel();
    }
}