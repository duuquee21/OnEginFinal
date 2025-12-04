using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan_UI : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject != null)
        {
            DraggableItem_UI ingrediente = droppedObject.GetComponent<DraggableItem_UI>();

            if (ingrediente != null)
            {
                // 1. Intentar encontrar la tabla a la que pertenecía (si estaba en una)
                PreparationArea_UI tabla = droppedObject.GetComponentInParent<PreparationArea_UI>();
                
                // 2. Si estaba en la tabla, avisar para que lo borre de la lista
                if (tabla != null)
                {
                    tabla.RemoveIngredient(ingrediente);
                }

                Debug.Log("🗑️ Basura eliminada: " + ingrediente.ingredientName);
                
                // 3. Destruir el objeto visual
                Destroy(droppedObject);
            }
        }
    }
}