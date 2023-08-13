using UnityEngine;

namespace Model.Readonly
{
    public interface IBlockType_Readonly
    {
        int Id { get; }
        Sprite Icon { get; }
        ParticleSystem DestroyEffect { get; }
    }
}