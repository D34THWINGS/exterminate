using UnityEngine;

public class Player : MonoBehaviour {

	Cell currentCell;
	Direction currentDirection;

	public void SetLocation (Cell cell ) {
		if (cell != null) {
			currentCell = cell;
		}

		transform.localPosition = cell.transform.localPosition;
	}

	public void Rotate (Direction direction) {
		transform.localRotation = direction.ToRotation ();
	}

	public void Move (Direction direction) {
		CellEdge edge = currentCell.GetEdge (direction);
		// if (edge is Empty) {
		// 	SetLocation (edge.otherCell);
		// }
	}

}