using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputAction input;

    [SerializeField]private float playerSpeed = 2f;
    private Camera mainCamera;
    private Coroutine coroutine;
    private Vector3 targetposition;

    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.performed += Move;
    }

    private void OnDisable()
    {
        input.performed -= Move;
        input.Disable();
    }

    private void Move(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider)
        {
            if(coroutine != null)StopCoroutine(coroutine);
           coroutine = StartCoroutine(PlayerMoveTowards(hit.point));
           targetposition = hit.point;
        }
    }

    private IEnumerator PlayerMoveTowards(Vector3 target)
    {
        float playerDistanceToFloor = transform.position.y - target.y;
        target.y += playerDistanceToFloor;
        while(Vector3.Distance(transform.position, target) > 0f)
        {
            Vector3 destination = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
            transform.position = destination;

            Vector3 direction = target - transform.position;
            rb.linearVelocity = direction.normalized * playerSpeed;

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(targetposition, 1);
    }
}
