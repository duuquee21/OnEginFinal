using UnityEngine;
using UnityEngine.UI;

public class IngredientSpawner_UI : MonoBehaviour
{
    
    public GameObject ingredientPrefab_UI;
    
    
    public Transform canvasTransform;

    
    public void SpawnIngredient()
    {
        // Crea el ingrediente como hijo del Canvas
        GameObject newIngredient = Instantiate(ingredientPrefab_UI, canvasTransform);
        
        // Lo posiciona exactamente donde está el ratón
        newIngredient.GetComponent<RectTransform>().position = Input.mousePosition;

       
    }
}