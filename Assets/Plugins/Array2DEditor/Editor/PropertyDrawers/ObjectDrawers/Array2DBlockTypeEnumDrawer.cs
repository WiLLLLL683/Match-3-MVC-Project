using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DBlockTypeEnum))]
    public class Array2DBlockTypeEnumDrawer : Array2DEnumDrawer<BlockTypeEnum> {}
}