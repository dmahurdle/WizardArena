using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Magic bullet;
	float coolDownTimer;

	// Use this for initialization
	void Start () {
		coolDownTimer = bullet.coolDown;
		
		Material m = this.gameObject.renderer.material;
		
		bullet.gameObject.renderer.material = this.gameObject.renderer.material;
		

	}

	public void Fire(Vector2 dir)
	{
		if (coolDownTimer >= bullet.coolDown) {
						Magic clone = (Magic)Instantiate (bullet, transform.position + (new Vector3 (dir.normalized.x, dir.normalized.y, 0.0f) * 0.46f), transform.rotation);
						clone.gameObject.renderer.material = this.gameObject.renderer.material;
			
			
						clone.velocity = dir.normalized;
						clone.characterNumber=((Controller)GetComponent("Controller")).getCharacter();
						coolDownTimer=0;
				}
	}
	
	// Update is called once per frame
	void Update ()
	{
		coolDownTimer += Time.deltaTime;
		//Fire (new Vector2 (0.0f, 1.0f));
	}
}