public class InteractableSleep : InteractableBase
{
    public Dialog dialog;

    public override void Interact()
    {
        TriggerDialog();
    }

    public void TriggerDialog()
    {
        FindObjectOfType<SleepManager>().StartDialog(dialog);
    }
}
