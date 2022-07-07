using System;
using UnityEngine;
using UnityEditor;

namespace GoudaDialogueEditor.Editor
{
    [CustomEditor(typeof(Node))]
    public class NodeEditor : UnityEditor.Editor
    {
        #region Fields and Properties
        protected SerializedObject dialogueAsset;
        #endregion

        #region Methods 
        public void InitEditor(DialogueAsset _asset)
        {
            dialogueAsset = new SerializedObject(_asset);
        }
        #endregion
    }
}
