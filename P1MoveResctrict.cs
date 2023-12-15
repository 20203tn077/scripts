using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1MoveResctrict : MonoBehaviour
{
    // controla las restricciones de movimiento del jugador 1 cuando entra y sale de zonas específicas,
    // marcadas por triggers con las etiquetas "P2Left" y "P2Right". 
    // metodo que se dispara cuando otro collider entra en el trigger asociado a este objeto.
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que se colisionó tiene la etiqueta "P2Left".
        // Si es así, desactiva la capacidad del jugador 1 de moverse hacia la derecha.
        if (other.gameObject.CompareTag("P2Left"))
        {
            Player1Move.WalkRightP1 = false;
        }
        // Verifica si el objeto con el que se colisionó tiene la etiqueta "P2Right".
        // Si es así, desactiva la capacidad del jugador 1 de moverse hacia la izquierda.
        if (other.gameObject.CompareTag("P2Right"))
        {
            Player1Move.WalkLeftP1 = false;
        }
    }

    // metodo que se dispara cuando otro collider sale del trigger asociado a este objeto.
    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale del trigger tiene la etiqueta "P2Left".
        // Si es así, reactiva la capacidad del jugador 1 de moverse hacia la derecha.
        if (other.gameObject.CompareTag("P2Left"))
        {
            Player1Move.WalkRightP1 = true;
        }
        // Verifica si el objeto que sale del trigger tiene la etiqueta "P2Right".
        // Si es así, reactiva la capacidad del jugador 1 de moverse hacia la izquierda.
        if (other.gameObject.CompareTag("P2Right"))
        {
            Player1Move.WalkLeftP1 = true;
        }
    }

}
