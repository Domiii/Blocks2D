using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
	public List<Collider2D> objects;

	void Awake() {
		objects = new List<Collider2D> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		objects.Add (other);
	}

	void OnTriggerExit2D(Collider2D other) {
		objects.Remove (other);
	}
}
