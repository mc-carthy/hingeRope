using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hook : MonoBehaviour {

	public Vector2 hookPoint;
	public GameObject lastNode;

	[SerializeField]
	private GameObject nodePrefab;
	[SerializeField]
	private Player player;
	[SerializeField]
	private LineRenderer linRen;
	[SerializeField]
	private float hookSpeed = 10;
	[SerializeField]
	private float nodeDis = 2;
	private bool isHookLanded = false;
	private List<GameObject> nodes = new List<GameObject>();
	private int vertexCount = 2;

	private void Start () {
		linRen = GetComponent<LineRenderer> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		lastNode = transform.gameObject;
		nodes.Add (transform.gameObject);
	}

	private void Update () {
		transform.position = Vector2.MoveTowards (transform.position, hookPoint, hookSpeed * Time.deltaTime);

		if (transform.position != new Vector3 (hookPoint.x, hookPoint.y, transform.position.z)) {
			if (Vector2.Distance (player.transform.position, lastNode.transform.position) > nodeDis) {
				CreateNode ();
			}
		} else if (!isHookLanded) {
			isHookLanded = true;
			lastNode.GetComponent<HingeJoint2D> ().connectedBody = player.GetComponent<Rigidbody2D> ();
		}
		RenderLine ();
	}

	private void CreateNode () {
		Vector2 nodePos = player.transform.position - lastNode.transform.position;
		nodePos.Normalize ();
		nodePos *= nodeDis;
		nodePos += (Vector2)lastNode.transform.position;

		GameObject newNode = Instantiate (nodePrefab, nodePos, Quaternion.identity) as GameObject;

		newNode.transform.SetParent (transform);

		lastNode.GetComponent<HingeJoint2D> ().connectedBody = newNode.GetComponent<Rigidbody2D> ();

		lastNode = newNode;
		nodes.Add (lastNode);
		vertexCount++;
	}

	private void RenderLine () {
		linRen.SetVertexCount (vertexCount);

		for (int i = 0; i < nodes.Count; i++) {
			linRen.SetPosition (i, nodes [i].transform.position);
		}

		linRen.SetPosition (nodes.Count, player.transform.position);
	}
}
