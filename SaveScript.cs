using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SaveScript : MonoBehaviour
{
    public static float Player1Health = 0.989f;
    public static float Player2Health = 0.989f;
    public static float Player1Timer = 2.0f;
    public static float Player2Timer = 2.0f;
    public static bool P1Reacting = false;
    public static bool P2Reacting = false;

    // Start is called before the first frame update
    void Start()
    {
        Player1Health = 0.989f;
        Player2Health = 0.989f;
        P1Reacting = false;
        P2Reacting = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
