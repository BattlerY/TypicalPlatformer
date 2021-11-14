using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _startMoveModificator;
    [SerializeField] private float _jumpForce;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private bool _isGrounded;

    public void TakeImpulse(float enemyXPosition, float impulseForce)
    {
        int direction = transform.position.x > enemyXPosition ? 1 : -1;
        _rigidBody.AddForce(Vector2.right * direction * impulseForce);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _isGrounded = true;
    }


    private void Update()
    {
        _isGrounded = _rigidBody.velocity.y == 0;

        Move();

        if (_isGrounded && Input.GetKey(KeyCode.Space))
            Jump();
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector2.up * _jumpForce);
        _isGrounded = false;
    }

    private void Move()
    {
        float direction = Input.GetAxis("Horizontal");
        float absoluteDirection = Mathf.Abs(direction);
        _animator.SetFloat(AnimatorPlayerController.Params.Speed, absoluteDirection);
        if (absoluteDirection >= _startMoveModificator)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime * direction);
            transform.localScale = direction > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
    }
}
