using UnityEngine;
using System.Collections;

public class Magic : MonoBehaviour {

	public float timeOut;
	public Vector3 velocity;
	public float Speed;
	public float coolDown;
	public int characterNumber;
	float currentTime;

	// Use this for initialization
	void Start () {
		currentTime = 0;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.name == "Character Prefab")
		{
			print ("Character Dies");
		}
		Character guy = (Character)collision.collider.gameObject.GetComponent("Character");
		if(guy != null && !guy.getInvincible())
		{
			if(characterNumber == guy.getId())
			{
				guy.alterNumKills(-1);
			}
			else
			{
				GameManager.getPlayer(characterNumber).alterNumKills(1);
				guy.Kill();
			}
			
		}
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		this.rigidbody.transform.Translate ((velocity*Speed) * Time.deltaTime);

		currentTime += Time.deltaTime;

		if(timeOut<=currentTime)
		{
			Destroy (this.gameObject);
		}
	}
}
