using Model.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName ="NewLevelData",menuName ="Data/LevelData")]
    public class LevelDataScriptable : ScriptableObject
    {
        public GameBoardData gameBoard;
        [SerializeReference]
        public List<Goal> goalsList;
        public Goal[] goals;
        public IRestriction[] restrictions;
        public Balance balance;
    }
}
