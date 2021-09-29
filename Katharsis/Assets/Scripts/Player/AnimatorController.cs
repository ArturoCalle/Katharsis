using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public static AnimatorController instance;
    public GameObject trompi;
    private Animator animator;
    private float idle = 0;
    private float time = 0.0f;
    private float interpolationPeriod = 10f;

    // Start is called before the first frame update
    void Start()
    {
        animator = trompi.GetComponent<Animator>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Se saca un numero aleatorio entre 1 y 10 cada 10s para cambiar la animacion de IDLE por la de mirar a la izquierda o a la derecha
        time += Time.deltaTime;

        if (time >= interpolationPeriod)
        {
            time = time - interpolationPeriod;
            idle = Random.Range(0f, 10f);
        }
        //
    }
    public void move(Vector3 inputs, float velocityY, bool isGrounded, bool jump, bool escalando, bool corner)
    {
        //Se validan los inputs para cambiar las animaciones
        if (inputs != new Vector3(0, 0, 0))
        {
            animator.SetBool("walk", true);
            //si la animacion es una de las las dos debe volver a la de IDLE ya que esta es la que está conectada con las demas
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("LookRight f") || animator.GetCurrentAnimatorStateInfo(0).IsName("LookLeft f"))
            {
                animator.Play("IDLE f");
            }
        }
        else
        {
            //Si no hay inputs hace las animaciones de IDLE segun el numero aleatorio generado en el update
            animator.SetBool("walk", false);
            IDLE();
            
        }
        //Se asignan los valores de las variables controladores que llegan del PlayerControls
        animator.SetFloat("yVelocity", velocityY);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("jump", jump);
        //Si la Variable de escalar Está activada hace los cambios respectivos segun los inputs y las colisiones del corner
        if (escalando)
        {
            if (corner)
            {
                animator.speed = 1;
                animator.SetBool("corner", true);
            }
            else
            {
                animator.SetBool("corner", false);
            }
            animator.SetBool("climb", true);
            //Como la variable input puede ser -1, 0 ó 1 se asigna ese valor al animator speed
            //de este modo la animacion puede pausarse o correrse en reversa segun el input
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Climb up f"))
            {
                if(inputs.z != 0)
                {
                    animator.speed = 1;
                    animator.SetFloat("upOrDown", inputs.z);
                }
                if (inputs.z == 0)
                {
                    animator.speed = 0;
                }
            }
        }
        else
        {
            animator.SetBool("climb", false);
            animator.speed = 1;
        }

        
        
        
    }
    public void IDLE()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE f"))
        {
            
            if (idle > 2 && idle < 6)
            {
                animator.Play("LookRight f");
                idle = 0;
            }
            else if (idle >= 6 && idle <= 10)
            {
                animator.Play("LookLeft f");
                idle = 0;
            }
        }
    }
}
