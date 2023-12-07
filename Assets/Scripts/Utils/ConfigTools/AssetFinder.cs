using Config;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Utils
{
    /// <summary>
    /// Утилита для поиска всех ассетов нужного типа
    /// </summary>
    public class AssetFinder
    {
        /// <summary>
        /// Заменить данные в листе найденными ассетами заданного типа
        /// </summary>
        public bool FindAllAssetsOfType<T,TParent>(ref List<T> listForAssets, TParent parentObject)
            where T : UnityEngine.Object
            where TParent : UnityEngine.Object
        {
#if UNITY_EDITOR

            string typeName = typeof(T).ToString();
            bool choice = EditorUtility.DisplayDialog("Replace data?",
                $"Do you want to remove old list of {typeName} in {parentObject.name} and replace it with found assets?",
                "Replace", "Cancel");

            if (!choice)
                return choice;

            Undo.RecordObject(parentObject, $"Replace list of {typeName}");
            listForAssets.Clear();
            string[] guids = AssetDatabase.FindAssets("t:" + typeName);

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                T asset = (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
                listForAssets.Add(asset);
            }

            return true;
#else
            return false;
#endif
        }

        public void FindAllBlockTypes<TParent>(ref List<BlockTypeSO_Weight> listForAssets, TParent parentObject)
            where TParent : UnityEngine.Object
        {
            List<BlockTypeSO> blockTypes = new();
            bool choice = FindAllAssetsOfType(ref blockTypes, parentObject);

            if (!choice)
                return;

            listForAssets.Clear();

            for (int i = 0; i < blockTypes.Count; i++)
            {
                BlockTypeSO_Weight weight = new();
                weight.blockTypeSO = blockTypes[i];
                listForAssets.Add(weight);
            }
        }
    }
}