using UnityEngine;

public class SelectLevelUI : UIWindow
{
    void Start()
    {
        UIManager.Instance.HideUI("SelectLevelUI");    
    }

    // --- NIVEL 1 ---
    public void OnLeve1()
    {
        // 1. Configurar Dificultad: 10 Pintxos, 60 Segundos
        if (GameManager.instance != null) 
            GameManager.instance.ConfigurarYEmpezar(10, 60f);

        // 2. Navegación UI
        UIManager.Instance.ShowUI("GameUI");
        UIManager.Instance.HideUI("SelectLevelUI");
    }

    // --- NIVEL 2 ---
    public void OnLeve2()
    {
        // 1. Configurar Dificultad: 20 Pintxos, 90 Segundos
        if (GameManager.instance != null) 
            GameManager.instance.ConfigurarYEmpezar(20, 90f);

        UIManager.Instance.ShowUI("GameUI");
        UIManager.Instance.HideUI("SelectLevelUI");
    }

    // --- NIVEL 3 ---
    public void OnLeve3()
    {
        // 1. Configurar Dificultad: 30 Pintxos, 120 Segundos
        if (GameManager.instance != null) 
            GameManager.instance.ConfigurarYEmpezar(30, 120f);

        UIManager.Instance.ShowUI("GameUI");
        UIManager.Instance.HideUI("SelectLevelUI");
    }

    // --- NIVEL 4 (Ejemplo: Más difícil) ---
    public void OnLeve4()
    {
        if (GameManager.instance != null) 
            GameManager.instance.ConfigurarYEmpezar(40, 150f);

        UIManager.Instance.ShowUI("GameUI");
        UIManager.Instance.HideUI("SelectLevelUI");
    }

    // --- NIVEL 5 (Ejemplo: Experto) ---
    public void OnLeve5()
    {
        if (GameManager.instance != null) 
            GameManager.instance.ConfigurarYEmpezar(50, 180f);

        UIManager.Instance.ShowUI("GameUI");
        UIManager.Instance.HideUI("SelectLevelUI");
    }
    
    public void OnBack()
    {
        UIManager.Instance.ShowUI("MainMenuUI");
        UIManager.Instance.HideUI("SelectLevelUI");
    }
}