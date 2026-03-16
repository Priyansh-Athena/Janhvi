using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolItem : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] string toolName;
    [SerializeField] string toolInfo;
    [SerializeField] string howToUse;
    [SerializeField] Sprite icon;

    [Header("References")]
    [SerializeField] TMP_Text nameTxt;
    [SerializeField] Image iconImg;

    private void Awake()
    {
        Name = toolName;
        Icon = icon;
        Information = toolInfo;
        HowToUse = howToUse;
    }

    public string Name
    {
        get { return toolName; }
        set
        {
            toolName = value;
            if (nameTxt != null)
                nameTxt.text = value;
        }
    }

    public string Information
    {
        get { return toolInfo; }
        set { toolInfo = value; }
    }

    public string HowToUse
    {
        get { return howToUse; }
        set { howToUse = value; }
    }

    public Sprite Icon
    {
        get { return icon; }
        set
        {
            icon = value;
            if (iconImg != null)
                iconImg.sprite = value;
        }
    }
}