using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using System.Collections.Generic;

// Implementamos estas interfaces para que el script reciba eventos de Clic, Arrastre y Soltar
public class DraggableItem_UI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public string ingredientName;
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas mainCanvas;
    private Vector3 startPosition; // Posición inicial
    private Transform startParent; // Padre inicial

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            // Añadimos un CanvasGroup si no existe. Es VITAL.
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        
        mainCanvas = GetComponentInParent<Canvas>();
        startPosition = rectTransform.position;
        startParent = transform.parent;
    }

    // Se llama cuando haces clic en el objeto
    public void OnPointerDown(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // Lo hace semi-transparente
        
        // Esto evita que el objeto bloquee los Raycasts
        // para que podamos detectar sobre qué lo soltamos
        canvasGroup.blocksRaycasts = false; 
        
        // Lo saca de su padre para que se dibuje encima de todo
        transform.SetParent(mainCanvas.transform);
    }

    // Se llama en CADA FRAME mientras arrastras
    public void OnDrag(PointerEventData eventData)
    {
        // Esta es la matemática para el 1:1 perfecto con el ratón
        rectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }

    // Se llama cuando sueltas el clic
    public void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // Lo vuelve opaco
        canvasGroup.blocksRaycasts = true; // Vuelve a ser "sólido"

        // Lanza un rayo para ver sobre qué lo hemos soltado
        bool droppedOnValidZone = false;
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
                if (customer.ReceivePintxo(this))
                {
                    droppedOnValidZone = true; // El cliente lo aceptó
                }
                break;
            }
        }

        if (droppedOnValidZone)
        {
            // Si se soltó bien, el script de la zona se encarga de él
            // (lo destruirá o lo adoptará como hijo)
        }
        else
        {
            // Si no, vuelve a su posición original y se destruye
            Destroy(gameObject);
        }
    }

    // Función de utilidad para obtener los resultados del Raycast
    private List<RaycastResult> GetRaycastResults(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results;
    }
}