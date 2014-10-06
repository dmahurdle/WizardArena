using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{


    public float speed = 10.0f;
    float distToGround;
    public int characterNumber = 0;
    public float jumpPower = 15;
    float wallJump = 0;
    bool isWallJumping = false;
    bool isJumping = false;
    string hmove;
    string vmove;
    string haim;
    string vaim;
    string firetrigger;
    string jumpbutton;
    float hAimMod = 1.0f;
    float vAimMod = 1.0f;

    // Use this for initialization
    void Start()
    {
        distToGround = collider.bounds.extents.y;

       setupController();
    }
    
    void setupController()
    {
		hmove = "Horizontal" + characterNumber;
		vmove = "Vertical" + characterNumber;
		
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{
			haim = "VAimM" + characterNumber;
			vaim = "VAimW" + characterNumber;
			hAimMod = -1.0f;
			firetrigger = "Fire" + characterNumber;
			jumpbutton = "JumpW" + characterNumber;
			vAimMod = -1.0f;
		}
		else
		{
			hAimMod = 1.0f;
			firetrigger = "Fire" + characterNumber;
			haim = "HAimM" + characterNumber;
			jumpbutton = "JumpM" + characterNumber;
			vAimMod = 1.0f;
			vaim = "VAimM" + characterNumber;
		}
    }
    
    public void setCharacter(int id)
    {
    	characterNumber = id;
    	setupController();
    }
    
    public int getCharacter()
    {
    	return characterNumber;
    }

    bool IsGrounded(float grace)
    {
        return (Physics.Raycast(new Vector3(transform.position.x - distToGround, transform.position.y, transform.position.z), -Vector3.up, distToGround + grace) || Physics.Raycast(new Vector3(transform.position.x + distToGround, transform.position.y, transform.position.z), -Vector3.up, distToGround + grace));
    }

    bool AllXAreEqual(Collision col)
    {
        //bool foundDisparity = false;
        float x = col.contacts [0].point.x;
        for (int i =1; i < col.contacts.Length; i ++)
        {
            if (Mathf.Abs(col.contacts [i].point.x - x) > float.Epsilon)
            {
                //Debug.Log("False");
                return false;
            }
        }
        //Debug.Log("True");
        return true;
    }

    void OnCollisionEnter(Collision col)
    {
        Vector3 relativePosition = transform.InverseTransformPoint(col.contacts [0].point);
        //Debug.Log (col.contacts.Length);
        //Debug.Log (col.contacts[0].point + " , " + col.contacts[1].point);
        //Actual position is probably... the greatest value in relativePosition... Maybe.

        if (AllXAreEqual(col))
        {
            //The wall is to the right or the left and not above or below. Do a bounce thing to wall jump.
            //wallJump = relativePosition.x > 0? -1 : 1;
            isWallJumping = true;
            isJumping = true;
        } else
        {
            wallJump = 0;
            isWallJumping = false;
            isJumping = false;
        }
    }

    bool fired = false;
    //Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis(hmove);
        //Debug.Log ("Wall left: " + IsWallLeft() + " , " + "Wall right: " + IsWallRight() + " , " + "jumping : " + isJumping);

        rigidbody.velocity = (new Vector3(speed * hor, rigidbody.velocity.y, 0));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        Vector2 aim = new Vector2(Input.GetAxis(haim) * hAimMod, Input.GetAxis(vaim) * vAimMod);


        float trigger = Input.GetAxis(firetrigger);
        if (aim.sqrMagnitude > 0.2f && !fired && trigger < -0.5f)
        {
            fired = true;
            Debug.Log("Player Fire " + characterNumber);
            ((Projectile)(GetComponent("Projectile"))).Fire(aim);

        }

        if (trigger <= 0.5)
        {
            fired = false;
        }

        if (!isJumping && (Input.GetButton(jumpbutton) || Input.GetAxis(vmove) > 0.5))
        {
            ///Debug.Log(characterNumber);
            isJumping = true;
            rigidbody.velocity += new Vector3(0, jumpPower, 0);
        }
        if (rigidbody.velocity.y > jumpPower)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpPower, rigidbody.velocity.z);
        }

        bool grounded = IsGrounded(0.01f);
        if (!isJumping && !grounded)
        {
            isJumping = true;
        }
        if (grounded)
        {
            isJumping = false;      
        }
        //isJumping = true;
    }
}
