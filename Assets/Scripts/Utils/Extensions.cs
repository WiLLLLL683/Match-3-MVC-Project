using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
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
        /// Преобразовать дельта-вектор в направление
        /// </summary>
        public static Directions ToDirection(this Vector2 deltaPosition, float minDelta = 0)
        {
            if (deltaPosition.magnitude <= minDelta)
                return Directions.Zero;

            if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.y))
            {
                if (deltaPosition.x > 0)
                    return Directions.Right;
                else
                    return Directions.Left;
            }
            else
            {
                if (deltaPosition.y > 0)
                    return Directions.Up;
                else
                    return Directions.Down;
            }
        }

        public static T[] MemberwiseArrayClone<T>(this T[] array) where T : ICloneable
        {
            T[] newArray = new T[array.Length];

            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = (T)array[i].Clone();
            }

            return newArray;
        }

        public static bool IsInBounds<T>(this T[] array, int index)
        {
            return 0 <= index && index < array.Length;
        }

        public static bool IsInBounds<T>(this T[,] array, Vector2Int position)
        {
            return 0 <= position.x && position.x < array.GetLength(0) && 0 <= position.y && position.y < array.GetLength(1);
        }

        public static bool IsInBounds<T>(this List<T> list, int index)
        {
            return 0 <= index && index < list.Count;
        }
    }
}