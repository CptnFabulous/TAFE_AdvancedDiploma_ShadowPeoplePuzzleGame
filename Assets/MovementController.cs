using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    public PlayerAnimationController animationHandler;
    
    [Header("Camera")]
    public Camera playerCamera;
    public float maxCameraDistanceFromPlayer = 5;
    public float paddingAroundCamera = 0.5f;
    public float safeDistanceSoCharacterRenders = 1f;

    [Header("Looking around")]
    public Transform head;
    public float cameraSensitivityX = 50;
    public float cameraSensitivityY = 50;
    Vector2 rotationValues;

    [Header("Movement")]
    Rigidbody rb;
    public float movementSpeed = 6;
    public Vector3 MovementValues { get; private set; }

    [Header("Jumping")]
    public float jumpForce = 5;
    public float groundedRaycastLength = 0.01f;
    public float groundedRaycastRadius = 0.5f;
    public LayerMask groundingDetection = ~0;
    bool willJump;
    /*
    RaycastHit groundingData;
    public bool IsGrounded
    {
        get
        {
            float distanceBackToPreventScrewyCollisions = 0.5f;
            Vector3 origin = transform.position + transform.up * distanceBackToPreventScrewyCollisions;
            if (Physics.SphereCast(origin, groundedRaycastRadius, -transform.up, out groundingData, distanceBackToPreventScrewyCollisions + groundedRaycastLength, ~0))
            {
                return true;
            }
            return false;
        }
    }
    */
    public RaycastHit GroundingData
    {
        get
        {
            RaycastHit rh;
            float distanceBackToPreventScrewyCollisions = 0.5f;
            Vector3 origin = transform.position + transform.up * distanceBackToPreventScrewyCollisions;
            float distance = distanceBackToPreventScrewyCollisions + groundedRaycastLength;
            Physics.Raycast(origin, -transform.up, out rh, distance, groundingDetection);
            //Physics.SphereCast(origin, groundedRaycastRadius, -transform.up, out rh, distance, groundingDetection);
            /*
            if (Physics.SphereCast(origin, groundedRaycastRadius, -transform.up, out rh, distance, groundingDetection))
            {
                
            }
            */
            Debug.Log(rh.collider);
            return rh;
        }
    }
    
    public void LookAt(Vector3 direction)
    {
        Vector3 torsoDirection = Vector3.ProjectOnPlane(direction, transform.up);
        transform.LookAt(transform.position + torsoDirection);
        head.LookAt(head.position + direction);
        head.transform.localRotation = Quaternion.Euler(head.transform.localRotation.x, 0, 0);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */
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
        MovementValues = new Vector3(moveX, 0, moveZ);

        if (Input.GetButtonDown("Jump") && GroundingData.collider != null)
        {
            Debug.Log("TEST SCREW YOU");
            willJump = true;
        }
    }

    private void FixedUpdate()
    {
        Quaternion changeMovementBasedOnRotation = transform.rotation;
        /*
        if (IsGrounded)
        {
            changeMovementBasedOnRotation = Quaternion.LookRotation(transform.forward, groundingData.normal);
        }
        */
        MovementValues = changeMovementBasedOnRotation * MovementValues;
        rb.MovePosition(transform.position + MovementValues * Time.deltaTime);

        if (willJump == true)
        {
            willJump = false;
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }
    }
}
