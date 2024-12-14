using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDeDrop : MonoBehaviour
{
    public List<GameObject> Drops = new List<GameObject>();

    private int probabilidade;
    public void Dropar()
    {
        GameObject bullet = Instantiate(Drops[escolherDrop()], gameObject.transform.position, Quaternion.identity);
    }

    private int escolherDrop()
    {
        probabilidade = Random.Range(0, 101);

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
        }
        else
        {
            return 3;
        }
    }
}
