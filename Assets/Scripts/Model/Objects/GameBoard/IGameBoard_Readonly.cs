using System;
using System.Collections.Generic;

namespace Model.Readonly
{
    /// <summary>
    /// Readonly досуп для презентеров, изменение модели должно происходить через игровую логику
    /// </summary>
    public interface IGameBoard_Readonly
    {
        public IEnumerable<IBlock_Readonly> Blocks_Readonly { get; }
        public ICell_Readonly[,] Cells_Readonly { get; }

        public event Action<IBlock_Readonly> OnBlockSpawn;
    }
}