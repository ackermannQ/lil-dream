using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    public float radius = 1f;
    public GameObject currentObj = null;

    void Update()
    {
        if (Input.GetButtonDown("Action") && currentObj)
        {
            Interact();
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (currentObj == null)
        {
            currentObj = gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        currentObj = null;
    }

    public virtual void Interact()
    {
        Debug.Log("interacting: " + gameObject.name);
    }
}
