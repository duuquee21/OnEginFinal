using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; // Necesario para Listas

public class Customer_UI : MonoBehaviour
{
    private RecipeData desiredRecipe; 
    
    [Header("Referencias Visuales")]
    public Image imagenBocadillo; 
    public GameObject orderBubbleObject;
    
    // --- NUEVO: Referencia a la imagen del personaje ---
    public Image imagenPersonaje; // Arrastra el componente Image del Cliente aquí
    
    // --- NUEVO: Lista de caras/personajes distintos ---
    public List<Sprite> posiblesPersonajes; // Arrastra tus sprites de clientes aquí

    private GameManager manager;

    void Start()
    {
        manager = GameManager.instance;
        
        // CAMBIO: Apagamos solo la imagen (dibujo), no el objeto entero
        if(imagenPersonaje != null) imagenPersonaje.enabled = true; 
        
        if(orderBubbleObject != null) orderBubbleObject.SetActive(true);

        StartCoroutine(AparecerNuevoCliente());
    }

    public void GenerarNuevoPedido()
    {
        // 1. Lógica de "No Repetir Receta"
        RecipeData recetaAnterior = desiredRecipe;

        if (manager.recetasDisponibles.Count > 1)
        {
            do
            {
                desiredRecipe = manager.ObtenerRecetaAleatoria();
            }
            while (desiredRecipe == recetaAnterior);
        }
        else
        {
            desiredRecipe = manager.ObtenerRecetaAleatoria();
        }

        // 2. Configurar el pedido visual
        if (desiredRecipe != null)
        {
            Debug.Log("NUEVO CLIENTE QUIERE: " + desiredRecipe.nombreDelPlato);
            
            if (imagenBocadillo != null && desiredRecipe.iconoDelPlato != null)
            {
                imagenBocadillo.sprite = desiredRecipe.iconoDelPlato;
                imagenBocadillo.preserveAspect = true; 
            }
            
            // 3. --- NUEVO: Cambiar la cara del cliente al azar ---
            if (posiblesPersonajes.Count > 0 && imagenPersonaje != null)
            {
                int azar = Random.Range(0, posiblesPersonajes.Count);
                imagenPersonaje.sprite = posiblesPersonajes[azar];
                imagenPersonaje.preserveAspect = true;
            }
        }
    }

    public bool ReceivePintxo(DraggableItem_UI pintxo)
    {
        if (pintxo.sourceRecipe == desiredRecipe)
        {
            Debug.Log("✅ ¡Pintxo correcto!");
            
            manager.PintxoEntregadoCorrectamente();
            Destroy(pintxo.gameObject);

            // Inicia la secuencia de "irse y que venga otro"
            StartCoroutine(SiguienteClienteRutina());

            return true; 
        }
        else
        {
            Debug.Log("❌ ERROR: El cliente quería " + desiredRecipe.nombreDelPlato);
            return false; 
        }
    }

    // --- RUTINA DE SALIDA Y ENTRADA ---
    System.Collections.IEnumerator SiguienteClienteRutina()
    {
        if (orderBubbleObject != null) orderBubbleObject.SetActive(false);
        
        // CAMBIO: Apagamos solo la imagen
        if (imagenPersonaje != null) imagenPersonaje.enabled = false;
        
        yield return new WaitForSeconds(1.0f); 

        StartCoroutine(AparecerNuevoCliente());
    }

    System.Collections.IEnumerator AparecerNuevoCliente()
    {
        // Preparamos los datos del nuevo
        GenerarNuevoPedido();

        yield return new WaitForSeconds(0.2f);

        // CAMBIO: Encendemos la imagen
        if (imagenPersonaje != null) imagenPersonaje.enabled = true;
        
        yield return new WaitForSeconds(0.3f);
        if (orderBubbleObject != null) orderBubbleObject.SetActive(true);
    }
}