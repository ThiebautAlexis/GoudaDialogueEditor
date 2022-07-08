using System;
using UnityEngine;
using UnityEditor;

namespace GoudaDialogueEditor.Editor
{
    [CustomEditor(typeof(DialogueNode))]
    public class DialogueNodeEditor : NodeEditor
    {
        #region Fields and Properties
        protected SerializedProperty dialogueLinesProperty = null;
        protected string[] keys = new string[] { }; 
        #endregion

        #region Methods 
        private void OnEnable()
        {
            dialogueLinesProperty = serializedObject.FindProperty("linesKeys");
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < dialogueLinesProperty.arraySize; i++)
                EditLineKey(i);

            if(EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        }

        public override void InitEditor(DialogueAsset _asset)
        {
            base.InitEditor(_asset);
            int _arraySize = 0;
            for (int i = 0; i < _asset.Database.Length; i++)
                _arraySize += _asset.Database[i].DialogueLines.Length;

            keys = new string[_arraySize];
            _arraySize = 0;
            for (int i = 0; i < _asset.Database.Length; i++)
            {
                for (int j = 0; j < _asset.Database[i].DialogueLines.Length; j++)
                    keys[_arraySize + j] = _asset.Database[i].DialogueLines[j].LineKey;

                _arraySize += _asset.Database[i].DialogueLines.Length;
            }
        }

        protected virtual void EditLineKey(int _index)
        {
            EditorGUILayout.Popup(new GUIContent("Line Key"), 0, keys);
        }
        #endregion
    }
}
