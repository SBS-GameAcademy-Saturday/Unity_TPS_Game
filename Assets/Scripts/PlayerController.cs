using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Player Movement")]
	public float playerSpeed = 2f;
	public float playerSprint = 5.0f;

	[Header("Player Health")]
	private float PlayerHealth = 100;
	private float presentHealth;
	public HealthBar healthBar;
	public AudioClip PlayerhurtSound;
	public AudioSource audioSource;

	[Header("Player Script Cameras")]
	public Transform playerCamera;
	public GameObject deathCamera;
	public GameObject EndGameMenuUI;

	[Header("Player Animator and Gravity")]
	public CharacterController controller;
	public float Gravity = -9.81f;
	public Animator animator;

	[Header("Player Jumping and Velocity")]
	public Transform surfaceCheck;
	public float surfaceDistance = 0.4f;
	public LayerMask surfaceMask;

	public float jumpRange = 1f;

	public float turnCalmTime = 0.1f;

	private Vector3 velocity;
	private bool onSurface;

	private float turnCalmVelocity;

	private void Awake()
	{
		presentHealth = PlayerHealth;
		healthBar.GiveFullHealth();
	}

	// Start is called before the first frame update
	void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
		onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

		if(onSurface && velocity.y < 0)
		{
			velocity.y = -2f;
		}

		//gravity
		velocity.y += Gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);

		Move();

		Jump();

		//Sprint();
	}

	void Move()
	{
		float horizontal_axis = Input.GetAxisRaw("Horizontal");
		float vertical_axis = Input.GetAxisRaw("Vertical");

		float speed = Input.GetButton("Sprint") ? playerSprint : playerSpeed;
		if (animator.GetBool("Aiming") || animator.GetBool("Fire"))
			speed = playerSpeed;
		Vector3 direction = new Vector3(horizontal_axis, 0, vertical_axis).normalized;

		if(direction.magnitude < 0.1f) 
		{
			speed = 0;
		}

		float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
		float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
		transform.rotation = Quaternion.Euler(0, angle, 0);

		Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

		//controller.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);

		controller.Move(moveDirection.normalized * speed * Time.deltaTime);

		animator.SetFloat("Speed", controller.velocity.magnitude);

	}

	void Jump()
	{
		if (Input.GetButtonDown("Jump") && onSurface)
		{
			velocity.y = Mathf.Sqrt(jumpRange * -2 * Gravity);
			animator.SetTrigger("Jump");
		}
	}

	public void HitDamage(float takeDamage)
	{
		presentHealth -= takeDamage;
		healthBar.SetHealth(presentHealth);
		audioSource.PlayOneShot(PlayerhurtSound);
		if (presentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		EndGameMenuUI.SetActive(true);
		deathCamera.SetActive(true);
		Cursor.lockState = CursorLockMode.None;
		Destroy(gameObject, 1.0f);
	}

	//void Sprint()
	//{
	//	if (Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurface)
	//	{
	//		float horizontal_axis = Input.GetAxisRaw("Horizontal");
	//		float vertical_axis = Input.GetAxisRaw("Vertical");

	//		Vector3 direction = new Vector3(horizontal_axis, 0, vertical_axis).normalized;

	//		if (direction.magnitude >= 0.1f)
	//		{
	//			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
	//			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
	//			transform.rotation = Quaternion.Euler(0, angle, 0);

	//			Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
	//			controller.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
	//		}
	//	}
	//}

}
