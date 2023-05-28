using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "FILE_NAME", menuName = "MENU/SUBMENU")]
    public class AllBlockTypes : ScriptableObject
    {
        //[SerializeField] private List<ABlockType> blockTypes = new();

        //private readonly string assetTypeName = typeof(ABlockType).Name;

        //public ABlockType GetBlockType()
        //{
        //    return null;
        //}
        //[Button]
        //public void FindAllBlockTypesInProject()
        //{
        //    blockTypes = new();

        //    string[] guids = AssetDatabase.FindAssets("t:" + assetTypeName);
        //    for (int i = 0; i < guids.Length; i++)
        //    {
        //        string path = AssetDatabase.GUIDToAssetPath(guids[i]);
        //        ABlockType asset = (ABlockType)AssetDatabase.LoadAssetAtPath(path, typeof(ABlockType));
        //        blockTypes.Add(asset);
        //    }
        //}
    }
}