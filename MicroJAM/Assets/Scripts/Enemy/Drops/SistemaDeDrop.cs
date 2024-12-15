using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SistemaDeDrop : MonoBehaviour
{
    public List<GameObject> Drops = new List<GameObject>();

    private int probabilidade;
    public void Dropar()
    {
        print("dropei");
        GameObject drop = Instantiate(Drops[escolherDrop()], gameObject.transform.position, Quaternion.identity);
        Destroy(drop, 50f);
    }

    public void Start(){
        
    }
    private int escolherDrop()
    {
        string nomeDaCena = SceneManager.GetActiveScene().name;
        probabilidade = Random.Range(0, 101);
        if (nomeDaCena == "Level 01"){
            if(probabilidade < 70)
            {
                return 0;
            }else if(probabilidade >= 70 && probabilidade <= 85)
            {
                return 1;
            }
            else if (probabilidade >= 85 && probabilidade < 90 )
            {
                return 2;
            }else 
            {
            return 3;
            }
        }else if(nomeDaCena == "Level 02"){
            if(probabilidade < 20)
            {
                return 0;
            }else if(probabilidade >= 20 && probabilidade < 75)
            {
                return 1;
            }
            else if (probabilidade >= 75 && probabilidade < 90 )
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }else
            {
                return 0;
            }
    }
}
