using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoirScript : MonoBehaviour {
    Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Noir Shoot"))
        {
            animator.SetBool("NoirIsShooting", false);
        }
    }

    public void noirShoot()
    {
        animator.SetBool("NoirIsShooting", true);

    }
}
