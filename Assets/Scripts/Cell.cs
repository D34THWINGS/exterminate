using UnityEngine;
using System;

public class Cell : MonoBehaviour {
	public IntVector2 coordinates;
	public CellEdge[] edges = new CellEdge[Directions.Count];

	private int initializedEdges = 0;

	public bool IsFullyInitialized {
		get {
			return initializedEdges == Directions.Count;
		}
	}

	public Direction RandomUnitializedEdge {
		get {
			for (int i = 0; i < Directions.Count; i++) {
				if (edges[i] == null) {
					return edges[i].direction;
				}
			}
			throw new Exception("Cell has no uninitialized directions left.");
		}
	}

	public CellEdge GetEdge (Direction direction) {
		return edges[(int)direction];
	}

	public void SetEdge (Direction direction, CellEdge edge) {
		edges[(int)direction] = edge;
	}
	
}
