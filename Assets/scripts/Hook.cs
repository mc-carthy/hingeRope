using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {

	public Vector2 hookPoint;

	[SerializeField]
	private float hookSpeed = 10;

	private void Update () {
		transform.position = Vector2.MoveTowards (transform.position, hookPoint, hookSpeed * Time.deltaTime);
	}

}
