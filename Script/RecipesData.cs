using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NuevaReceta", menuName = "Sistema Cocina/Receta")]
public class RecipeData : ScriptableObject
{
    public string nombreDelPlato; 
    public List<string> ingredientesNecesarios; 
    public string nombreDelPintxoFinal;
    public GameObject prefabResultado;
    
    
    public Sprite iconoDelPlato; 
}