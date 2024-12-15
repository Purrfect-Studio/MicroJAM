using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public void CarregarFase(string nomeFase)
    {
        SceneManager.LoadScene(nomeFase);
    }
}
