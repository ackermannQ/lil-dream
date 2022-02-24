public class InteractableStartDialog : InteractableBase
{
    public Dialog dialog;
    public override void Interact()
    {
        TriggerDialog();
    }

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
}
