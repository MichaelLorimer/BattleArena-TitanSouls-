using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour 
{
	// target
	//speed
	//on contact enter with sword 

	// ---- Enemy Vars ----
	public float m_Speed; //Set in Inspector for enemy Variety
	public int m_Health;  //Set in Inspector for enemy Variety
	Vector2 m_Target;     //Find Player when awake 

	// ---- Enemy Components ----
	Rigidbody2D m_EnemyBody;     //Stores the Enemy rigid Body
	SpriteRenderer m_SpriteRend; //Stores the Enemy Sprite renderer
	Animator m_Animater;		 //Stored the Enemy Animator

	// -- Animations/ Sprites --
	Sprite m_DefaultSprite; //Stores the default Sprite
	Sprite m_DamagesSprite; //Stored the damaged Sprite
	Sprite m_AttackSprite;  //Shows the attacking sprite 

	Animation m_Walking;    //Stores Walking animation
	Animation m_Attacking;  //Stores Attacking animation
	Animation m_Death;      //Stored Death Animation

	// Use this for initialization
	void Start () 
	{
		// - Cache Components -
		m_EnemyBody = GetComponent<Rigidbody2D> ();     //Assign RigidBody Component
		m_SpriteRend = GetComponent<SpriteRenderer> (); //Assign SpriteRenderer Component
		m_Animater = GetComponent<Animator>();			//Assign Animator Component
	}
	
	// Update is called once per frame
	void Update () 
	{
		// -- Change this to line of sight/ Distance to player for awake 
		m_Target = GameObject.FindWithTag ("Player").GetComponent<Rigidbody2D> ().position; //Find the players Position as the target
		Vector3 CurrentPos = m_EnemyBody.transform.position;								//Set CurrentPos to = the RigidBody2D.position
		CurrentPos = Vector3.MoveTowards (CurrentPos, m_Target, m_Speed * Time.deltaTime);  
		m_EnemyBody.transform.position = CurrentPos;										//Set RigidBody2D.position to CurrentPos

		//If Health = 0 Destroy object
		if (m_Health <= 0) 
		{
			//PlayDeath Anim
			Destroy(gameObject); //Destroys THIS gameObject
			//Prep for any drops
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		//If Collision with "Sword" - Change Sword to whatever will be doing the killing
		if (col.gameObject.name == "Sword") 
		{
			m_Health--; //Reduce health by 1
		}


		//There has to be a Better way to impliment this Knock Back effect
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

		Vector3 newpos = m_EnemyBody.transform.position;
		newpos.x -= xdir + 2 * m_Speed * Time.deltaTime;
		newpos.y -= ydir + 2 * m_Speed * Time.deltaTime;
		m_EnemyBody.transform.position = newpos;
	}
}
