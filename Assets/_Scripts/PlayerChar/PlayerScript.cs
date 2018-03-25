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
	static bool m_DamageTaken = false; //Determins if the player has been damaged

	// ----- Game States
	bool m_GameOver = false;	//Should possibly be in GameManager Script

	// ---- PlayerVars ----
	public float speed;			// Player movement speed (Set in the inpector)
	public int Health = 3;				// Playersstarting health (Should probably be an inpector element later)

	// Use this for initialization
	void Start () 
	{
		m_PlayerRB = GetComponent<Rigidbody2D> (); // Cache and set m_PlayerRB to the ObjectsRidigbody2D
	}

	//Updatefunction for physics
	void FixedUpdate()
	{
		Moving(); // Calls the movment code

		//Apply a knock back when damage taken --!! Later on: Make it more robust and move in the oposit direction of the damage !!--
		if (m_DamageTaken == true) 
		{
			Vector3 CurrentPos = m_PlayerRB.transform.position; //Assign CurrentPos to the players RB Position
			CurrentPos.y += -(speed*10) * Time.deltaTime;       //Apply the knock back
			CurrentPos.z = -1;									//Set the Z level --!! Find out why it resets to 0 Later !!--
			m_PlayerRB.transform.position = CurrentPos;			//Set the nw modyfied CurrentPos to the RB position

			if (Health > 0) 
			{
				Health--; //Reduce players health by 1 per hit	
			} 
			else 
			{
				m_GameOver = true; //Game over = true if health is 0 or less
			}
			m_DamageTaken = false;								//Set damage to falsereadyfor the next hit 
		}
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

		//Attack button
		if (Input.GetKey (KeyCode.L)) 
		{
			Rigidbody2D sword = SwordDamage.SwordBod;
			Vector3 SwordPlace = sword.position;
			SwordPlace.z = -1;
			SwordPlace = m_PlayerRB.position;
			SwordPlace.y += 5f;
			sword.transform.position = SwordPlace;
		}
	}

	public static void TakenDamage()
	{
		m_DamageTaken = true; //Set to true to activate damage code --!! Wont need when damage is calculated in this script !!--
	}

	//Getter for positon for things that follow the player 
	public static Vector3 GetPosition ()
	{
		return pos;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") 
		{
			Health--;
		}
	}
}
