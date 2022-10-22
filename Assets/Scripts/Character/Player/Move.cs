using UnityEngine;

namespace Character.Player
{
    public class Move : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private float moveSpeed = .025f;
        
        public GameObject raycastPoint;
        public LayerMask groundLayer;

        public bool isGrounded;
        public float groundDistance = 1f;
        public float gravity = 9.81f;
        public float rotationSpeed = .8f;

        private CharacterController _characterController;
        private Vector3 _velocity;
        
        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                PlayerMove();
                PlayerRotate();
                PlayerAnimations.Instance.KeepRunning();
            }
            else
            {
                PlayerAnimations.Instance.StopRunning();
            }
        }

        private void PlayerMove()
        {
            isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayer);

            if (isGrounded && !raycastPoint.activeInHierarchy) // if raycastPoint is enabled
            {
                raycastPoint.SetActive(true);
            }

            if (isGrounded && _velocity.y < 0) { _velocity.y = 0; }

            _characterController.Move(new Vector3(joystick.Horizontal * moveSpeed, 0, joystick.Vertical * moveSpeed));
            _velocity.y -= gravity * Time.deltaTime * 40f;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void PlayerRotate()
        {
            // calculates the angle to rotate
            var angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg; 
            Quaternion targetRotation = Quaternion.Euler(0, -angle + 90f, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }
    }
}
