using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour 
{
	// ---- Sword Vars ----
	int m_Damage = 1;

	static public Rigidbody2D SwordBod;

	void Start ()
	{
		SwordBod = GetComponent < Rigidbody2D> ();
	}
	/*int OnCollisonEnter(Collision other)
	{
		//Get Enemy Script/ apply damage to work with all enemies 
		int health = GetComponent<EnemyStats> ().m_CurHealth;
		health--;
		return health;
	}*/



}
