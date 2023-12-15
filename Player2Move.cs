using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Player2Move : MonoBehaviour
{
    private Animator Anim;
    public float WalkSpeed = 0.09f;
    public float JumpSpeed = 0.05f;
    private float MoveSpeed;
    private bool IsJumping = false;
    private AnimatorStateInfo Player1Layer0;
    private bool CanWalkLeft = true;
    private bool CanWalkRight = true;
    public GameObject Player1; // Referencia al jugador 1 (para uso en interacciones entre jugadores).
    public GameObject Opponent; // Referencia al oponente (podría ser otro jugador o un NPC).
    private Vector3 OppPosition; // Posición del oponente.
    public static bool FacingLeftP2 = false;
    public static bool FacingRightP2 = true;
    public static bool WalkLeft = true;
    public static bool WalkRight = true;
    public AudioClip LightPunch;
    public AudioClip HeavyPunch;
    public AudioClip LightKick;
    public AudioClip HeavyKick;
    private AudioSource MyPlayer;
    public GameObject Restrict;
    public Rigidbody RB;
    public Collider BoxCollider;
    public Collider CapsuleCollider;

    void OnEnable()
    {
        // Configura la orientación inicial del jugador al habilitar el script. 
        FacingLeftP2 = false;
        FacingRightP2 = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        // Inicialización de componentes y configuraciones iniciales.
        Opponent = GameObject.Find("Player1");
        Anim = GetComponentInChildren<Animator>();
        StartCoroutine(FaceRight());
        MyPlayer = GetComponentInChildren<AudioSource>();
        MoveSpeed = WalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Controles de movimiento, animación y reacción basados en la entrada del usuario y la lógica del juego.
        // Incluye movimiento horizontal, saltos, agacharse, reacciones a golpes, etc.

        // ... (código de actualización y lógica del juego)

        // Ajusta la velocidad de caminata si el jugador está saltando
        if (Player2Actions.FlyingJumpP2 == true)
            {
                WalkSpeed = JumpSpeed;
            }
            else
            {
                WalkSpeed = MoveSpeed;
            }
        // Comprueba si el jugador ha sido noqueado
        if (SaveScript.Player2Health <= 0.011)
            {
                Debug.Log("KnockedOutCall");
                Anim.SetTrigger("KnockOut");
                Player1.GetComponent<Player2Actions>().enabled = false;
                StartCoroutine(KnockedOut());

            }
        // Comprueba si el oponente ha sido noqueado
        if (SaveScript.Player1Health <= 0.011)
            {
                Anim.SetTrigger("Victory");
                Player1.GetComponent<Player2Actions>().enabled = false;
                this.GetComponent<Player2Move>().enabled = false;
            }


        // Obtiene información de la capa actual de la animación
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        // Restringe al jugador de salir de la pantalla.
        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);

            if (ScreenBounds.x > Screen.width - 200)
            {
                CanWalkRight = false;
            }
            if (ScreenBounds.x < 200)
            {
                CanWalkLeft = false;
            }
            else if (ScreenBounds.x > 200 && ScreenBounds.x < Screen.width - 200)
            {
                CanWalkRight = true;
                CanWalkLeft = true;
            }

        // Obtiene la posición del oponente
        OppPosition = Opponent.transform.position;

        // Obtiene la posición del oponente y ajusta la orientación del jugador.
        if (OppPosition.x > Player1.transform.position.x)
            {
                StartCoroutine(FaceLeft());
            }
            if (OppPosition.x < Player1.transform.position.x)
            {
                StartCoroutine(FaceRight());
            }


        /// Controla el movimiento horizontal
        if (Player1Layer0.IsTag("Motion"))
            {
            Time.timeScale = 1.0f;
                if (Input.GetAxis("HorizontalP2") > 0)
                {
                    if (CanWalkRight == true)
                    {
                        if (WalkRight == true)
                        {
                            Anim.SetBool("Forward", true);
                            transform.Translate(WalkSpeed , 0, 0);
                        }
                    }
                }
                if (Input.GetAxis("HorizontalP2") < 0)
                {
                    if (CanWalkLeft == true)
                    {
                        if (WalkLeft == true)
                        {
                            Anim.SetBool("Backward", true);
                            transform.Translate(-WalkSpeed , 0, 0);
                        }
                    }
                }
            }
            if (Input.GetAxis("HorizontalP2") == 0)
            {
                Anim.SetBool("Forward", false);
                Anim.SetBool("Backward", false);
            }


        // Control de saltos y agacharse.
        if (Input.GetAxis("VerticalP2") > 0)
            {
                if (IsJumping == false)
                {
                    IsJumping = true;
                    Anim.SetTrigger("Jump");
                    StartCoroutine(JumpPause());
                }
            }
            if (Input.GetAxis("VerticalP2") < 0)
            {
                Anim.SetBool("Crouch", true);
            }
            if (Input.GetAxis("VerticalP2") == 0)
            {
                Anim.SetBool("Crouch", false);
            }

        // Restablece las restricciones de movimiento si no está activa la restricción.
        if (Restrict.gameObject.activeInHierarchy == false)
            {
                WalkLeft = true;
                WalkRight = true;
            }
        // Ajusta el estado físico del jugador basado en si está bloqueando o no.
        if (Player1Layer0.IsTag("Block"))
            {
                RB.isKinematic = true;
                BoxCollider.enabled = false;
                CapsuleCollider.enabled = false;
            }
            else
            {
                BoxCollider.enabled = true;
                CapsuleCollider.enabled = true;
                RB.isKinematic = false;
            }
        
    }

    // Gestiona las colisiones con otros objetos, como los golpes del oponente.
    private void OnTriggerEnter(Collider other)
    {
        // Ejecuta reacciones y sonidos basados en el tipo de golpe recibido.
        if (SaveScript.P2Reacting == false)
        {
            if (other.gameObject.CompareTag("FistLight"))
            {
                Anim.SetTrigger("HeadReact");
                MyPlayer.clip = LightPunch;
                MyPlayer.Play();
            }
            if (other.gameObject.CompareTag("FistHeavy"))
            {
                Anim.SetTrigger("HeadReact");
                MyPlayer.clip = HeavyPunch;
                MyPlayer.Play();
            }
            if (other.gameObject.CompareTag("KickHeavy"))
            {
                Anim.SetTrigger("BigReact");
                MyPlayer.clip = HeavyKick;
                MyPlayer.Play();
            }
            if (other.gameObject.CompareTag("KickLight"))
            {
                Anim.SetTrigger("HeadReact");
                MyPlayer.clip = LightKick;
                MyPlayer.Play();
            }
        }
    }

    // Corrutinas para controlar acciones temporizadas.

    // Controla la duración del salto.
    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
    }
    // Gira al jugador hacia la izquierda después de un breve retraso.
    IEnumerator FaceLeft()
    {
        if (FacingLeftP2 == true)
        {
            FacingLeftP2 = false;
            FacingRightP2 = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, -180, 0);
            Anim.SetLayerWeight(1, 0);
        }

    }
    // Gira al jugador hacia la derecha después de un breve retraso.
    IEnumerator FaceRight()
    {
        if (FacingRightP2 == true)
        {
            FacingRightP2 = false;
            FacingLeftP2 = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);
            Anim.SetLayerWeight(1, 1);
        }

    }
    // Controla el comportamiento cuando el jugador es noqueado.
    IEnumerator KnockedOut()
    {
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Player2Move>().enabled = false;
    }
}
