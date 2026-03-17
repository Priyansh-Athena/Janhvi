using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tools : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<ToolItem> toolItems;
    [SerializeField] CanvasGroup toolInfoCg, allToolsCg, toolsPanel;

    [Space(10)]
    [SerializeField] Image toolIcon;
    [SerializeField] Button selectBtn;
    [SerializeField] CanvasGroup unselectBtnCg, openToolBtnCg;
    [SerializeField] TMP_Text toolTitle, toolInfo, toolUsage, selectBtnTxt;

    [SerializeField] ToolItem selectedToolItemUI; 

    [Space(10), Header("Data")]
    [TextArea(2, 10), SerializeField] string defaultToolName;
    [SerializeField] Sprite defaultToolSprite;

    void OnToolSelect(ToolItem _toolItem)
    {
        if(_toolItem == null)
        {
            selectedToolItemUI.Name = defaultToolName;
            selectedToolItemUI.Icon = defaultToolSprite;

            unselectBtnCg.blocksRaycasts = false;
            unselectBtnCg.DOFade(0f, 0.5f);
            return;
        }

        unselectBtnCg.blocksRaycasts = true;
        unselectBtnCg.DOFade(1f, 0.5f);

        selectedToolItemUI.Name = _toolItem.Name;
        selectedToolItemUI.Icon = _toolItem.Icon;
        selectedToolItemUI.Information = _toolItem.Information;
        selectedToolItemUI.HowToUse = _toolItem.HowToUse;
    }

    public UnityEvent<ToolItem> OnToolSelected;

    ToolItem selectedTool;
    int currIdx = -1;

    public ToolItem GetSelectedTool
    {
        get { return selectedTool; }
    }

    public void OpenToolInfo(int index)
    {
        if (index < 0 || index >= toolItems.Count) return;

        currIdx = index;
        ToolItem tool = toolItems[index];

        // Always update UI
        toolTitle.text = tool.Name;
        toolInfo.text = tool.Information;
        toolUsage.text = tool.HowToUse;
        toolIcon.sprite = tool.Icon;

        // Update button state
        if (selectedTool != null && selectedTool == tool)
        {
            selectBtn.interactable = false;
            selectBtnTxt.text = "Selected";
        }
        else
        {
            selectBtn.interactable = true;
            selectBtnTxt.text = "Select";
        }

        toolInfoCg.blocksRaycasts = true;
        toolInfoCg.DOFade(1f, 0.5f);
    }

    public void OpenAllTools()
    {
        toolInfoCg.DOFade(0f, 0.5f).OnComplete(() =>
        {
            toolInfoCg.blocksRaycasts = false;
        });
    }

    public void OpenToolsPanel()
    {
        toolsPanel.blocksRaycasts = true;
        toolsPanel.DOFade(1f, 0.5f);
        OpenAllTools();
    }

    public void CloseToolsPanel()
    {
        OpenAllTools();
        toolsPanel.blocksRaycasts = false;
        toolsPanel.DOFade(0f, 0.5f);
        openToolBtnCg.DOFade(1f, 0.35f);
    }

    public void SelectTool()
    {
        if (currIdx < 0 || currIdx >= toolItems.Count) return;

        selectedTool = toolItems[currIdx];

        CloseToolsPanel();

        OnToolSelect(selectedTool);
        OnToolSelected?.Invoke(selectedTool);
    }

    public void UnSelectTool()
    {
        OnToolSelect(null);
        OnToolSelected.Invoke(null);
        selectedTool = null;
        currIdx = -1;
    }
}