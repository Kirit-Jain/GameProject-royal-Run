using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Tuning Variables
    [SerializeField] float moveSpeed = 5f;

    //Refrence Variable
    Rigidbody rb;

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
        Debug.Log(movement);
    }
    

    void HandleMovement()
    {
        Vector3 currentPosition = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
        Vector3 newPosition = currentPosition + (moveDirection * (Time.fixedDeltaTime * moveSpeed));


        rb.MovePosition(newPosition);
    }


}
