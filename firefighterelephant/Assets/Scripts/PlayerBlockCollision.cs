using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockCollision : MonoBehaviour
{
	public LayerMask BlockLayerMask;

	private CameraController camera;

	private void Start()
	{
		camera = FindObjectOfType<CameraController>();
		StartCoroutine(CheckForBlock());
	}

	private IEnumerator CheckForBlock()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.5f);

			var hit = Physics2D.Raycast(transform.position, transform.up, -2f, BlockLayerMask);

			Debug.DrawRay(transform.position, transform.right * -2f, Color.red);

			if (hit)
			{
				camera.ChangeFocus(hit.transform.parent);
			}
		}
	}
}
