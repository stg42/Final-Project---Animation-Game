using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float	maxSpeed = 10f;
	bool			facingRight = true;

	Animator 		anim;

	bool 			grounded = false;

	public Transform groundCheck;

	float groundRadius = 0.2f;

	public LayerMask whatIsGround;


	public float jumpForce = 800f;






	void Start () {
		anim = GetComponent<Animator> ();
	}
	

	void FixedUpdate () {
	
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);



		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("speed", Mathf.Abs (move));

		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

		if (move > 0 && ! facingRight) 
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
		
	}


	void Update(){
		if(grounded && Input.GetKeyDown(KeyCode.Space)){
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}


		if(grounded && Input.GetKeyDown(KeyCode.V)){
			anim.SetBool("Punch", true);
		}else if(Input.GetKeyUp(KeyCode.V)){
			anim.SetBool("Punch", false);
		}


		if(grounded && Input.GetKeyDown(KeyCode.DownArrow) && anim.GetFloat("speed") == 0f && anim.GetBool("SkateBoard") == false){
			anim.SetBool("Crouch", true);
				maxSpeed = 0;

		}else if(Input.GetKeyUp(KeyCode.DownArrow) && anim.GetBool("SkateBoard") == false){
			anim.SetBool("Crouch", false);
			maxSpeed = 10f;
		}


		if(grounded && Input.GetKeyDown(KeyCode.UpArrow) && anim.GetFloat("speed") == 0f && anim.GetBool("SkateBoard") == false){
			anim.SetBool("LookUp", true);
			maxSpeed = 0;
		}else if(Input.GetKeyUp(KeyCode.UpArrow) && anim.GetBool("SkateBoard") == false){
			anim.SetBool("LookUp", false);
			maxSpeed = 10f;
		}



		if(grounded && Input.GetKeyDown(KeyCode.B) && anim.GetFloat("speed") == 0f){
			anim.SetBool("Block", true);
			maxSpeed = 0;
			
		}else if(Input.GetKeyUp(KeyCode.B)){
			anim.SetBool("Block", false);
			maxSpeed = 10f;
		}


		if(grounded && Input.GetKey(KeyCode.N) && anim.GetBool("Block") == false  && anim.GetBool("LookUp") == false && anim.GetBool("Crouch") == false){
			anim.SetBool("SkateBoard", true);
			maxSpeed = 15f;
			jumpForce = 1000f;


			if(Input.GetKey(KeyCode.DownArrow)){
				anim.SetBool("SkateBoardCrouch", true);
				maxSpeed = 18f;

			}else if(Input.GetKeyUp(KeyCode.DownArrow) ){
				anim.SetBool("SkateBoardCrouch", false);
				anim.SetBool("SkateBoard", false);
				maxSpeed = 15f;
				if(anim.GetFloat("speed") > 0.01f){
					jumpForce = 1200f;
				}
				rigidbody2D.AddForce(new Vector2(0, jumpForce));
			}	
			
		}else if(Input.GetKeyUp(KeyCode.N) && anim.GetBool("Crouch") == false){
			anim.SetBool("SkateBoard", false);
			anim.SetBool("SkateBoardCrouch", false);
			maxSpeed = 10f;
			jumpForce = 800f;
		}





	
	
	}



	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}






}
