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
        //Theres no "floor" we in space lol
        //so we don't need to have an offset
        //float playerDistanceToFloor = transform.position.y - target.y;
        //target.y += playerDistanceToFloor;
        
        while(Vector3.Distance(transform.position, target) > 0f)
        {
            Vector3 destination = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
            transform.position = destination;

            Vector3 direction = target - transform.position;
            rb.linearVelocity = direction.normalized * playerSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized), 
            rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(targetposition, 1);
    }
}
