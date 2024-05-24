using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private float fixedY;
    private Rigidbody rb;
    private int originalLayer;

    private void Start()
    {
        fixedY = transform.position.y + 0.1f;
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.playGame)
        {
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            offset = transform.position - GetMouseWorldPosition();
            originalLayer = gameObject.layer;
            gameObject.layer = LayerMask.NameToLayer("IgnoreRaycastDuringDrag");
        }
    }

    private void OnMouseDrag()
    {
        if (GameManager.Instance.playGame)
        {
            Vector3 mouseWorldPos = GetMouseWorldPosition() + offset;
            mouseWorldPos.y = fixedY;
            transform.position = mouseWorldPos;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private void OnMouseUp()
    {
        if (rb != null)
        {
            rb.isKinematic = false; 
        }
        gameObject.layer = originalLayer;
    }
}
