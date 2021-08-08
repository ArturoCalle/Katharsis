using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public static AnimatorController instance;
    public GameObject trompi;
    private Animator animator;
    private Animation anim;
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
        time += Time.deltaTime;

        if (time >= interpolationPeriod)
        {
            time = time - interpolationPeriod;
            idle = Random.Range(0f, 10f);
        }
    }
    public void move(Vector3 inputs, float velocityY, bool isGrounded, bool jump, bool escalando, bool corner)
    {
        if (inputs != new Vector3(0, 0, 0))
        {
            animator.SetBool("walk", true);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("LookRight f") || animator.GetCurrentAnimatorStateInfo(0).IsName("LookLeft f"))
            {
                animator.Play("IDLE f");
            }
        }
        else
        {
            animator.SetBool("walk", false);
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE f"))
            {
                if(idle > 6 && idle < 8)
                {
                    animator.Play("LookRight f");
                    idle = 0;
                }else if(idle > 8 && idle < 10)
                {
                    animator.Play("LookLeft f");
                    idle = 0;
                }
            }
            
        }
        animator.SetFloat("yVelocity", velocityY);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("jump", jump);

        if (escalando)
        {
            if (!corner)
            {
                animator.SetBool("corner", true);
            }
            else
            {
                animator.SetBool("corner", false);
            }
            animator.SetBool("climb", true);
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
}
