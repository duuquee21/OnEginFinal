using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Componentes")]
    public AudioSource musicSource;

    [Header("Clip de Música de Fondo")]
    public AudioClip musicaFondo; // Una sola pista para todo (menú y juego)

    void Awake()
    {
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
        // Si el AudioSource existe y tiene un clip asignado...
        if (musicSource != null && musicaFondo != null)
        {
            // 1. Asignar el clip
            musicSource.clip = musicaFondo;
            
            // 2. Asegurar que se repite y que empieza
            musicSource.loop = true;
            
            // Solo empezar si no está ya sonando
            if (!musicSource.isPlaying) 
            {
                musicSource.Play();
            }
        }
    }
    
    // Dejamos esta función por si quieres un botón de Mute, aunque ya no se usa para cambiar pistas
    public void PararMusica()
    {
        musicSource.Stop();
    }
}