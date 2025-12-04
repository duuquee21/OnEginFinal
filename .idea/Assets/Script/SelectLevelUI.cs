using UnityEngine;

    public class SelectLevelUI : UIWindow
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Instance.HideUI("SelectLevelUI");    }

    public void OnLeve1()
    {
        UIManager.Instance.ShowUI("GameUI");
        UIManager.Instance.HideUI("SelectLevelUI");
    }
    public void OnLeve2()
    {
        UIManager.Instance.ShowUI("GameUI");
        UIManager.Instance.HideUI("SelectLevelUI");
    }
    public void OnLeve3()
    {
        UIManager.Instance.ShowUI("GameUI");
        UIManager.Instance.HideUI("SelectLevelUI");
    }
    public void OnLeve4()
    {
        UIManager.Instance.ShowUI("GameUI");
        UIManager.Instance.HideUI("SelectLevelUI");
    }
    public void OnLeve5()
    {
        UIManager.Instance.ShowUI("GameUI");
        UIManager.Instance.HideUI("SelectLevelUI");
    }
    
    public void OnBack()
    {
        UIManager.Instance.ShowUI("MainMenuUI");
        UIManager.Instance.HideUI("SelectLevelUI");
    }
}
