using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField]
	private float force = 500;
	[SerializeField]
	private Hook hookPrefab;
	private Hook currentHook;
	private Rigidbody2D rb;
	private bool isRopeActive = false;

	private void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = Vector2.up * 10;
	}

	private void Update () {
		CreateHookAtPoint ();
		MovePlayer ();
	}

	private void CreateHookAtPoint () {
		if (Input.GetMouseButtonDown (0)) {
			if (isRopeActive) {
				Destroy (currentHook.gameObject);
				isRopeActive = false;
			} else {
				Vector2 aimPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				currentHook = Instantiate (hookPrefab, transform.position, Quaternion.identity) as Hook;
				currentHook.hookPoint = aimPoint;
				isRopeActive = true;
			}
		}
	}

	private void MovePlayer () {
		rb.AddForce (Vector2.right * Input.GetAxisRaw ("Horizontal") * force * Time.deltaTime);
	}
}
