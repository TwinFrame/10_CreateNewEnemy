using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour
{
	[SerializeField] private int _damage;
	[SerializeField] private float _speed;
	[SerializeField] private Direction _directionOfMovement;

	private Vector2 _direction;

	private enum Direction
	{
		Left,
		Right
	}

	private void Awake()
	{
		switch (_directionOfMovement)
		{
			case Direction.Left:
			default:
				_direction = Vector2.left;
				break;
			case Direction.Right:
				_direction = Vector2.right;
				break;
		}
	}

	public int Damage => _damage;

	private void Update()
	{
		transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy) && GetComponentInParent<Enemy>() == null)
		{
			enemy.TakeDamage(_damage);

			Destroy(gameObject);

			return;
		}

		if (collision.gameObject.TryGetComponent<Player>(out Player player) && GetComponentInParent<Player>() == null)
		{
			player.TakeDamage(_damage);

			Destroy(gameObject);

			return;
		}

		if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
		{
			Destroy(gameObject);

			Destroy(collision.gameObject);
		}
	}
}
