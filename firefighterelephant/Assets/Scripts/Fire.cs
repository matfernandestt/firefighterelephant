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

	public ParticleSystem FireParent;
	public ParticleSystem Fire0;
	public ParticleSystem Fire1;
	public ParticleSystem Smoke0;
	public ParticleSystem Smoke1;

	[HideInInspector] public bool Extinguishing;

	private float counter;

	public void Explode()
	{
		Instantiate(ExplosionParticle, transform.position, ExplosionParticle.transform.rotation, transform);
	}

	private void Update()
	{
		if (Extinguishing)
		{
			counter += Time.deltaTime;
			Extinguishing = false;
		}

		if (counter >= 1f && Fire0.main.loop)
		{
			var main = Fire0.main;
			main.loop = false;
		}
		else if (counter >= 2f && Fire1.main.loop)
		{
			var main = Fire1.main;
			main.loop = false;

			var mainSmoke0 = Smoke0.main;
			mainSmoke0.loop = false;

			var mainSmoke1 = Smoke1.main;
			mainSmoke1.loop = false;
		}
		else if (!Smoke1.IsAlive())
		{
			Destroy(gameObject);
		}
	}
}
