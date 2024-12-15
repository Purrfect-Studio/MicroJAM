using UnityEngine;

public class VasoDrop : MonoBehaviour
{
    [Header("Arraste o SistemaDeDrop aqui")]
    public SistemaDeDrop sistemaDeDrop; // Agora você pode arrastar no Inspector

    void OnDestroy()
    {
        if (sistemaDeDrop != null)
        {
            print("Entrei no on destroy");
            sistemaDeDrop.Dropar();
        }
        else
        {
            Debug.LogWarning("SistemaDeDrop não foi atribuído ao objeto: " + gameObject.name);
        }
    }
}
