using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _startMoveModificator;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _cameraLerp;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private bool _isGrounded;

    public Rigidbody2D GetRigidBody => _rigidBody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _isGrounded = true;
    }

    private void Update()
    {
        _isGrounded = _rigidBody.velocity.y == 0;

        Vector3 cameraPosition = Camera.main.transform.position;
        cameraPosition.x = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPosition, _cameraLerp*Time.deltaTime);

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
        _animator.SetFloat("Speed", absoluteDirection);
        if (absoluteDirection >= _startMoveModificator)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime * direction);
            transform.localScale = direction > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
            collision.gameObject.SetActive(false);
    }
}
