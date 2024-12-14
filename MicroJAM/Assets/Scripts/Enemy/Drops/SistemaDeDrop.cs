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

        if(probabilidade <= 90)
        {
            return 0;
        }else
        {
            return 1;
        }
    }
}
