using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    // Referencia al panel del menú de pausa, marcado como serializable para poder asignarlo desde el editor de Unity.
    [SerializeField] private GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        // Inicialmente, el juego no está pausado y el panel de pausa está desactivado.
        isPaused = false;
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica si el jugador presionó la tecla Escape.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si el juego ya está pausado, se reanuda.
            if (isPaused)
            { 
                Resume();
            }
            // Si el juego no está pausado, se pausa.
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        // Detiene el tiempo en el juego.
        Time.timeScale = 0f;
        // Indica que el juego está pausado.
        isPaused = true;
        // Activa el panel de pausa.
        pausePanel.SetActive(true);

        // Detener animaciones
        foreach (var animator in FindObjectsOfType<Animator>())
        {
            animator.enabled = false;
        }
    }

    public void Resume()
    {
        // Restablece el tiempo en el juego.
        Time.timeScale = 1f;
        // Indica que el juego ya no está pausado.
        isPaused = false;
        // Desactiva el panel de pausa.
        pausePanel.SetActive(false);

        // Reanudar animaciones
        foreach (var animator in FindObjectsOfType<Animator>())
        {
            animator.enabled = true;
        }
    }

    public void MainMenu()
    {
        // Carga la escena del menú principal.
        SceneManager.LoadScene("Main menu"); ;
    }
}
