using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2MoveResctrict : MonoBehaviour
{
    // metodo que se dispara cuando otro collider entra en el trigger asociado a este objeto.
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que se colisionó tiene la etiqueta "P1Left".
        // Si es así, desactiva la capacidad del jugador 2 de moverse hacia la derecha.
        if (other.gameObject.CompareTag("P1Left"))
        {
            Player2Move.WalkRight = false;
        }
        // Verifica si el objeto con el que se colisionó tiene la etiqueta "P1Right".
        // Si es así, desactiva la capacidad del jugador 2 de moverse hacia la izquierda.
        if (other.gameObject.CompareTag("P1Right"))
        {
            Player2Move.WalkLeft = false;
        }
    }

    // metodo que se dispara cuando otro collider sale del trigger asociado a este objeto.
    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale del trigger tiene la etiqueta "P1Left".
        // Si es así, reactiva la capacidad del jugador 2 de moverse hacia la derecha.
        if (other.gameObject.CompareTag("P1Left"))
        {
            Player2Move.WalkRight = true;
        }
        // Verifica si el objeto que sale del trigger tiene la etiqueta "P1Right".
        // Si es así, reactiva la capacidad del jugador 2 de moverse hacia la izquierda.
        if (other.gameObject.CompareTag("P1Right"))
        {
            Player2Move.WalkLeft = true;
        }
    }

}
