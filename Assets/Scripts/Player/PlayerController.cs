using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Tuning Variables
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float minMoveSpeed = 5f;
    [SerializeField] float maxMoveSpeed = 13f;
    [SerializeField] float xClamp = 5f;
    [SerializeField] float zClamp = 5f;

    //Refrence Variable
    Rigidbody rb;
    LevelGenerator levelGenerator;

    //Variables
    Vector2 movement; //input variable

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        // Debug.Log(movement);
    }

    public void ChangeMoveSpeed(float amount)
    {
        moveSpeed += amount;
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
    }
    

    void HandleMovement()
    {
        Vector3 currentPosition = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
        Vector3 newPosition = currentPosition + (moveDirection * (Time.fixedDeltaTime * moveSpeed));

        //Clamping the x and z of the newPosition
        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);


        rb.MovePosition(newPosition);
    }


}
