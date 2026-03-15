using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] SceneType sceneToOpen;
    [SerializeField] InstructionType instruciton;
    bool canEnterDoor;

    private void Update()
    {
        if (canEnterDoor && Input.GetKeyDown(KeyCode.E))
        {
            OnDoorTriggerExit();
            Persisting.Instance.LoadScene(Scenes.Get(sceneToOpen));
        }
    }

    public void OnDoorTriggerEnter()
    {
        canEnterDoor = true;
        Persisting.Instance.ShowPlayerInstruction(Instructions.Get(instruciton));
    }

    public void OnDoorTriggerExit()
    {
        canEnterDoor = false;
        Persisting.Instance.HidePlayerInstruction();
    }
}
