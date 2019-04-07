using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
	public static Animator Animator;

	private void Start()
	{
		Animator = GetComponent<Animator>();
	}

	public void OnCompleteFade()
	{
		Animator.SetTrigger("FadeIn");
	}

	public static void Fade()
	{
		Animator.SetTrigger("FadeOut");
	}
}
