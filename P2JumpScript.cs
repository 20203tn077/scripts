using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2JumpScript : MonoBehaviour
{
    // Variable que representa el GameObject del jugador 2.
    public GameObject Player2;

    // metodo que se dispara cuando otro collider entra en el trigger asociado a este objeto.
    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto con el que se colisionó tiene la etiqueta "P1SpaceDetector".
        if (other.gameObject.CompareTag("P1SpaceDetector"))
        {
            // Si el jugador 2 está mirando hacia la izquierda (FacingLeftP2 == true),
            // mueve al jugador 2 hacia la derecha en el eje X.
            if (Player2Move.FacingLeftP2 == true)
            {
                Player2.transform.Translate(0.8f, 0, 0);
                //Debug.Log("Space Left detected");
            }
            // Si el jugador 2 está mirando hacia la derecha (FacingRightP2 == true),
            // mueve al jugador 2 hacia la izquierda en el eje X
            if (Player2Move.FacingRightP2 == true)
            {
                Player2.transform.Translate(-0.8f, 0, 0);
                //Debug.Log("Space Right detected");
            }
        }
    }
}
    
