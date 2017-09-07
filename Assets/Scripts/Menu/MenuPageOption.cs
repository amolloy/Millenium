﻿using UnityEngine;
using UnityEngine.UI;

public class MenuPageOption : SelectableHelper {

    [TextArea]
    public string descriptionText;
    public Color overlayColor;

    private MenuManager menuManager;
    private Cursor cursor;
    private Color oldColor;
    private CanvasRenderer canvasRenderer;

    public override void onCursorInit(Cursor cursor) {
        base.onCursorInit(cursor);
        menuManager = GetComponentInParent<MenuManager>();
        canvasRenderer = GetComponent<CanvasRenderer>();
        oldColor = canvasRenderer.GetColor();
        this.cursor = cursor;

        canvasRenderer.SetColor(overlayColor);
    }

    public override void onCursorSelect() {
        base.onCursorSelect();

        int selectedIndex = cursor.selectedIndex;
        MenuPage selectedPage = menuManager.getPageOfIndex(selectedIndex);

        menuManager.descriptionBox.text = descriptionText;
        canvasRenderer.SetColor(oldColor);
        selectedPage.gameObject.SetActive(true);
        selectedPage.focusPage();
        selectedPage.transform.SetSiblingIndex(menuManager.pages.Length);
        if(cursor.previousSelectedIndex > cursor.selectedIndex){
            selectedPage.animator.SetTrigger("TurnRight");
        }else if(cursor.previousSelectedIndex < cursor.selectedIndex) {
            selectedPage.animator.SetTrigger("TurnLeft");
        }
    }

    public override void onCursorLeave() {
        base.onCursorLeave();
        canvasRenderer.SetColor(overlayColor);
        menuManager.getPageOfIndex(cursor.previousSelectedIndex).unfocusPage(menuManager.animationLength);
    }

    public override void onOKPressed() {
        base.onOKPressed();
    }

    public override void onCancelPressed() {
        base.onCancelPressed();
        
        menuManager.closeMenu();
    }

}
