using UnityEngine;
using UnityEngine.UI;

public class IngredientSpawner_UI : MonoBehaviour
{
    // Arrastra el Prefab de UI (con DraggableItem_UI.cs) aquí
    public GameObject ingredientPrefab_UI;
    
    // Arrastra el Canvas principal aquí
    public Transform canvasTransform;

    // Conecta esta función al OnClick() del Botón en el Inspector
    public void SpawnIngredient()
    {
        // Crea el ingrediente como hijo del Canvas
        GameObject newIngredient = Instantiate(ingredientPrefab_UI, canvasTransform);
        
        // Lo posiciona exactamente donde está el ratón
        newIngredient.GetComponent<RectTransform>().position = Input.mousePosition;

        // NOTA: El jugador tendrá que hacer clic y arrastrar el nuevo objeto
        // (Si queremos que se "pegue" al ratón automáticamente, 
        // DraggableItem_UI necesitaría una función "StartDragNow")
    }
}