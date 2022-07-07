using System;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace GoudaDialogueEditor.Editor
{
    public class DialogueNodeInspectorView : VisualElement
    {
        #region Fields and Properties
        private DialogueAsset dialogueAsset;
        private NodeEditor editor; 
        #endregion

        #region Constructors
        public new class UxmlFactory : UxmlFactory<DialogueNodeInspectorView, VisualElement.UxmlTraits> { }

        public DialogueNodeInspectorView()
        {

        }
        #endregion

        #region Methods 
        public void InitInspector(DialogueAsset _asset) => dialogueAsset = _asset;

        public void DisplayNodeInspector(NodeView _nodeView)
        {
            Clear();
            UnityEditor.Editor.DestroyImmediate(editor);

            editor = UnityEditor.Editor.CreateEditor(_nodeView.node) as NodeEditor;
            editor.InitEditor(dialogueAsset);
            IMGUIContainer _container = new IMGUIContainer(editor.OnInspectorGUI);
            Add(_container);
        }
        #endregion
    }
}
