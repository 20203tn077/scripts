using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// manejar la lógica de colisiones y daño para un personaje 
public class Player2TriggerAI : MonoBehaviour
{
    // Referencia al Collider del objeto.
    public Collider Col;
    public float DamageAmt = 0.1f;

    // Flag para determinar si este script está controlando al jugador 2
    public bool Player2 = true;

    // Update is called once per frame
    void Update()
    {
        // Controla si el Collider debe estar activado o no, basado en si el jugador ha realizado un ataque exitoso.
        if (Player2 == true)
        {
            if (Player2ActionsAI.HitsAI == false)
            {
                Col.enabled = true;
            }
            else
            {
                Col.enabled = false;
            }
        }
        else if (Player1Actions.Hits == false)
            {
                Col.enabled = true;
            }
            else
            {
                Col.enabled = false;
            }
        }

    private void OnTriggerEnter(Collider other)
    { // Lógica de colisión y daño.
        if (Player2 == true)
        {
            // Si es el jugador 2, y colisiona con el jugador 1, aplica daño y actualiza el estado de golpe.
            if (other.gameObject.CompareTag("Player1"))
            {
                Player2ActionsAI.HitsAI= true;
                SaveScript.Player1Health -= DamageAmt;
                if (SaveScript.Player1Timer < 2.0f)
                {
                    SaveScript.Player1Timer += 2.0f;
                }
            }
        }
        else if(Player2 == false)
        {
            // Si es el jugador 1 (o un personaje que no es el jugador 2), y colisiona con el jugador 2,
            // aplica daño y actualiza el estado de golpe
            if (other.gameObject.CompareTag("Player2"))
            {
                Player1Actions.Hits = true;
                SaveScript.Player2Health -= DamageAmt;
                if (SaveScript.Player2Timer < 2.0f)
                {
                    SaveScript.Player2Timer += 2.0f;
                }

            }
        }
    }

}
