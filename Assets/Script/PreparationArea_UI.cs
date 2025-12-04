using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq; 

public class PreparationArea_UI : MonoBehaviour
{
    public List<RecipeData> libroDeRecetas; 
    public Transform canvasTransform; 

    // La lista de ingredientes sobre la tabla
    private List<DraggableItem_UI> ingredientsOnBoard = new List<DraggableItem_UI>();

    public void AddIngredient(DraggableItem_UI ingredient)
    {
        // 1. LIMPIEZA DE FANTASMAS (Paso Vital)
        // Antes de añadir nada, borramos de la memoria cualquier objeto que haya sido destruido
        ingredientsOnBoard.RemoveAll(item => item == null);

        // 2. Evitar duplicados
        if(ingredientsOnBoard.Contains(ingredient)) return;
        
        ingredientsOnBoard.Add(ingredient);
        
        // 3. Pegar visualmente
        ingredient.transform.SetParent(this.transform);
        ingredient.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; 

        CheckForRecipe();
    }

    // Función para sacar cosas de la lista (usada por la Papelera)
    public void RemoveIngredient(DraggableItem_UI ingredient)
    {
        if (ingredientsOnBoard.Contains(ingredient))
        {
            ingredientsOnBoard.Remove(ingredient);
        }
        // Limpiamos fantasmas por si acaso
        ingredientsOnBoard.RemoveAll(item => item == null);
    }

    private void CheckForRecipe()
    {
        // LIMPIEZA EXTRA DE SEGURIDAD
        ingredientsOnBoard.RemoveAll(item => item == null);

        // Si la tabla está vacía, no hacemos nada
        if (ingredientsOnBoard.Count == 0) return;

        List<string> currentIngredientNames = ingredientsOnBoard.Select(item => item.ingredientName).ToList();

        foreach (RecipeData receta in libroDeRecetas)
        {
            if (receta == null) continue;

            bool countMatch = currentIngredientNames.Count == receta.ingredientesNecesarios.Count;
            
            // Usamos un método seguro para comparar listas
            bool contentMatch = !currentIngredientNames.Except(receta.ingredientesNecesarios).Any() && 
                                !receta.ingredientesNecesarios.Except(currentIngredientNames).Any();

            if (countMatch && contentMatch)
            {
                Debug.Log("✅ ¡ÉXITO! Cocinando: " + receta.nombreDelPlato);
                CocinarPlato(receta);
                return; 
            }
        }
    }

    private void CocinarPlato(RecipeData receta) 
    {
        // 1. Destruir los ingredientes sueltos (Materia Prima)
        foreach (DraggableItem_UI item in ingredientsOnBoard)
        {
            if (item != null) Destroy(item.gameObject);
        }
        
        // 2. ¡VITAL! Vaciar la lista para que la tabla quede limpia para el siguiente plato
        ingredientsOnBoard.Clear();

        // 3. Instanciar el resultado final
        if (receta.prefabResultado != null)
        {
            GameObject platoFinal = Instantiate(receta.prefabResultado, canvasTransform);
            
            // Lo ponemos donde está la tabla
            platoFinal.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
            
            // Inyectar Identidad
            DraggableItem_UI pintxoScript = platoFinal.GetComponent<DraggableItem_UI>();
            if (pintxoScript != null)
            {
                pintxoScript.sourceRecipe = receta; 
                // Opcional: Permitir que el pintxo final también se añada a la tabla si quisieras combos complejos
                // Pero por ahora, el pintxo final NO se añade a la lista ingredientsOnBoard
            }
        }
    }
}