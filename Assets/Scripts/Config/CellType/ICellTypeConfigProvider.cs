namespace Config
{
    public interface ICellTypeConfigProvider
    {
        CellTypeSO HiddenCellType { get; }

        CellTypeSO GetSO(int id);
    }
}