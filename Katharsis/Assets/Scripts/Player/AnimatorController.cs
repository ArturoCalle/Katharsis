using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public static AnimatorController instance;
    public GameObject trompi;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = trompi.GetComponent<Animator>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move(Vector3 inputs, float velocityY, bool isGrounded, bool jump)
    {
        if(inputs != new Vector3(0, 0, 0))
        {
            animator.SetFloat("walk", 1);
        }
        else
        {
            animator.SetFloat("walk", 0);
        }
        animator.SetFloat("yVelocity", velocityY);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("jump", jump);
    }
}
