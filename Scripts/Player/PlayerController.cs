using UnityEngine;
using GangWarfare.Input;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 4f;
        public float sprintMultiplier = 1.5f;
        public float rotationSpeed = 720f;
        public float gravity = -9.81f;

        private CharacterController controller;
        private GangWarfareInput input;

        private Vector2 moveInput;
        private bool isSprinting;
        private float verticalVelocity;
		
		private Animator animator;
		private bool canMove = false;
		
        private void Awake()
        {
			animator = GetComponentInChildren<Animator>();
            controller = GetComponent<CharacterController>();
            input = new GangWarfareInput();
        }

        private void OnEnable()
        {
            input.Enable();
			GameStateManager.OnStateChanged += HandleStateChanged;
        }

        private void OnDisable()
        {
            input.Disable();
			GameStateManager.OnStateChanged -= HandleStateChanged;
        }

        private void Update()
        {
			if (!canMove)
				return;

            moveInput = input.Player.Move.ReadValue<Vector2>();
            isSprinting = input.Player.Sprint.IsPressed();

            Move();
			
			float currentSpeed = moveInput.magnitude;
			animator.SetFloat("Speed", currentSpeed, 0.1f, Time.deltaTime);
			animator.SetBool("IsSprinting", isSprinting);
        }

        private void Move()
        {
            Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);

            if (moveDir.sqrMagnitude > 0.01f)
            {
                float speed = moveSpeed * (isSprinting ? sprintMultiplier : 1f);

                // Rotate toward movement
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );

                moveDir = transform.forward;
                controller.Move(moveDir * speed * Time.deltaTime);
            }

            // Gravity
            if (controller.isGrounded && verticalVelocity < 0)
                verticalVelocity = -2f;

            verticalVelocity += gravity * Time.deltaTime;

            controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
        }
		
		private void HandleStateChanged(GameState state)
		{
			canMove = (state == GameState.Gameplay);
		}
    }
}