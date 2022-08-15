using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DCellTypeEnum))]
    public class Array2DCellTypeEnumDrawer : Array2DEnumDrawer<CellTypeEnum> {}
}
