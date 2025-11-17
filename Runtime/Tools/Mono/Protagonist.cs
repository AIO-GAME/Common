using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Protagonist : MonoBehaviour
{
    public static Protagonist         Current;
    private       CharacterController _characterController;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;

    private float   horizontal;
    private float   vertical;
    private Vector3 direction;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        if (Current == null)
            Current = this;
    }

    private void Update() { ProtagonistMovement(); }

    /// <summary>
    /// 移动
    /// </summary>
    private void ProtagonistMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical   = Input.GetAxis("Vertical");

        direction.Set(horizontal, 0, vertical);
        direction.y = -9.8f; //Simple Gravity
        _characterController.Move(direction * _moveSpeed * Time.deltaTime);
    }
}
