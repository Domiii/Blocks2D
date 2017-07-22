using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BlockGrid))]
public class BlockGridEditor : Editor {


	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		var grid = (BlockGrid)target;
		grid.Init ();

		if (GUILayout.Button ("Snap all blocks")) {
			var blocks = GameObject.FindObjectsOfType<BlockSprite> ();

			foreach (var block in blocks) {
				Undo.RecordObject (block.transform, "Snapped block");
				grid.SnapToGrid (block.GetComponent<SpriteRenderer> ());
			}
		}
	}
}
