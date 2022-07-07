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
        #endregion

        #region Methods 
        private void OnEnable()
        {
            dialogueLinesProperty = serializedObject.FindProperty("linesKeys");
        }

        public override void OnInspectorGUI()
        {

        }
        #endregion
    }
}
