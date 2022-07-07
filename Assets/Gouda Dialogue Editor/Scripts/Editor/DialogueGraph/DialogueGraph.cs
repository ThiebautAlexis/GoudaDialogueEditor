using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace GoudaDialogueEditor.Editor
{
    public class DialogueGraph : EditorWindow
    {

        #region Fields and Properties
        private DialogueGraphView dialogueGraphView;
        private DialogueBlackboardView dialogueBlackboard;
        private DialogueNodeInspectorView dialogueNodeInspector;
        #endregion

        #region Methods
        [MenuItem("Gouda/Dialogue/Dialogue Graph")]
        public static void OpenDialogueGraph()
        {
            DialogueGraph wnd = GetWindow<DialogueGraph>();
            wnd.titleContent = new GUIContent("DialogueGraph");
        }

        private void OnDestroy()
        {
            NodeView.OnNodeSelected -= OnNodeSelectionChange;
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Gouda Dialogue Editor/Data/Stylesheets/DialogueGraph.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Gouda Dialogue Editor/Data/Stylesheets/DialogueGraph.uss");
            root.styleSheets.Add(styleSheet);

            dialogueGraphView = root.Q<DialogueGraphView>();
            dialogueBlackboard = root.Q<DialogueBlackboardView>();           
            dialogueNodeInspector = root.Q<DialogueNodeInspectorView>();

            NodeView.OnNodeSelected += OnNodeSelectionChange;
            
            OnSelectionChange();
        }

        private void OnSelectionChange()
        {
            DialogueAsset _dialogueAsset = Selection.activeObject as DialogueAsset;
            if (_dialogueAsset)
            {
                dialogueBlackboard.PopulateBlackboard(_dialogueAsset);
                dialogueGraphView.PopulateView(_dialogueAsset);
                dialogueNodeInspector.InitInspector(_dialogueAsset);
            }
        }

        private void OnNodeSelectionChange(NodeView _nodeView)
        {
            dialogueNodeInspector.DisplayNodeInspector(_nodeView);
        }
        #endregion
    }
}