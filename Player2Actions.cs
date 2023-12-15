using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Actions : MonoBehaviour
{
    public float JumpSpeed = 1.0f;
    public float FlipSpeed = 0.8f;
    // Referencias a GameObjects y componentes para animaciones y sonidos.
    public GameObject Player1;
    private Animator Anim;
    private AnimatorStateInfo Player1Layer0;
    // Variables para controlar el movimiento durante golpes pesados y reacciones.
    private bool HeavyMoving = false;
    private bool HeavyReact = false;
    public float PunchSlideAmt = 2f;
    public float HeavyReactAmt = 4f;
    private AudioSource MyPlayer;
    // Sonidos para golpes y patadas.
    public AudioClip PunchWoosh;
    public AudioClip KickWoosh;
    // Variables estáticas para controlar si el jugador ha realizado un golpe y si está realizando un salto.
    public static bool HitsP2 = false;
    public static bool FlyingJumpP2 = false;

    // Start is called before the first frame update
    void Start()
    {
        // Inicialización de los componentes Animator y AudioSource.
        Anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // Controla el deslizamiento durante golpes pesados.
        if (HeavyMoving == true)
        {
            if (Player2Move.FacingRightP2 == true)
            {
                Player1.transform.Translate(PunchSlideAmt * Time.deltaTime, 0, 0);
            }
            if (Player2Move.FacingLeftP2 == true)
            {
                Player1.transform.Translate(-PunchSlideAmt * Time.deltaTime, 0, 0);
            }
        }

        // Controla el deslizamiento en reacción a golpes pesados.
        if (HeavyReact == true)
        {
            if (Player2Move.FacingRightP2 == true)
            {
                Player1.transform.Translate(-HeavyReactAmt * Time.deltaTime, 0, 0);
            }
            if (Player2Move.FacingLeftP2 == true)
            {
                Player1.transform.Translate(HeavyReactAmt * Time.deltaTime, 0, 0);
            }
        }

        // Escucha al Animator para conocer el estado actual de la animación.
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        // Controla los ataques de pie, incluyendo golpes y patadas.
        if (Player1Layer0.IsTag("Motion"))
        {
            if (Input.GetButtonDown("Fire1P2"))
            {
                Anim.SetTrigger("LightPunch");
                HitsP2 = false;
            }
            if (Input.GetButtonDown("Fire2P2"))
            {
                Anim.SetTrigger("HeavyPunch");
                HitsP2 = false;
            }
            if (Input.GetButtonDown("Fire3P2"))
            {
                Anim.SetTrigger("LightKick");
                HitsP2 = false;
            }
            if (Input.GetButtonDown("JumpP2"))
            {
                Anim.SetTrigger("HeavyKick");
                HitsP2 = false;
            }
            if (Input.GetButtonDown("BlockP2"))
            {
                Anim.SetTrigger("BlockOn");
            }
        }

        // Controla el fin del bloqueo.
        if (Player1Layer0.IsTag("Block"))
        {
            if (Input.GetButtonUp("BlockP2"))
            {
                Anim.SetTrigger("BlockOff");
            }
        }


        //Crouching attack
        if (Player1Layer0.IsTag("Crouching"))
        {
            if (Input.GetButtonDown("Fire3P2"))
            {
                Anim.SetTrigger("LightKick");
                HitsP2 = false;
            }
        }

        // Controla movimientos aéreos.
        if (Player1Layer0.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Anim.SetTrigger("HeavyKick");
                HitsP2 = false;
            }
        }
    }

    // para realizar un salto.
    public void JumpUp()
    {
        // Mueve al jugador hacia arriba en el eje Y, usando la velocidad de salto.
        Player1.transform.Translate(0, JumpSpeed * Time.deltaTime, 0);
    }
    // metodo para iniciar un movimiento pesado.
    public void HeavyMove()
    {
        // Inicia la corrutina PunchSlide que controla el deslizamiento durante un golpe pesado
        StartCoroutine(PunchSlide());
    }
    // Método para reaccionar a un golpe pesado.
    public void HeavyReaction()
    {
        // Inicia la corrutina HeavySlide que controla el deslizamiento en respuesta a un golpe pesado.
        StartCoroutine(HeavySlide());
    }
    // Método para realizar un salto hacia arriba (flip).
    public void FlipUp()
    {
        // Mueve al jugador hacia arriba en el eje Y, usando la velocidad de flip.
        Player1.transform.Translate(0, FlipSpeed * Time.deltaTime, 0);
        // Indica que el jugador está realizando un salto tipo flip.
        FlyingJumpP2 = true;
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0, FlipSpeed * Time.deltaTime, 0);
        FlyingJumpP2 = true;
    }
    // Método para restablecer la velocidad de movimiento a la normalidad.
    public void IdleSpeed()
    { 
        // Indica que el jugador ha terminado su movimiento aéreo.
        FlyingJumpP2 = false;
    }
    // Método para reproducir el sonido de una patada.
    public void KickWooshSound()
    {
        MyPlayer.clip = KickWoosh;
        MyPlayer.Play();
    }
    // Método para reproducir el sonido de un golpe.
    public void PunchWooshSound()
    {
        MyPlayer.clip = PunchWoosh;
        MyPlayer.Play();
    }
    // Corrutina para controlar la duración del deslizamiento durante un golpe pesado.
    IEnumerator PunchSlide()
    {
        HeavyMoving = true;
        yield return new WaitForSeconds(0.1f);
        HeavyMoving = false;
    }

    // Corrutina para controlar la duración de la reacción a un golpe pesado.
    IEnumerator HeavySlide()
    {
        HeavyReact = true;
        yield return new WaitForSeconds(0.3f);
        HeavyReact = false;
    }
}
