using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoManager<T> : MonoBehaviour where T : MonoManager<T>
{
	private static T instance;

	public static T Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<T> ();
			}
			return instance;
		}
	}
}