using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	public GameObject characterModel;
	private Controller controller;
	private int numKills, playerId;
	private bool invincible;
	private static int currentId = 0;
	
	
	public int getNumKills()
	{
		return numKills;
	}
	
	public void setInvincible(bool val)
	{
		invincible = val;
	}
	
	public bool getInvincible()
	{
		return invincible;
	}
	
	public void alterNumKills(int amount)
	{
		numKills += amount;
	}
	// Use this for initialization
	void Start () 
	{
		invincible = false;
		playerId = currentId++;
		controller = (Controller)this.gameObject.GetComponent("Controller");
		this.gameObject.name = "player" + playerId;
		controller.setCharacter(playerId);
		float x = (Random.value * 10) - 5;
		float y = (Random.value * 10) - 5;
		float z = (Random.value * 10) - 5;
		((Projectile)this.gameObject.GetComponent("Projectile")).bullet.characterNumber = playerId;
		GameObject character = (GameObject)Instantiate(characterModel, new Vector3(x, y, z), Quaternion.identity);
	}
	
	public int getId()
	{
		return playerId;
	}
	
	public void Restart()
	{
		numKills = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Kill()
	{
		float x = 0;
		float y = 1;
		float z = 0;

		this.gameObject.transform.position = new Vector3 (x, y, z);
		this.rigidbody.isKinematic = true;
		this.rigidbody.isKinematic = false;
	}
}

//Drew was here.