using UnityEngine;
using System.Collections;

public class MechaTorso : MonoBehaviour {

	public float m_lookFactor = 1.1f;
	public float m_distance = 100f;

	// Update is called once per frame
	void Update () {

		Vector3 m_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_distance);
		m_pos = Camera.main.ScreenToWorldPoint(m_pos);

		Quaternion m_rotationGun = Quaternion.LookRotation(m_pos - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, m_rotationGun, Time.deltaTime * m_lookFactor);
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);

	}
}
