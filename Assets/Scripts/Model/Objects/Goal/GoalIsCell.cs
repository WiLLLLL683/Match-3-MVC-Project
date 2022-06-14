using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model.Objects
{
    [System.Serializable]
    public class GoalIsCell : AGoal
    {
        public ACellType CellType { get { return cellType; } }
        [SerializeField]
        protected ACellType cellType;
        public override event GoalDelegate goalIsUpdatedEvent;

        public override void UpdateGoal(IGoalTarget goalTarget)
        {
            if ((ACellType)goalTarget == cellType)
            {
                count -= 1;
                CheckCount();
                goalIsUpdatedEvent?.Invoke(this, new EventArgs());
            }
        }
    }
}