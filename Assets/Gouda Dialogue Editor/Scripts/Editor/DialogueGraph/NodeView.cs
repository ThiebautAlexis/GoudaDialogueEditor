using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GoudaDialogueEditor.Editor
{
    public class NodeView: UnityEditor.Experimental.GraphView.Node
    {
        #region Events 
        public static event Action<NodeView> OnNodeSelected;
        #endregion

        #region Fields and Properties
        public Node node;
        public Port input;
        public Port output;
        public VisualElement contentElementContainer;
        public DialogueAsset dialogueAsset; 
        #endregion

        #region Methods 
        public NodeView(Node _node, DialogueAsset _asset) : base("Assets/Gouda Dialogue Editor/Data/Stylesheets/NodeView.uxml")
        {
            dialogueAsset = _asset;

            this.node = _node;
            this.title = _node.name;
            this.viewDataKey = _node.Guid;
            style.left = _node.Position.x;
            style.top = _node.Position.y;

            CreateInputPorts();
            CreateOutputPorts();
            CreateContent();
            AddToClassList(node.ClassListName);     
        }


        private void CreateInputPorts()
        {
            RootNode _rootNode = node as RootNode;
            if (_rootNode)
                return;

            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
            if(input != null)
            {
                input.portName = string.Empty;
                inputContainer.Add(input);
            }
        }

        private void CreateOutputPorts()
        {
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            if(output != null)
            {
                output.portName = string.Empty;
                outputContainer.Add(output);
            }
        }

        private void CreateContent()
        {
            // Get Container
            contentElementContainer = this.Q("contentElement");
            
            DialogueNode _dialogueNode = node as DialogueNode;
            if(_dialogueNode)
            {
                int[] linesKeys = _dialogueNode.LineKeys;
                for (int i = 0; i < linesKeys.Length; i++)
                {
                    Label _label = new Label(dialogueAsset.GetLineFromID(linesKeys[i]));
                    _label.style.whiteSpace = WhiteSpace.Normal;            
                    contentElementContainer.Add(_label);
                }
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.Position.Set(newPos.xMin, newPos.yMin);
        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }
        #endregion
    }
}
