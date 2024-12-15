using UnityEngine;

public class PrefabInstanciate : MonoBehaviour
{
    [Header("Prefab a ser instanciado")]
    public GameObject prefab;

    // Método que instancia o prefab na posição atual do objeto
    public void InstanciarPrefab()
    {
        if (prefab != null)
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("Prefab não atribuído no script InstanciadorPrefab!");
        }
    }
}
