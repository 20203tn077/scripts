using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Actions : MonoBehaviour
{
    public float JumpSpeed = 1.0f;
    // 
    public GameObject Player1;
    // controlar las animaciones de un objeto
    // con Anim, el script puede activar o cambiar animaciones en respuesta a entradas del usuario o eventos del juego
    private Animator Anim;
    // almacenar información sobre el estado actual de la capa 0 del Animator
    // se usa para consultar qué animación se está reproduciendo actualmente y tomar decisiones en base a esa información.
    private AnimatorStateInfo Player1Layer0;
    private bool HeavyMoving = false;
    private bool HeavyReact = false;
    public float PunchSlideAmt = 2f;
    public float HeavyReactAmt = 3f;
    private AudioSource MyPlayer;
    public AudioClip PunchWoosh;
    public AudioClip KickWoosh;
    public static bool Hits = false;
    public static bool FlyingJumpP1 = false;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Controla el deslizamiento durante un golpe pesado.
        if (HeavyMoving == true)
        {
            // Mueve al jugador en la dirección en la que está mirando.
            if (Player1Move.FacingRight == true)
            {
                Player1.transform.Translate(PunchSlideAmt * Time.deltaTime, 0, 0);
            }
            if (Player1Move.FacingLeft == true)
            {
                Player1.transform.Translate(-PunchSlideAmt * Time.deltaTime, 0, 0);
            }
        }

        // Controla el deslizamiento en reacción a un golpe pesado.
        if (HeavyReact == true)
        {
            if (Player1Move.FacingRight == true)
            {
                Player1.transform.Translate(-HeavyReactAmt * Time.deltaTime, 0, 0);
            }
            if (Player1Move.FacingLeft == true)
            {
                Player1.transform.Translate(HeavyReactAmt * Time.deltaTime, 0, 0);
            }
        }

        // Obtiene información del estado actual de la animación.
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        //Standing attacks
        if (Player1Layer0.IsTag("Motion"))
        {
            // Disparadores para diferentes ataques y bloqueos.
            if (Input.GetButtonDown("Fire1"))
            {
                Anim.SetTrigger("LightPunch");
                Hits = false;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                Anim.SetTrigger("HeavyPunch");
                Hits = false;
            }
            if (Input.GetButtonDown("Fire3"))
            {
                Anim.SetTrigger("LightKick");
                Hits = false;
            }
            if (Input.GetButtonDown("Jump"))
            {
                Anim.SetTrigger("HeavyKick");
                Hits = false;
            }
            if (Input.GetButtonDown("Block"))
            {
                Anim.SetTrigger("BlockOn");
            }
        }

        // Control para finalizar el bloqueo.
        if (Player1Layer0.IsTag("Block"))
        {
            if (Input.GetButtonUp("Block"))
            {
                Anim.SetTrigger("BlockOff");
            }
        }


        // Control para ataque agachado.
        if (Player1Layer0.IsTag("Crouching"))
        {
            if (Input.GetButtonDown("Fire3"))
            {
                Anim.SetTrigger("LightKick");
                Hits = false;
            }
        }

        // Control para movimientos aéreos.
        if (Player1Layer0.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Anim.SetTrigger("HeavyKick");
                Hits = false;
            }
        }
    }

    public void JumpUp()
    {
        Player1.transform.Translate(0, JumpSpeed * Time.deltaTime, 0);
    }
    public void HeavyMove()
    {
        StartCoroutine(PunchSlide());
    }
    public void HeavyReaction()
    {
        StartCoroutine(HeavySlide());
    }
    public void FlipUp()
    {
        Player1.transform.Translate(0, JumpSpeed * Time.deltaTime, 0);
        FlyingJumpP1 = true;
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0, JumpSpeed * Time.deltaTime, 0);
        FlyingJumpP1 = true;
    }

    public void IdleSpeed()
    {
        FlyingJumpP1 = false;
    }

    public void ResetTime()
    {
        Time.timeScale = 1.0f;
    }

    public void KickWooshSound()
    {
        MyPlayer.clip = KickWoosh;
        MyPlayer.Play();
    }

    public void PunchWooshSound()
    {
        MyPlayer.clip = PunchWoosh;
        MyPlayer.Play();
    }

    // Corrutinas para controlar la duración de los deslizamientos.
    // crear retrasos o efectos que se desarrollan a lo largo del tiempo sin bloquear el resto del juego
    IEnumerator PunchSlide()
    {
        // Se activa el estado de deslizamiento.
        HeavyMoving = true;
        // La corrutina pausa su ejecución durante el tiempo especificado (0.1 segundos) 
        // sin bloquear el resto del juego. Esto crea un retraso.
        yield return new WaitForSeconds(0.1f);
        // Después del retraso, se desactiva el estado de deslizamiento.
        HeavyMoving = false;
    }

    IEnumerator HeavySlide()
    {
        HeavyReact = true;
        yield return new WaitForSeconds(0.3f);
        HeavyReact = false;
    }
}

