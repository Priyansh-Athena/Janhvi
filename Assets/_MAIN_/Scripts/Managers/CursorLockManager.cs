using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using DG.Tweening;

public class CursorLockManager : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.Escape;
    [SerializeField] CanvasGroup cursorInstructionCg;

    bool isCursorLocked = false;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (isCursorLocked)
                UnlockCursor();
            else
                LockCursor();
        }

        if (!isCursorLocked && Input.GetMouseButtonDown(0))
        {
            // Lock only if we are NOT clicking a raycastable UI
            if (!IsPointerOverRaycastUI())
            {
                LockCursor();
            }
        }
    }

    bool IsPointerOverRaycastUI()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count > 0;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;
        cursorInstructionCg.DOFade(1f, 0.35f);
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
        cursorInstructionCg.DOFade(0f, 0.35f);
    }
}