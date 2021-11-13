using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _checkingPoint;
    [SerializeField] private float _speed;
    [SerializeField] private float _impulse;

    private void Update()
    {
        Debug.DrawRay(_checkingPoint.position, Vector3.down, Color.yellow);

        if (Physics2D.Raycast(_checkingPoint.position, Vector3.down, 0.75f))
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        }
        else
        {
            _speed *= -1;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            int direction = player.transform.position.x > transform.position.x ? 1 : -1;
            player.RigidBody.AddForce(Vector2.right * direction * _impulse);
        }
    }
}
