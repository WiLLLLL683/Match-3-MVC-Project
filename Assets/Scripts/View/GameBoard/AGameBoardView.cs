using UnityEngine;

public abstract class AGameBoardView : MonoBehaviour
{
    public abstract Transform BlocksParent { get; }
    public abstract Transform CellsParent { get; }
}