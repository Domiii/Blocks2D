using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockGrid : MonoBehaviour {
	public static BlockGrid Instance {
		get;
		private set;
	}

	public float cellSize = 1f;
	public IntVector2 min = new IntVector2(-10, -10);
	public IntVector2 max = new IntVector2(10, 10);

	GameObject cellRoot;
	Cell cellPrefab;
	public Cell[,] cells;

	public Vector3 MinPos {
		get;
		private set;
	}

	public Vector3 MaxPos {
		get;
		private set;
	}

	public BlockGrid() {
		Instance = this;
	}

	public Cell GetCell(int i, int j) {
		return cells[j, i];
	}

	public void Init() {
		MinPos = new Vector2 (min.x * cellSize, min.y * cellSize);
		MaxPos = new Vector2 (max.x * cellSize, max.y * cellSize);
	}

	void Awake() {
		Init ();
		CreateCellPrefab ();
		CreateCells ();
	}

	void CreateCellPrefab() {
		if (cellPrefab == null) {
			var cellGO = new GameObject ("Cell");
			var cellCollider = cellGO.AddComponent<BoxCollider2D> ();
			cellCollider.isTrigger = true;
			cellCollider.size = Vector2.one * cellSize;
			cellPrefab = cellGO.AddComponent<Cell> ();
		}
	}

	void CreateCells() {
		CreateCellPrefab ();
		if (cellRoot != null) {
			Destroy (cellRoot);
		}

		cellRoot = new GameObject ("Cells");
		cellRoot.transform.parent = transform;
		cells = new Cell[max.y-min.y, max.x-min.x];

		Vector2 pos;
		for (var j = min.y; j < max.y; ++j) {
			pos.y = j * cellSize + cellSize / 2;
			for (var i = min.x; i < max.x; ++i) {
				pos.x = i * cellSize + cellSize / 2;
				var cell = Instantiate<Cell>(cellPrefab, pos, Quaternion.identity, cellRoot.transform);

				var iIdx = i - min.x;
				var jIdx = j - min.y;
				cell.name = string.Format("Cell ({0}, {1})", iIdx, jIdx);
				cells[jIdx, iIdx] = cell;
			}
		}
	}

	public void SnapToGrid(SpriteRenderer sprite) {
		// convert positions and sizes to cell space
		var spriteMin = (Vector2)sprite.bounds.min / cellSize;
		var spriteMax = (Vector2)sprite.bounds.max / cellSize;
		var w = 2 * sprite.bounds.extents.x / cellSize;
		var h = 2 * sprite.bounds.extents.y / cellSize;
		Vector2 snappedPos = (Vector2)sprite.transform.position / cellSize;

		// let's go!
		var snappedW = (int)Ceil(w);
		var snappedH = (int)Ceil(h);

		if (snappedW % 2 == 1) {
			// blocks of odd size are snapped to cell centers (instead of snapped to grid nodes)
			snappedPos -= Vector2.one * 0.5f;
		}

		var snapCorrection = GetSnapCorrection (snappedPos);
		//snappedPos += snapCorrection;

		if (snappedW % 2 == 1) {
			// blocks of odd size are snapped to cell centers (instead of snapped to grid nodes)
			snappedPos += Vector2.one *  0.5f;
		}
		var snappedMin = Floor (spriteMin + snapCorrection);
		var snappedMax = Ceil (spriteMax + snapCorrection);

		// make sure we don't leave the grid!
		if (IsTouchingLeftEdge (snappedMin)) {
			// don't go smaller than min
			snappedMin.x = MinPos.x;
			snappedMax.x = snappedMin.x + snappedW;
		} else if (IsTouchingRightEdge (snappedMax)) {
			// don't go bigger than max	
			snappedMax.x = MaxPos.x;
			snappedMin.x = snappedMax.x - snappedW;
		} if (IsTouchingBottomEdge (snappedMin)) {
			// don't go smaller than min
			snappedMin.y = MinPos.y;
			snappedMax.y = snappedMin.y + snappedH;
		} else if (IsTouchingTopEdge (snappedMax)) {
			// don't go bigger than max	
			snappedMax.y = MaxPos.y;
			snappedMin.y = snappedMax.y - snappedH;
		}

		// anchor position based on snapped min vertex
		snappedPos = snappedMin + (snappedPos - spriteMin);

		// convert final position back to world space
		sprite.transform.position = snappedPos * cellSize;
	}

	bool IsTouchingLeftEdge(Vector2 pos) {
		return pos.x <= min.x;
	}

	bool IsTouchingBottomEdge(Vector2 pos) {
		return pos.y <= min.y;
	}

	bool IsTouchingRightEdge(Vector2 pos) {
		return pos.x >= max.x;
	}

	bool IsTouchingTopEdge(Vector2 pos) {
		return pos.y >= max.y;
	}

	float Ceil(float t) {
		return Mathf.Ceil(t);
	}

	Vector2 SnapToGrid(Vector2 pos) {
		return Floor (pos);
	}

	// if sprite does is not exactly a multiple of cellSize, make sure, it's centered inside the cells
	Vector2 GetSnapCorrection(Vector2 pos) {
		var xMod = Mathf.Repeat (pos.x, 1);
		var yMod = Mathf.Repeat (pos.y, 1);
		return new Vector2 (
			Mathf.Round (xMod) - xMod,
			Mathf.Round (yMod) - yMod
		);
	}

	Vector2 Floor(Vector2 pos) {
		var res = new Vector2(Mathf.Floor (pos.x), Mathf.Floor (pos.y));
//		res.x = (int)Mathf.Clamp (res.x, min.x, max.x);
//		res.y = (int)Mathf.Clamp (res.y, min.y, max.y);
		return res;
	}

	public Vector2 SnapWorldToGridFloor(Vector2 pos) {
		return Floor(pos/cellSize) * cellSize;
	}

	Vector2 Ceil(Vector2 pos) {
		var res = new Vector2(Mathf.Ceil (pos.x), Mathf.Ceil (pos.y));
//		res.x = (int)Mathf.Clamp (res.x, min.x, max.x);
//		res.y = (int)Mathf.Clamp (res.y, min.y, max.y);
		return res;
	}


	#region Gizmos
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.black;
		Vector3 from = new Vector3(), to = new Vector3();
		from.x = min.x * cellSize;
		to.x = max.x * cellSize;

		for (var j = min.y; j < max.y; ++j) {
			from.y = to.y = j * cellSize;
			Gizmos.DrawLine (from, to);
		}

		from.y = min.y * cellSize;
		to.y = max.y * cellSize;
		for (var i = min.x; i < max.x; ++i) {
			from.x = to.x = i * cellSize;
			Gizmos.DrawLine (from, to);
		}

		OnDrawGizmos ();
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.gray;
		Vector3 from = new Vector3(), to = new Vector3();
		from.x = min.x * cellSize;
		to.x = max.x * cellSize;

		for (var j = min.y; j <= max.y; j += max.y) {
			from.y = to.y = j * cellSize;
			Gizmos.DrawLine (from, to);
		}

		from.y = min.y * cellSize;
		to.y = max.y * cellSize;
		for (var i = min.x; i <= max.x; i += max.x) {
			from.x = to.x = i * cellSize;
			Gizmos.DrawLine (from, to);
		}
	}
	#endregion
}
