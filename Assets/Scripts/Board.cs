using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {

	public IntVector2 size;
	public Cell cellPrefab;
	public Transform rotorPrefab;
	public Transform conveyorPrefab;
	public Transform voidPrefab;
	public Transform wallPrefab; // Possibility to use an array to avoid redundancy
	public Transform checkpointPrefab;
	public CellEdge edgePrefab;

	private	Cell[,] cells;

	public Cell GetCell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.y];
	}

	private bool IsInsideBoard (IntVector2 coordinates) {
		return coordinates.x > 0 && coordinates.x < size.x && coordinates.y > 0 && coordinates.y < size.y;
	}

	public void Generate ()
	{
		cells = new Cell[size.x, size.y];
		List<Cell> activeCells = new List<Cell> ();
		activeCells.Add (CreateCell (new IntVector2 (0, 0)));
		while (activeCells.Count > 0) {
			GenerateNext (activeCells);
		}
	}

	public void GenerateNext (List<Cell> activeCells) {
		Cell currentCell = activeCells[0];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt (0);
			return;
		}

		Direction direction = currentCell.RandomUnitializedEdge;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2 ();
		if (IsInsideBoard (coordinates)){
			Cell neighbor = GetCell (coordinates);
			if (neighbor == null) {
				neighbor = CreateCell (coordinates);
				CreateEdge (currentCell, neighbor, direction);
				activeCells.Add (neighbor);
			} else {
				CreateEdge (currentCell, neighbor, direction);
			}
		} else {
			CreateEdge (currentCell, null, direction);
		}	
	}

	private Cell CreateCell (IntVector2 coordinates) {
		Cell newCell = Instantiate (cellPrefab) as Cell;
		cells[coordinates.x, coordinates.y] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Cell " + coordinates.x + " " + coordinates.y;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3 (coordinates.x - size.x * 0.5f + 1f, coordinates.y - size.y * 0.5f + 1f, 0f);
		return newCell;
	}

	private void CreateEdge (Cell cell, Cell otherCell, Direction direction) {
		CellEdge edge = Instantiate (edgePrefab) as CellEdge;
		edge.Initialize (cell, otherCell, direction);
		if (otherCell != null) {
			edge = Instantiate (edgePrefab) as CellEdge;
			edge.Initialize (otherCell, cell, direction.GetOpposite ());
		}
	}
	
}
