using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollNavigation : MonoBehaviour
{
    RectTransform scrollRectTransform;
    [SerializeField] RectTransform contentPanel;
    RectTransform selectedRectTransform;
    GameObject lastSelected;
 
    void Start() {
        scrollRectTransform = GetComponent<RectTransform>();
    }
 
    void Update() {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        //  Return if not game object currently selected.
        if (selected == null) {
            return;
        }
        //  Return if selected game object not inside the scroll rect.
        if (selected.transform.parent != contentPanel.transform) {
            return;
        }
        //  Return selected game object same as last frame ie. not moved.
        if (selected == lastSelected) {
            return;
        }
        selectedRectTransform = selected.GetComponent<RectTransform>();
        //  Position of the selected UI element is the absolute anchor position.
        //  If scrolling down, it's local position in the scroll rect plus its height.
        //  If scrolling up, it's just the absolute anchor position.
        float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;
        //  Upper bound is anchor position of content.
        float scrollViewMinY = contentPanel.anchoredPosition.y;
        //  Lower bound is anchor position + height of scroll rect.
        float scrollViewMaxY = contentPanel.anchoredPosition.y + scrollRectTransform.rect.height;
        //  Scroll down if selected position is below current upper bound of scroll view.
        if (selectedPositionY > scrollViewMaxY) {
            float newY = selectedPositionY - scrollRectTransform.rect.height;
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, newY);
        }
        //  Scroll up if selected position is above current upper bound of scroll view.
        else if (Mathf.Abs(selectedRectTransform.anchoredPosition.y) < scrollViewMinY) {
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, Mathf.Abs(selectedRectTransform.anchoredPosition.y));
        }
        lastSelected = selected;
    }
}
