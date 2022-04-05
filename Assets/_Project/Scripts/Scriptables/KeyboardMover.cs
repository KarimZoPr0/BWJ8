// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;
using UnityEngine;

namespace RoboRyanTron.Unite2017.Variables
{
    public class KeyboardMover : MonoBehaviour
    {

        public Animator animator;
        public float speed;
        private Vector3 movement;
        private void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            transform.position += movement * Time.deltaTime * speed;

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            
            if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1  || Input.GetAxisRaw("Vertical") == 1  || Input.GetAxisRaw("Vertical") == -1)
            {
                animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
            }
            
            if (movement.x < 0 && !facingRight)
            {
                Flip();
            }
            if (movement.x > 0 && facingRight)
            {
                Flip();
            }
            
        }

        private bool facingRight;

        public void Flip()
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}