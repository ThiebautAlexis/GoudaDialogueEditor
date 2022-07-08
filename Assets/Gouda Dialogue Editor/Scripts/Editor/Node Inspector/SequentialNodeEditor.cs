using System;
using UnityEngine;
using UnityEditor;

namespace GoudaDialogueEditor.Editor
{
    [CustomEditor(typeof(SequentialNode))]
    public class SequentialNodeEditor : DialogueNodeEditor
    {
        #region Fields and Properties

        #endregion

        #region Methods 
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button(new GUIContent("+")))
            {
                dialogueLinesProperty.InsertArrayElementAtIndex(dialogueLinesProperty.arraySize);
                serializedObject.ApplyModifiedProperties();
            }
        }

        protected override void EditLineKey(int _index)
        {
            EditorGUILayout.BeginHorizontal();
            base.EditLineKey(_index);
            if (GUILayout.Button("x", GUILayout.Width(10f)))
                dialogueLinesProperty.DeleteArrayElementAtIndex(_index);
            EditorGUILayout.EndHorizontal();
        }
        #endregion
    }
}
