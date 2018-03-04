using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public const string PICKUP_TAG = "Pickup";

	public float speed = 100;

	private int collected = 0;
	private int total	  = 0;

//	public Rigidbody rigidbody;

	public Rigidbody rb;

	public GUIText countText;
	public GUIText winText;

	public GameObject firework;

	void Start() {
		SetCountText ();
		winText.text = "";
		firework.SetActive (false);

		GameObject[] pickups = GameObject.FindGameObjectsWithTag (PICKUP_TAG);
		total = pickups.Length;

		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);

		rb.AddForce (movement * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == PICKUP_TAG) {
			other.gameObject.SetActive (false);
			collected++;
			SetCountText();
		}
	}

	void SetCountText() {
		countText.text = "Count: " + collected.ToString ();

		if (collected >= total && total > 0) {
			winText.text = "You win!";
			firework.SetActive (true);
		}
	}
}