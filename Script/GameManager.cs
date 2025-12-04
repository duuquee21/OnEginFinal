using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic; // Necesario para Listas

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Configuración del Nivel")]
    public int objetivoPintxos = 10; // Cambia esto a 20 o 30 según el nivel
    public float tiempoLimite = 60f; // Dale más tiempo si son más pintxos!
    
    [Header("Menú del Bar")]
    public List<RecipeData> recetasDisponibles; // ¡Arrastra todas tus recetas aquí!

    [Header("Referencias UI")]
    public TMP_Text textoTemporizador;
    public TMP_Text textoMarcador; // Nuevo: Para ver "5 / 10"
    public GameObject panelPerder;
    public GameObject panelGanar;

    // Estado interno
    private float tiempoRestante;
    private int pintxosEntregados = 0;
    private bool juegoEnMarcha = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // ... (Tu código de inicio: tiempo, etc.)

        // --- CÓDIGO DE DEPURACIÓN PARA ENCONTRAR DATOS SUCIOS ---
        Debug.Log("--- CHEQUEANDO MENÚ FINAL ---");
        foreach (var receta in recetasDisponibles)
        {
            if (receta.prefabResultado != null)
            {
                string nombreFinal = receta.prefabResultado.GetComponent<DraggableItem_UI>().ingredientName;
            
                if (string.IsNullOrEmpty(nombreFinal))
                {
                    Debug.LogError("❌ ¡RECETA CORRUPTA ENCONTRADA! El Prefab de la receta '" + receta.name + "' tiene el nombre final vacío. ¡Arréglalo!");
                }
                else
                {
                    Debug.Log("✅ Receta OK: " + receta.name + " -> Prefab Nombre: " + nombreFinal);
                }
            }
        }
        Debug.Log("-----------------------------");
        // -----------------------------------------------------------
        pintxosEntregados = 0;
        tiempoRestante = tiempoLimite;
        juegoEnMarcha = true;
        
        if (panelPerder != null) panelPerder.SetActive(false);
        if (panelGanar != null) panelGanar.SetActive(false);
        
        ActualizarUI();
    }

    void Update()
    {
        if (!juegoEnMarcha) return;

        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarUI();
        }
        else
        {
            PerderNivel();
        }
    }

    // --- Lógica del Juego ---

    public RecipeData ObtenerRecetaAleatoria()
    {
        // SEGURIDAD: Si la lista no existe o está vacía, avisamos y devolvemos null
        if (recetasDisponibles == null || recetasDisponibles.Count == 0)
        {
            Debug.LogError("🚨 ERROR CRÍTICO EN GAMEMANAGER: La lista 'Recetas Disponibles' está vacía. ¡Arrastra las recetas en el Inspector!");
            return null;
        }

        int azar = Random.Range(0, recetasDisponibles.Count);
        return recetasDisponibles[azar];
    }

    public void PintxoEntregadoCorrectamente()
    {
        pintxosEntregados++;
        ActualizarUI();

        if (pintxosEntregados >= objetivoPintxos)
        {
            GanarNivel();
        }
    }

    // --- UI y Estados ---

    void ActualizarUI()
    {
        if (textoTemporizador != null) 
            textoTemporizador.text = "Tiempo: " + Mathf.Ceil(tiempoRestante).ToString("F0");
            
        if (textoMarcador != null)
            textoMarcador.text = "Pintxos: " + pintxosEntregados + " / " + objetivoPintxos;
    }

    public void PerderNivel()
    {
        juegoEnMarcha = false;
        Time.timeScale = 0f;
        if (panelPerder != null) panelPerder.SetActive(true);
    }

    public void GanarNivel()
    {
        juegoEnMarcha = false;
        Time.timeScale = 0f;
        if (panelGanar != null) panelGanar.SetActive(true);
    }
    
    // ... (variables anteriores) ...

    // ESTA ES LA FUNCIÓN NUEVA QUE LLAMARÁN LOS BOTONES
    public void ConfigurarYEmpezar(int objetivo, float tiempo)
    {
        // 1. Configurar dificultad
        objetivoPintxos = objetivo;
        tiempoLimite = tiempo;

        // 2. Resetear variables de juego
        pintxosEntregados = 0;
        tiempoRestante = tiempoLimite;
        juegoEnMarcha = true;
        Time.timeScale = 1f; // Asegurar que el tiempo corre

        // 3. Actualizar UI inicial
        ActualizarUI();
        
        // 4. Asegurarnos de que los paneles de fin de juego están cerrados
        if (panelPerder != null) panelPerder.SetActive(false);
        if (panelGanar != null) panelGanar.SetActive(false);
        
        Debug.Log("Nivel Iniciado: Objetivo " + objetivo + " | Tiempo " + tiempo);
       
        if (AudioManager.instance != null)
        {
            
        }
    }
}