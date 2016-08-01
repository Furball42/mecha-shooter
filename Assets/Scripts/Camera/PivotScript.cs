using UnityEngine;
using System.Collections;

public class PivotScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float aimupdown = Input.GetAxis ("Mouse Y");

		transform.Rotate (Vector3.right * -aimupdown);
	}
}
