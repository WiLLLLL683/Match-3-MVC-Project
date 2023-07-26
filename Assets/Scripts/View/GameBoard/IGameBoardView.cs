using UnityEngine;

public abstract class IGameBoardView : MonoBehaviour
{
    public abstract Transform BlocksParent { get; }
    public abstract Transform CellsParent { get; }
}