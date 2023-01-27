using Model.Objects;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Data
{
    public class Balance
    {
        private Dictionary<ABlockType, int> typesWeight;
        private int totalWeight;

        public Balance()
        {
            //TODO typeWeight = data...
            typesWeight = new Dictionary<ABlockType, int>();

            totalWeight = 0;
            foreach (var item in typesWeight)
            {
                totalWeight += item.Value;
            }
        }

        public Balance(Dictionary<ABlockType, int> _typesWeight)
        {
            typesWeight = _typesWeight;

            totalWeight = 0;
            foreach (var item in typesWeight)
            {
                totalWeight += item.Value;
            }
        }

        public ABlockType GetRandomBlockType()
        {
            int weightIndex = new Random().Next(0, totalWeight);

            int currentWeightIndex = 0;
            foreach (var item in typesWeight)
            {
                currentWeightIndex += item.Value;
                if (currentWeightIndex >= weightIndex)
                {
                    return item.Key;
                }
            }

            return new BasicBlockType();
        }

        public Balance Clone()
        {
            return (Balance)this.MemberwiseClone();
        }
    }
}