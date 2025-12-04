using UnityEngine;

public class Customer_UI : MonoBehaviour
{
    public string desiredPintxo = "PintxoFinal_Atun";
    public GameObject orderBubble; 
    public GameObject clientVisualObject; // La imagen del cliente (si está separada)

    public bool ReceivePintxo(DraggableItem_UI pintxo)
    {
        if (pintxo.ingredientName == desiredPintxo)
        {
            // ¡¡CAMBIO IMPORTANTE!!
            // Ya no destruimos al cliente. Notificamos al GameManager.
            
            // Llama al GameManager para ganar el nivel
            GameManager.instance.GanarNivel();

            // Oculta el bocadillo
            if (orderBubble != null) orderBubble.SetActive(false);
            
            // Oculta la imagen del cliente (opcional, en lugar de destruirlo)
            if (clientVisualObject != null)
            {
                clientVisualObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false); // Oculta este objeto
            }
            
            // Destruye el pintxo
            Destroy(pintxo.gameObject);
            
            return true; 
        }
        else
        {
            Debug.Log("¡Este no es mi pintxo!");
            return false; 
        }
    }
}