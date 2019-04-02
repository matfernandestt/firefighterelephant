using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Civilian : MonoBehaviour
{
	public LayerMask CollisionMask;
	public float SkinWidth = .015f;
	public int HorizontalRayCount = 4;
	public int VerticalRayCount = 4;
	public float MoveSpeed;

	private float gravity = -20f;
	private float horizontalRaySpacing;
	private float verticalRaySpacing;
	private BoxCollider2D collider;
	private RaycastOrigins raycastOrigins;
	private Vector3 velocity;

	private void Start()
	{
		collider = GetComponent<BoxCollider2D>();

		CalculateRaySpacing();
	}

	private void Update()
	{
		//velocity.x = input.x * MoveSpeed;
		//velocity.y += gravity * Time.deltaTime;
		//Move(velocity * Time.deltaTime);
	}

	public void Move(Vector3 velocity)
	{
		UpdateRaycastOrigins();

		if (velocity.x != 0)
		{
			HorizontalCollisions(ref velocity);
		}

		if (velocity.y != 0)
		{
			VerticalCollisions(ref velocity);
		}

		transform.Translate(velocity);
	}

	public void HorizontalCollisions(ref Vector3 velocity)
	{
		float directionX = Mathf.Sign(velocity.x);
		float rayLength = Mathf.Abs(velocity.x) + SkinWidth;

		for (int i = 0; i < HorizontalRayCount; i++)
		{
			Vector2 rayOrigin = (directionX == -1f) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);

			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, CollisionMask);

			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

			if (hit)
			{
				velocity.x = (hit.distance - SkinWidth) * directionX;
				rayLength = hit.distance;
			}
		}
	}

	public void VerticalCollisions(ref Vector3 velocity)
	{
		float directionY = Mathf.Sign(velocity.y);
		float rayLength = Mathf.Abs(velocity.y) + SkinWidth;

		for (int i = 0; i < VerticalRayCount; i++)
		{
			Vector2 rayOrigin = (directionY == -1f) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i * velocity.x);

			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, CollisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

			if (hit)
			{
				velocity.y = (hit.distance - SkinWidth) * directionY;
				rayLength = hit.distance;
			}
		}
	}

	private void UpdateRaycastOrigins()
	{
		Bounds bounds = collider.bounds;
		bounds.Expand(SkinWidth * -2); //Negativo para que a expansão seja para dentro.

		raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
		raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
	}

	private void CalculateRaySpacing()
	{
		Bounds bounds = collider.bounds;
		bounds.Expand(SkinWidth * -2);

		HorizontalRayCount = Mathf.Clamp(HorizontalRayCount, 2, int.MaxValue);
		VerticalRayCount = Mathf.Clamp(VerticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (HorizontalRayCount - 1); //-1 porque ele conta apenas os espaços entre os raios.
		verticalRaySpacing = bounds.size.x / (VerticalRayCount - 1);
	}

	private struct RaycastOrigins
	{
		public Vector2 topLeft, topRight, bottomLeft, bottomRight;
	}
}
