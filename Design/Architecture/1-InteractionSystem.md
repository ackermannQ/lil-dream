# Interaction System:



We want the user to satisfy an interface IInteractable:

```csharp
public interface IInteractable
{
     void Interact();
}
```



This will be the contract of the base class: InteractableBase

```csharp
public class InteractableBase : MonoBehaviour, IInteractable
{
    // Getter/Setter of the private member
    public virtual void OnInteract()
   {
      Debug.Log("INTERACTED : " + gameObject.name);
   }
}
```



The idea is to override this on the different object and couple it with a dialog system

As a consequence, the dialog will be stored inside the objects and called on interaction while displaying the dialog window