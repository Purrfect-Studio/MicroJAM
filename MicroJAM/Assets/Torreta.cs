using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public TargetsScanner targetScanner;
    public GameObject tiroPrefab;        // Prefab do projétil

    public void Start()
    {
        targetScanner = GetComponent<TargetsScanner>();
    }

    
   public void ScanAndShoot()
   {
    targetScanner.ScanArea();

    if (targetScanner.GetClosestTarget() != null)
    {
        Instantiate(tiroPrefab, transform.position, transform.rotation);
    }

    // se encontrar algo na área, spawnar o tiro
   }
}
