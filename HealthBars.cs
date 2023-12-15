using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controlar las barras de salud de dos jugadores en un juego
// Declaramos la clase HealthBars, que hereda de MonoBehaviour para poder integrarla en Unity.
public class HealthBars : MonoBehaviour
{
    public UnityEngine.UI.Image Player1BG;
    public UnityEngine.UI.Image Player2BG;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // valor de relleno de las barras de salud de los jugadores según sus salud actual.
        Player1BG.fillAmount = SaveScript.Player1Health;
        Player2BG.fillAmount = SaveScript.Player2Health;

        // Reduce el temporizador del jugador 2 si es mayor a 0.011, para hacer el efecto combo de que no baja la barra hasta que terminen lo golpes.
        if (SaveScript.Player2Timer > 0.011)
        {
            SaveScript.Player2Timer -= 2.0f * Time.deltaTime;
        }
        if (SaveScript.Player1Timer > 0.011)
        {
            SaveScript.Player1Timer -= 2.0f * Time.deltaTime;
        }
        // Si el temporizador del jugador 2 es menor o igual a 0.011, y la barra de salud visual es mayor que la salud actual, la reduce gradualmente.
        if (SaveScript.Player2Timer <= 0.011)
        {
            if (Player2BG.fillAmount > SaveScript.Player2Health)
            {
                Player2BG.fillAmount -= 0.003f;
            }
        }
        if (SaveScript.Player1Timer <= 0.011)
        {
            if (Player1BG.fillAmount > SaveScript.Player1Health)
            {
                Player1BG.fillAmount -= 0.003f;
            }
        }
    }
}