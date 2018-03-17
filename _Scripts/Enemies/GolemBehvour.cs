using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBehvour : MonoBehaviour 
{
	// ===========================
	// ---- To - Do ----
	//------------------

	//Attacks 
	// --- short
	// --- long
	//Movements 
	// -- Figure 8
	// -- Smash

	//States

	// ---- Boss States ----
	bool m_Idle; 			// Asleep?
	bool m_Moving;
	bool m_Attacking; 		// State to pick an attack and initite it
	bool m_Vulnerable;		// Vecoms vunnerable after attack
	bool m_Invlunerable; 	//Any time before an attack 

	// Transition- idle-> Invlunerable/ Attacking -> Vulnerable -> back to attacking 
	public Transform m_target;	 //Target for following/ Smashing(player)
	public float m_speed = 2f; 	 //Movment speed

	public Vector2 m_TargetPos; //Target for following/ Smashing(player)

	// ---- Boss Cmponents ----
	Rigidbody2D m_BossBody;			//Rigid Body decloration to store the sprite renderer
	SpriteRenderer m_spriteRender;	//Sprite renderer decloration tostore the sprite renderer

	public Sprite m_DefaultSprite;   	//Default sprite for animating attacks --!!Shouldstic kin the animator later !!--
	public Sprite m_SmashSprite;		//Smashing hand sprite for animating attacks --!!Shouldstic kin the animator later !!--
	public Sprite m_VulnerableSprite;   //Vulnerable hand sprite for animating attacks --!!Shouldstic kin the animator later !!--

	// Use this for initialization
	void Start () 
	{
		m_Moving = true; 								  //first state currently 
		m_BossBody = GetComponent<Rigidbody2D> (); 		  //Cache the Rigidbody component
		m_spriteRender = GetComponent<SpriteRenderer> (); //Cache the Rigidbody component
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check if the handiswithing a given distance betweenthe player and if not close move towards the player
		if (Vector2.Distance (m_BossBody.transform.position, m_target.position) > 0.5f) 
		{
			Vector3 CurrentPos = m_BossBody.transform.position; 														   //Set Current Posto the RB position of the hand 
			CurrentPos = Vector3.MoveTowards (m_BossBody.transform.position, m_target.position, m_speed * Time.deltaTime); //Move towards the player
			m_BossBody.transform.position = CurrentPos; 																   //Apply the new calculatedpositon to the golemhand
		}

		//Set the Sprite to a smash sprite and activates the players apply damagefuntion fora knockback effect
		if (Vector2.Distance (m_BossBody.transform.position, m_target.position) < 1f) 
		{
			m_spriteRender.sprite = m_SmashSprite; //Set theCurrent Sprite to the Smash sprite to apply damage --!! Should do thiswith animator later !!--
			PlayerScript.TakenDamage ();		   //Actvates the players take damage function for aknock back effect --!! Should do this in player sript later !!--
		}
	}

	// --!! Will be used to pick which action totake !!--
	void PickAction()
	{

	}

	// Attack method 
	void Attacks()
	{
		//Smash


		//---- Follow ---
		/// -- Follow player
		/// --move down whenabove player y within 10 units?
		/// move down to players x 
		/// deal damage
		/// become vulnerable

		//BossBody.position
		Vector2 currentPos = m_BossBody.position;       //The current golem position
		Vector2 TargetPos = PlayerScript.GetPosition(); //Get the player position

	}
}
