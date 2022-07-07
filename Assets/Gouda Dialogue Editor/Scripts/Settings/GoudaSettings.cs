using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace GoudaDialogueEditor
{
    public class GoudaSettings : ScriptableObject
    {
        #region Static 
#if UNITY_EDITOR
        #region Static Fields
        private static readonly string settingsAssetPath = "Assets/Gouda Dialogue Editor/Data/Settings/";
        private static readonly string settingAssetName = "GoudaSettings.asset";
        #endregion

        #region Static Methods 
        [MenuItem("Gouda/Settings")]
        public static void SelectAsset()
        {
            if(!AssetDatabase.IsValidFolder(settingsAssetPath))
            {
                AssetDatabase.CreateFolder("Assets", "Gouda Dialogue Editor");
                AssetDatabase.CreateFolder("Assets/Gouda Dialogue Editor", "Data");
                AssetDatabase.CreateFolder("Assets/Gouda Dialogue Editor/Data", "Settings");

            }
            EditorUtility.FocusProjectWindow();
            GoudaSettings _settings = AssetDatabase.LoadAssetAtPath<GoudaSettings>(settingsAssetPath);
            if (!_settings)
            {
                _settings = CreateInstance<GoudaSettings>();
                AssetDatabase.CreateAsset(_settings, settingsAssetPath + settingAssetName);
                AssetDatabase.SaveAssets();
            }
            Selection.activeObject = _settings;
        }
        #endregion
#endif
        #endregion

        #region Fields and properties
        public string[] LocalisationKeys = new string[2] { "En_en", "Fr_fr" };
        #endregion

        #region Methods

        #endregion
    }
}
