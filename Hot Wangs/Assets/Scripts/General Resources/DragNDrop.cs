using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    [SerializeField] bool isDraggable = true;
    bool isDragged = false;
    Vector3 offset; // mouse distance from center of object

    private void Update()
    {
        // If mouse button not held, release drag
        if (!Input.GetMouseButton(0))
        {
            isDragged = false;
        }

        // Update object position based on mouse
        if (isDragged)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            pos.z = 0;
            gameObject.transform.position = pos;
        }
    }

    // Start dragging when object is clicked
    private void OnMouseOver()
    {
        if (isDraggable && Input.GetMouseButtonDown(0))
        {
            isDragged = true;
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    // Stop dragging on mouse up
    private void OnMouseUp()
    {
        isDragged = false;
    }

    // Allows drag to be set on instantiate
    public void Drag()
    {
        OnMouseOver();
    }
}
