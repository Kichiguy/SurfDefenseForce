using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonScript : MonoBehaviour {
    Animator animator;
	// Use this for initialization
	void Start() {
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Neon Shoot"))
        {
            animator.SetBool("NeonIsShooting", false);
        }
	}

    public void neonShoot()
    {
        animator.SetBool("NeonIsShooting", true);
        
    }


}
