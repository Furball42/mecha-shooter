using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	public Vector3 m_crosshair;

	private GameObject m_pivotpoint;

	// Use this for initialization
	void Start () {
		m_pivotpoint = GameObject.Find ("PivotPoint");
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Physics.Raycast (transform.position, transform.rotation * Vector3.forward, out hit);
		if (hit.transform != null)
		{
			m_crosshair = hit.point;
		}
	}

	void LateUpdate() {
		float offsetBack = 15;

		transform.rotation = (m_pivotpoint.transform.rotation);
		transform.position = m_pivotpoint.transform.position + offsetBack * - transform.forward;
	}
}
