using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimationController : MonoBehaviour
{
    public MovementController movement;
    public Animator controller;
    public Renderer renderer;

    /*
    private void Awake()
    {
        a = GetComponent<Animator>();
    }
    */
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
    private void LateUpdate()
    {
        controller.SetBool("Is grounded", movement.IsGrounded);
    }
}
