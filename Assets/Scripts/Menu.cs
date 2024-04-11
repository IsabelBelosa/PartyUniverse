using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void MenuJugadores()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Inicio()
    {
        SceneManager.LoadScene("Tablero");
    }
    
}
