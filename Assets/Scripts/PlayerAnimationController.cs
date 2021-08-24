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
        Vector3 moveValues = movement.transform.rotation * movement.MovementValues;
        if (moveValues.magnitude > 0)
        {
            transform.LookAt(movement.transform.position + moveValues);
            //Debug.DrawRay(transform.position + transform.up, moveValues * 2, Color.green);
        }

        controller.SetBool("Is grounded", movement.GroundingData.collider != null);
        controller.SetFloat("Movement speed", moveValues.magnitude);
    }
}
