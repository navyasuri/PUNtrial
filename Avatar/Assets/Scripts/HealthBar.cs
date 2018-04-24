using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon;

public class HealthBar : Photon.MonoBehaviour {

	public GameObject player;
	public Vector3 greenHealthBar;
	public float greenBarWidth;

	[SerializeField]
	public float playerHP;

	void Start() {
		greenHealthBar = GameObject.Find ("Healthy").GetComponent<RectTransform> ().localScale;
	}

	void Update () {
		if (player == null) {
			Destroy (this.gameObject);
			return;
		}
		playerHP = player.GetComponent<PlayerBehavior>().health;
		greenHealthBar.x = playerHP / 100;

		if (greenHealthBar.x <= 0f) {
			greenHealthBar.x = 0;
		}

		GameObject.Find ("Healthy").GetComponent<RectTransform> ().localScale = new Vector3 (greenHealthBar.x, greenHealthBar.y, greenHealthBar.z);
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (playerHP);
		} else {
			playerHP = (float)stream.ReceiveNext ();
		}
	}
	
}
