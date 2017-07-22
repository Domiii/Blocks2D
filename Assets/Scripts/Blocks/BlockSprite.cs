using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Draggable buildings are always snapped to grid, and can be dragged by mouse.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class BlockSprite : MonoBehaviour {
	public SpriteRenderer sprite;
	public bool canDrag = true;

	private bool mouseDown = false;
	Vector3 dragOffset;
	private bool restrictX;
	private bool restrictY;
	private float fakeX;
	private float fakeY;
	private float myWidth;
	private float myHeight;

	void Start()
	{
		sprite = GetComponent<SpriteRenderer> ();
		SnapToGrid ();
	}


	void OnMouseDown() 
	{
		if (canDrag) {
			mouseDown = true;
			var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			// make sure that the cell of the sprite that the mouse touched is always in the same cell as the mouse
			dragOffset = transform.position - mousePos;
		}
	}

	void OnMouseUp() 
	{
		mouseDown = false;
	}

	void SnapToGrid() {
		BlockGrid.Instance.SnapToGrid (sprite);
	}


	void Update () 
	{
		if (mouseDown) {
			var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = mousePos + dragOffset;
			SnapToGrid ();
		}
	}
}