using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField]
	private Hook hookPrefab;

	private Hook currentHook;

	private void Update () {
		CreateHookAtPoint ();
	}

	private void CreateHookAtPoint () {
		if (Input.GetMouseButtonDown (0)) {
			Vector2 aimPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			currentHook = Instantiate (hookPrefab, transform.position, Quaternion.identity) as Hook;
			currentHook.hookPoint = aimPoint;
		}
	}
}
