/*
Script Responsible for updating player position and orientation
Movement: Left,Right,Jump 
*/
using UnityEngine;
using UnityEngine.Events;

public class Controller2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f; // force added to jump						
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; // movement smoothing
	[SerializeField] private bool m_AirControl = false; // if player can steer while jumping
	[SerializeField] private LayerMask m_WhatIsGround; // game object to find ground
	[SerializeField] private Transform m_GroundCheck; // check for gounded player
	
	private bool m_Grounded;            // If player is grounded
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = false;  // Direction character is facing
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		// component respondisble for character movement
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		// create new event to know when the player is on the ground
		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}


	public void Move(float move, bool jump)
	{

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{	
			// Move character by tagetVelovity and smooth motion
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If input right but facing left
			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			// If input left but facing right
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		// Player Jumps
		if (m_Grounded && jump)
		{
			// Add vertical force
			m_Grounded = true;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			
		}
	}


	private void Flip()
	{
		// reverse where the charcter is facing
		m_FacingRight = !m_FacingRight;

		// Multiply the scale by -1 to flip the image
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

 
