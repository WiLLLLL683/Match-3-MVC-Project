using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model.Objects
{
    [System.Serializable]
    public class GoalIsBlock : AGoal
    {
        public ABlockType BlockType { get { return blockType; } }
        [SerializeField]
        protected ABlockType blockType;
        public override event GoalDelegate goalIsUpdatedEvent;

        public override void UpdateGoal(IGoalTarget goalTarget)
        {
            if ((ABlockType)goalTarget == blockType)
            {
                count -= 1;
                CheckCount();
                goalIsUpdatedEvent?.Invoke(this, new EventArgs());
            }
        }
    }
}