using UnityEngine;
using UnityEngine.UI; // ¡¡Necesario para el texto del temporizador!!
using TMPro;

public class GameManager : MonoBehaviour
{
    // --- Singleton (para que el Cliente pueda encontrarlo) ---
    public static GameManager instance;

    // --- Variables del Temporizador ---
    public float tiempoLimite = 30f; // 30 segundos
    private float tiempoRestante;
    private bool juegoEnMarcha = false;

    // --- Conexiones de UI (Arrastra en el Inspector) ---
    public TMP_Text textoTemporizador;
    public GameObject panelPerder; // El panel que aparece si pierdes
    public GameObject panelGanar; // El panel que aparece si ganas (opcional)

    void Awake()
    {
        // Configuración del Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Asegúrate de que los paneles están ocultos al empezar
        if (panelPerder != null) panelPerder.SetActive(false);
        if (panelGanar != null) panelGanar.SetActive(false);

        // Inicia el juego
        IniciarNivel();
    }

    public void IniciarNivel()
    {
        tiempoRestante = tiempoLimite;
        juegoEnMarcha = true;
        Time.timeScale = 1f; // Asegura que el juego no esté pausado
    }

    void Update()
    {
        // Si el juego no está en marcha (ej. perdido), no hacer nada
        if (!juegoEnMarcha) return;

        if (tiempoRestante > 0)
        {
            // Resta el tiempo
            tiempoRestante -= Time.deltaTime;
            
            // Actualiza el texto en pantalla
            ActualizarTimerUI(tiempoRestante);
        }
        else
        {
            // ¡Se acabó el tiempo!
            tiempoRestante = 0;
            ActualizarTimerUI(tiempoRestante);
            PerderNivel();
        }
    }

    void ActualizarTimerUI(float tiempo)
    {
        if (textoTemporizador == null) return;
        
        // Formatea los segundos a un formato 00
        textoTemporizador.text = "Tiempo: " + Mathf.Ceil(tiempo).ToString("F0");
    }

    // --- Funciones de Estado del Juego ---

    public void PerderNivel()
    {
        juegoEnMarcha = false;
        Time.timeScale = 0f; // Pausa el juego
        Debug.Log("¡HAS PERDIDO! Se acabó el tiempo.");
        
        if (panelPerder != null)
        {
            panelPerder.SetActive(true);
        }
    }

    public void GanarNivel()
    {
        juegoEnMarcha = false;
        Time.timeScale = 0f; // Pausa el juego
        Debug.Log("¡HAS GANADO! Pintxo entregado.");
        
        if (panelGanar != null)
        {
            panelGanar.SetActive(true);
        }
    }
}