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

    public void move(Vector3 inputs, float velovityY, bool isGrounded, bool jump)
    {
        animator.SetFloat("walk", inputs.z);
        animator.SetFloat("yVelocity", velovityY);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("jump", jump);
    }
}
