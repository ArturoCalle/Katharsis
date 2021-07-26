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

    public void move(Vector3 inputs, bool jump)
    {
        animator.SetFloat("walk", inputs.z);
        if (jump)
        {
            animator.Play("Jump");
            animator.SetBool("jump", false);
        }
        
    }
}
