using UnityEngine;
using System.Collections;

public class CrosshairScript : MonoBehaviour {

	public Transform m_crosshair;

	bool m_foundHit = false;
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit = new RaycastHit();

		m_foundHit = Physics.Raycast(transform.position, transform.forward, out hit, 100);

		if(m_foundHit && hit.transform.gameObject.layer != LayerMask.NameToLayer("Level"))
		{
			//m_crosshair.position = hit.point;
			Debug.DrawLine(transform.position, hit.point, Color.red);
			Debug.Log(hit.transform.name);
		}
		else {
			Vector3 endpoint = new Vector3();
			endpoint = transform.position + transform.forward * 100;
			Debug.DrawLine(transform.position, endpoint, Color.green);
		}
	}
}
