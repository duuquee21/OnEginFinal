using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PreparationArea_UI : MonoBehaviour
{
    // ¡¡MUY IMPORTANTE!! 
    // Ve al componente "Image" de esta tabla en el Inspector
    // y ASEGÚRATE de que la casilla "Raycast Target" esté MARCADA.
    
    private List<DraggableItem_UI> ingredientsOnBoard = new List<DraggableItem_UI>();

    // Define la receta
    public List<string> pintxoAtunCebolla = new List<string> { "Pan", "Atun", "Cebolla" };
    
    // Arrastra el Prefab_PintxoFinal_UI aquí
    public GameObject pintxoCompletoPrefab_UI;
    public Transform canvasTransform; // Arrastra el Canvas principal

    public void AddIngredient(DraggableItem_UI ingredient)
    {
        ingredientsOnBoard.Add(ingredient);
        
        // "Pega" el ingrediente a esta tabla
        ingredient.transform.SetParent(this.transform);
        ingredient.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Lo centra

        CheckForRecipe();
    }

    private void CheckForRecipe()
    {
        List<string> currentNames = ingredientsOnBoard.Select(item => item.ingredientName).ToList();
        bool recipeMatch = currentNames.Count == pintxoAtunCebolla.Count && 
                           currentNames.All(pintxoAtunCebolla.Contains);

        if (recipeMatch)
        {
            Debug.Log("¡RECETA UI COMPLETADA!");
            foreach (DraggableItem_UI item in ingredientsOnBoard)
            {
                Destroy(item.gameObject);
            }
            ingredientsOnBoard.Clear();

            // Crea el pintxo final
            GameObject pintxoFinal = Instantiate(pintxoCompletoPrefab_UI, canvasTransform);
            // Colócalo donde estaba la tabla
            pintxoFinal.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
        }
    }
}