using System;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    /// <summary>
    /// Преобразовать направление в Vector2Int
    /// </summary>
    public static Vector2Int ToVector2Int(this Directions direction)
    {
        return direction switch
        {
            Directions.Up => Vector2Int.up,
            Directions.Down => Vector2Int.down,
            Directions.Left => Vector2Int.left,
            Directions.Right => Vector2Int.right,
            Directions.Zero => Vector2Int.zero,
            _ => Vector2Int.zero,
        };
    }
    /// <summary>
    /// Преобразовать направление в противоположное
    /// </summary>
    public static Directions ToOpposite(this Directions direction)
    {
        return direction switch
        {
            Directions.Up => Directions.Down,
            Directions.Down => Directions.Up,
            Directions.Left => Directions.Right,
            Directions.Right => Directions.Left,
            Directions.Zero => Directions.Zero,
            _ => Directions.Zero,
        };
    }
    /// <summary>
    /// Преобразовать направление из координат модели в координаты вью и наоборот, инвертировав верх и низ
    /// </summary>
    public static Directions InvertUpDown(this Directions direction)
    {
        return direction switch
        {
            Directions.Up => Directions.Down,
            Directions.Down => Directions.Up,
            Directions.Left => Directions.Left,
            Directions.Right => Directions.Right,
            Directions.Zero => Directions.Zero,
            _ => Directions.Zero,
        };
    }
    /// <summary>
    /// Преобразовать положение из координат модели в координаты вью
    /// </summary>
    public static Vector2Int ToViewPos(this Vector2Int modelPosition)
    {
        //строки положения нумеруются сверху вниз, поэтому Position.y отрицательный
        return new Vector2Int(modelPosition.x, -modelPosition.y);
    }
}
