using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------- To-Do ---------------
//-------------------------------------

// -- Main Task --
// ----- Fix Collision, Slowdown movement

// - Animations
// --- Attack
// --- Moving 
// --- dodging? 
// --- dieing

// - Player Actions
// --- Attack
// --- Dodge?
// --- Die
// --- Move -Part Implimented

public class PlayerScript : MonoBehaviour 
{
	//------ Player Components ------
	static Rigidbody2D m_PlayerRB;	//Holds the players RigidBody2D
	SpriteRenderer m_PlayerSR;  	//Holds the Players SpriteRenderer

	//------ Player Vars ------
	int m_PlayerHealth; //Holds the player Health (Set in Inspector/ Awake())
	int m_PlayerLives;  //Holds the player Lives (likely set to 3 by Default in Inspector/Awake())
	int m_PlayerDamage; //Holds the Attack Value for the player (likely set in inspector/ Awake())

	static public Vector3 pos; // Current position If the player (public so it canbe accessed by enemies)

	//------ Player Bools ------
	// ----- Actions
	bool m_Attack = false;   	//Determins if the player is attacking 
	bool m_Moving = false; 	    //Determins if the player is moving 
	bool m_Attacking = false;   //Determins if the player has been damaged

	// ----- Game States
	bool m_GameOver = false;	//Should possibly be in GameManager Script

	// ---- PlayerVars ----
	public float speed;			// Player movement speed (Set in the inpector)
	public int Health = 3;		// Playersstarting health (Should probably be an inpector element later)


	float Timer;

	// Use this for initialization
	void Start () 
	{
		m_PlayerRB = GetComponent<Rigidbody2D> (); // Cache and set m_PlayerRB to the ObjectsRidigbody2D
	}

	//Updatefunction for physics
	void FixedUpdate()
	{
		Moving(); // Calls the movment code
	}

	// Update is called once per frame
	void Update () 
	{
		pos = m_PlayerRB.transform.position; //Set the Iitial RB Position for enemy follows --!! Change it to a target Transform later !!--
	}

	// Movemet function for player movement
	void Moving()
	{
		//character will move a set amount without using physics
		if (Input.GetKey (KeyCode.W)) 
		{
			Vector3 CurrentPos = m_PlayerRB.transform.position; //Assign CurrentPos to the players RB Position
			CurrentPos.y += speed * Time.deltaTime;				//Apply Movement
			CurrentPos.z = -1;									//Set the Z level --!! Find out why it resets to 0 Later !!--
			m_PlayerRB.transform.position = CurrentPos;			//Set the nw modyfied CurrentPos to the RB position

		}
		if (Input.GetKey (KeyCode.A)) 
		{
			Vector3 CurrentPos = m_PlayerRB.transform.position; //Assign CurrentPos to the players RB Position
			CurrentPos.x -= speed * Time.deltaTime;				//Apply Movement
			CurrentPos.z = -1;									//Set the Z level --!! Find out why it resets to 0 Later !!--
			m_PlayerRB.transform.position = CurrentPos;			//Set the nw modyfied CurrentPos to the RB position

		}
		if (Input.GetKey (KeyCode.S)) 
		{
			Vector3 CurrentPos = m_PlayerRB.transform.position; //Assign CurrentPos to the players RB Position
			CurrentPos.y -= speed * Time.deltaTime;				//Apply Movement
			CurrentPos.z = -1;									//Set the Z level --!! Find out why it resets to 0 Later !!--
			m_PlayerRB.transform.position = CurrentPos;			//Set the nw modyfied CurrentPos to the RB position
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			Vector3 CurrentPos = m_PlayerRB.transform.position; //Assign CurrentPos to the players RB Position
			CurrentPos.x += speed * Time.deltaTime;				//Apply Movement
			CurrentPos.z = -1;									//Set the Z level --!! Find out why it resets to 0 Later !!--
			m_PlayerRB.transform.position = CurrentPos;			//Set the nw modyfied CurrentPos to the RB position
		}

		//Attack Button
		if (Input.GetKey (KeyCode.K)) 
		{
			m_Attacking = true;              //Attacking = true to stop knockback when NOT taking damage 
			Timer = 0.2f;					 //Timer for damage(should base this on an animation later)
		}

		if(m_Attacking == true)
		{
			Timer -= Time.deltaTime;         //Timer -= 1 by time . delta time (1second over many frames)
			if(Timer <= 0)
			{
				m_Attacking = false; 		 //Attacking = false now damage can continue to be taken 
			}
		}
	}

	void TakenDamage()
	{
		//Vector3 CurrentPos = m_PlayerRB.transform.position; //Assign CurrentPos to the players RB Position
		//CurrentPos.y += -(speed*10) * Time.deltaTime;       //Apply the knock back
		//CurrentPos.z = -1;									//Set the Z level --!! Find out why it resets to 0 Later !!--
		//m_PlayerRB.transform.position = CurrentPos;			//Set the nw modyfied CurrentPos to the RB position


		if (Health > 0) 
		{
			Health--; //Reduce players health by 1 per hit	
		} 
		else 
		{
			m_GameOver = true; //Game over = true if health is 0 or less
		}
	}

	//Getter for positon for things that follow the player 
	public static Vector3 GetPosition ()
	{
		return pos;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (m_Attacking == false) 
		{
			if (col.gameObject.tag == "Enemy") 
			{
				TakenDamage ();
				Vector2 HitDir = col.gameObject.transform.position;
				Vector2 extentsize = transform.localScale / 3.0f;

				Vector2 centre = transform.position;
				Vector2 minExtent = centre - extentsize;
				Vector2 maxExtent = centre + extentsize;

				float xdir = 0;
				if (HitDir.x > maxExtent.x)
					xdir = 1;
				else if (HitDir.x < minExtent.x)
					xdir = -1;
				else
					xdir = 0;

				float ydir = 0;
				if (HitDir.y > maxExtent.y)
					ydir = 1;
				else if (HitDir.y < minExtent.y)
					ydir = -1;
				else
					ydir = 0;

				Vector3 newpos = m_PlayerRB.transform.position;
				//newpos.x -= xdir * (speed*5) * Time.deltaTime;
				//newpos.y -= ydir * (speed*5) * Time.deltaTime;

				newpos.x -= xdir + 2 * speed * Time.deltaTime;
				newpos.y -= ydir + 2 * speed * Time.deltaTime;
				m_PlayerRB.transform.position = newpos;
			}
		}
	}
}
