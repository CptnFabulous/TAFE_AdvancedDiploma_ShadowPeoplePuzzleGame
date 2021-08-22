using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    public PlayerAnimationController animationHandler;
    
    public Camera playerCamera;
    public float maxCameraDistanceFromPlayer = 5;
    public float paddingAroundCamera = 0.5f;
    public float safeDistanceSoCharacterRenders = 1f;

    public Transform head;
    public float cameraSensitivityX = 50;
    public float cameraSensitivityY = 50;
    Vector2 rotationValues;

    Rigidbody rb;
    public float movementSpeed = 6;
    Vector3 movementValues;
    public float jumpForce = 5;
    public float groundedRaycastLength = 0.01f;
    bool willJump;

    public bool IsGrounded
    {
        get
        {
            float distanceBackToPreventScrewyCollisions = 0.5f;
            Vector3 origin = transform.position + transform.up * distanceBackToPreventScrewyCollisions;
            RaycastHit groundedCheck;
            if (Physics.Raycast(origin, -transform.up, out groundedCheck, distanceBackToPreventScrewyCollisions + groundedRaycastLength, ~0))
            {
                return true;
            }
            return false;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceForCameraBehindPlayer = maxCameraDistanceFromPlayer;
        RaycastHit cameraCheck;
        if (Physics.SphereCast(head.position, paddingAroundCamera, -head.forward, out cameraCheck, maxCameraDistanceFromPlayer + paddingAroundCamera, playerCamera.cullingMask))
        {
            distanceForCameraBehindPlayer = cameraCheck.distance - paddingAroundCamera;
        }
        playerCamera.transform.localPosition = new Vector3(0, 0, -distanceForCameraBehindPlayer);
        animationHandler.renderer.enabled = (distanceForCameraBehindPlayer > safeDistanceSoCharacterRenders);

        rotationValues.x = Input.GetAxis("Mouse X") * cameraSensitivityX * Time.deltaTime;
        rotationValues.y -= Input.GetAxis("Mouse Y") * cameraSensitivityY * Time.deltaTime;
        transform.Rotate(0, rotationValues.x, 0);
        head.transform.localRotation = Quaternion.Euler(rotationValues.y, 0, 0);

        float moveX = Input.GetAxis("Horizontal") * movementSpeed;
        float moveZ = Input.GetAxis("Vertical") * movementSpeed;
        movementValues = new Vector3(moveX, 0, moveZ);

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            willJump = true;
        }
    }

    private void FixedUpdate()
    {
        movementValues = transform.rotation * movementValues;
        rb.MovePosition(transform.position + movementValues * Time.deltaTime);

        if (willJump == true)
        {
            willJump = false;
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }
    }
}
