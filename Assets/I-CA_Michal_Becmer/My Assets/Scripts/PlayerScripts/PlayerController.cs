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
    [SerializeField]private float rotationSpeed = 3f;
    private Camera mainCamera;
    private Coroutine coroutine;
    private Vector3 targetposition;

    [SerializeField] private Rigidbody rb;
    private int TraversableLayer;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb.GetComponent<Rigidbody>();
        TraversableLayer = LayerMask.NameToLayer("Traversable");
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
        if(Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider
        && hit.collider.gameObject.layer.CompareTo(TraversableLayer) == 0)
        {
            if(coroutine != null)StopCoroutine(coroutine);
           coroutine = StartCoroutine(PlayerMoveTowards(hit.point));
           targetposition = hit.point;
        }
    }

    private IEnumerator PlayerMoveTowards(Vector3 target)
    {
        //Calculate direction to target
        Vector3 direction = target - transform.position;
        direction.y = 0; //Ensures rotation is only on the horizontal plane

        //First rotate the player towards the target
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        //Then move the player towards the target
        while (Vector3.Distance(transform.position, target) > 0f)
        {
            Vector3 destination = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
            transform.position = destination;

            //Apply velocity
            rb.linearVelocity = direction.normalized * playerSpeed;

            yield return null;
        }

        //Stop the Rigidbody's velocity to prevent sliding
        rb.linearVelocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        //A sphere at the area we are going to for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetposition, 0.01f);
    }
}
