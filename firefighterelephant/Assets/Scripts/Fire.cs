using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireType
{
	A, B, C
}

public class Fire : MonoBehaviour
{
	public FireType Type;
	public GameObject ExplosionParticle;

	public void Explode()
	{
		Instantiate(ExplosionParticle, transform.position, ExplosionParticle.transform.rotation, transform);
	}
}
