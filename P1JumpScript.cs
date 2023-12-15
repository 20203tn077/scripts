using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1JumpScript : MonoBehaviour
{
    // Variable que representa el GameObject del jugador 1.
    public GameObject Player1;

    // metodo que se activa cuando otro collider entra en el trigger asociado a este objeto.
    public void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto con el que se colisionó tiene la etiqueta "P2SpaceDetector".
        if (other.gameObject.CompareTag("P2SpaceDetector"))
        {
            // Si el jugador 1 está mirando hacia la derecha (FacingRight == true),
            // mueve al jugador 1 hacia la izquierda en el eje X.
            if (Player1Move.FacingRight == true)
            {
                Player1.transform.Translate(-0.8f, 0, 0);
            }
            // Si el jugador 1 está mirando hacia la izquierda (FacingLeft == true),
            // mueve al jugador 1 hacia la derecha en el eje X.
            if (Player1Move.FacingLeft == true)
            {
                Player1.transform.Translate(0.8f, 0, 0);
            }
        }
    }
}
