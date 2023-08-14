using Data;
using System.Collections;
using UnityEngine;

namespace Model.Objects.UnitTests
{
    public class TestBlockType : IBlockType
    {
        public TestBlockType(int id = 0)
        {

        }

        public ParticleSystem DestroyEffect
        {
            get
            {
                // TODO: Add your implementation
                return null;
            }
        }

        public Sprite Icon
        {
            get
            {
                // TODO: Add your implementation
                return null;
            }
        }

        public int Id
        {
            get
            {
                // TODO: Add your implementation
                return 0;
            }
        }

        public bool Activate()
        {
            return true;
        }
    }
}