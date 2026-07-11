using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveTest : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveInput;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
