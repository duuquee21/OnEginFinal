using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections; // Necesario para la coroutine del Start Drag

public class DraggableItem_UI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // --- IDENTIDAD (NUEVO) ---
    // Este campo se llena en el pintxo final para que el cliente sepa qué es.
    [HideInInspector] public RecipeData sourceRecipe; 

    // Mantenemos este campo para la comprobación en la Tabla de Cortar
    public string ingredientName; 
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas mainCanvas;
    private Vector3 startPosition;
    private Transform startParent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        
        mainCanvas = GetComponentInParent<Canvas>();
        startPosition = rectTransform.position;
        startParent = transform.parent;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; 
        canvasGroup.blocksRaycasts = false; 
        
        // Pone el objeto al frente en el Canvas
        transform.SetParent(mainCanvas.transform); 

        // Esto permite que el objeto sea arrastrado inmediatamente después de ser instanciado
        StartCoroutine(SimulateDragStart(eventData.position));
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Mueve la posición del ratón de forma fluida y 1:1
        rectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; 
        canvasGroup.blocksRaycasts = true;

        bool droppedOnValidZone = false;
        
        // Raycast para ver qué hay debajo del cursor
        foreach (RaycastResult result in GetRaycastResults(eventData))
        {
            // ¿Es la tabla de cortar?
            PreparationArea_UI prepArea = result.gameObject.GetComponent<PreparationArea_UI>();
            if (prepArea != null)
            {
                prepArea.AddIngredient(this);
                droppedOnValidZone = true;
                break;
            }

            // ¿Es el cliente?
            Customer_UI customer = result.gameObject.GetComponent<Customer_UI>();
            if (customer != null)
            {
                // El cliente verifica si el sourceRecipe es correcto
                if (customer.ReceivePintxo(this))
                {
                    droppedOnValidZone = true; 
                }
                break;
            }
            
            TrashCan_UI basura = result.gameObject.GetComponent<TrashCan_UI>();
            if (basura != null)
            {
                // La basura usa la interfaz IDropHandler, así que podemos llamarla manualmente
                // O dejar que el sistema de eventos lo haga. 
                // Como estamos en OnPointerUp manual, llamamos a su lógica:
                basura.OnDrop(eventData);
                droppedOnValidZone = true;
                break;
            }
        }

        if (!droppedOnValidZone)
        {
            // Vuelve a su padre inicial y a su posición (o destrúyelo si vino del spawner)
            Destroy(gameObject);
        }
    }

    // --- Funciones de Utilidad ---

    private List<RaycastResult> GetRaycastResults(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results;
    }

    // Coroutine para manejar el arrastre suave después de instanciar (similar al StartDragCor anterior)
    private IEnumerator SimulateDragStart(Vector2 initialPosition)
    {
        // Espera un frame para que Unity registre el objeto
        yield return null; 
        
        // Simular que el objeto está en el cursor al instante de la creación
        // Esto previene el problema del doble clic o del arrastre lento
        rectTransform.position = initialPosition;
    }
}