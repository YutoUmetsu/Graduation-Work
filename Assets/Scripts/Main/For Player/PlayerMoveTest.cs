using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveInput;
    public Vector2 MoveDirection { get; private set; }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        MoveDirection = value.Get<Vector2>().normalized;
    }

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
