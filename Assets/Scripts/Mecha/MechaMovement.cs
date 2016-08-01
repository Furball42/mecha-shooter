using UnityEngine;

public class MechaMovement : MonoBehaviour {
      

	//NOTES
	// Need to add:
	// - Jump cool down delay


	public float m_acceleration = 2.0f;
	public float m_TurnSpeed = 180f;
	public float m_maxSpeed = 25f;
	public float m_maxReverse = 10f;
	public float m_JumpHeight = 10f;
	public bool m_IsJumpCapable = true;

	private string m_MovementAxisName;     
	private string m_TurnAxisName;         
	private Rigidbody m_Rigidbody;         
	private float m_MovementInputValue;    
	private float m_TurnInputValue;
	private bool m_Collided;
	private float m_currentSpeed;
	private bool m_IsGrounded;
	private bool m_IsJumping;


	void Awake() {
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Collided = false;
		m_IsGrounded = true;
		m_IsJumping = false;
	}

	// Use this for initialization
	void Start () {
		m_MovementAxisName = "Vertical";
		m_TurnAxisName = "Horizontal";
		m_currentSpeed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

	}

	void FixedUpdate() {

		CheckIfGrounded();

		if(Input.GetKeyDown("space") && m_IsJumpCapable) {
			Jump();
		}

		if(!m_Collided) {
			Move();
		}

		Turn();
	}

	void OnCollisionEnter(Collision collision) {

	}

	void OnTriggerEnter(Collider collider) {	
		//check if collider is on level layer
		if (collider.gameObject.layer == LayerMask.NameToLayer("Level") && !m_Collided)
		{
			DeadStop();
			m_Collided = true;
		}			
	}

	void OnTriggerExit() {		
		m_Collided = false;
	}

	void Move() {

		Vector3 movement = transform.forward * m_MovementInputValue * m_currentSpeed * Time.deltaTime;
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

		if (m_MovementInputValue > 0) //forward
		{			
			m_currentSpeed += m_acceleration;

			if (m_currentSpeed > m_maxSpeed)
				m_currentSpeed = m_maxSpeed;
		}		
		else if (m_MovementInputValue < 0) { //backward

			m_currentSpeed += m_acceleration;

			if (m_currentSpeed > m_maxReverse)
				m_currentSpeed = m_maxReverse;
		}
		else if (m_MovementInputValue == 0) { //slow down - stop

			m_currentSpeed = 0;

			if (m_currentSpeed < 0)
				m_currentSpeed = 0;
		}
	}

	void Turn() {
		float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
	}

	void Jump () {

		if (m_IsGrounded) {
			m_Rigidbody.velocity += new Vector3(0, m_JumpHeight, 0); //+ (1.0f * transform.forward);
			m_IsJumping = true;
		}			
	}

	void DeadStop() {

		Vector3 movement = transform.forward * m_MovementInputValue * m_currentSpeed * Time.deltaTime;
		m_Rigidbody.MovePosition(m_Rigidbody.position - movement);

		m_Rigidbody.velocity = Vector3.zero;
		m_Rigidbody.angularVelocity = Vector3.zero;
		m_Rigidbody.Sleep();
	}

	void CheckIfGrounded() {

		RaycastHit hit = new RaycastHit();
		if(Physics.Raycast(transform.position, -Vector3.up, out hit, 2)) {
			if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Level"))
			{
				Debug.DrawLine(transform.position, hit.point, Color.cyan);
				m_IsGrounded = true;
			}
		}
		else {
			m_IsGrounded = false;
			Debug.DrawLine(transform.position, hit.point, Color.yellow);
		}

		if (m_IsJumping && m_IsGrounded)
		{
			m_IsJumping = false;
		}
	}
}
