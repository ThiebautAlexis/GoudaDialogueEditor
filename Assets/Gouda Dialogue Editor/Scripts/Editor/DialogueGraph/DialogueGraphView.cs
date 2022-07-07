using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

namespace GoudaDialogueEditor.Editor
{
    public class DialogueGraphView : GraphView
    {
        #region Fields and Properties
        private DialogueAsset dialogueAsset;
        public DialogueAsset DialogueAsset => dialogueAsset;
        #endregion

        #region Constructors 
        public new class UxmlFactory : UxmlFactory<DialogueGraphView, GraphView.UxmlTraits> { }

        public DialogueGraphView()
        {
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Gouda Dialogue Editor/Data/Stylesheets/DialogueGraph.uss");
            styleSheets.Add(styleSheet);


        }
        #endregion

        #region Methods
        internal void PopulateView(DialogueAsset _dialogueAsset)
        {
            this.dialogueAsset = _dialogueAsset;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;
            if(_dialogueAsset.RootNode == null)
            {
                _dialogueAsset.RootNode = _dialogueAsset.CreateNode(typeof(RootNode), Vector2.zero) as RootNode;
            }
            CreateNodeView(_dialogueAsset.RootNode);
            foreach (var node in dialogueAsset.nodes)
            {
                // Create Node Views
                CreateNodeView(node);
            }

            List<Node> linkedNodes = dialogueAsset.GetLinkedNodes(_dialogueAsset.RootNode);
            if(linkedNodes.Count > 0)
            {
                NodeView _nodeView = GetNodeByGuid(_dialogueAsset.RootNode.Guid) as NodeView;
                NodeView _linkedView = GetNodeByGuid(linkedNodes[0].Guid) as NodeView;

                Edge edge = _nodeView.output.ConnectTo(_linkedView.input);
                AddElement(edge);
            }

            foreach (var node in dialogueAsset.nodes)
            {
                // Create Edges
                linkedNodes = dialogueAsset.GetLinkedNodes(node);

                foreach (Node linkedNode in linkedNodes)
                {
                    NodeView _nodeView = GetNodeByGuid(node.Guid) as NodeView;
                    NodeView _linkedView = GetNodeByGuid(linkedNode.Guid) as NodeView;

                    Edge edge = _nodeView.output.ConnectTo(_linkedView.input);
                    AddElement(edge);
                }

            }
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if(graphViewChange.elementsToRemove != null)
            {
                foreach (var element in graphViewChange.elementsToRemove)
                {
                    NodeView _nodeView = element as NodeView;
                    if (_nodeView != null)
                    {
                        dialogueAsset.DeleteNode(_nodeView.node);
                    }
                    Edge _edge = element as Edge;
                    if(_edge != null)
                    {
                        NodeView fromView = _edge.output.node as NodeView;
                        NodeView toView = _edge.input.node as NodeView;
                        dialogueAsset.DelinkNodes(fromView.node, toView.node);
                    }
                }
            }
            if(graphViewChange.edgesToCreate != null)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    NodeView fromView = edge.output.node as NodeView;
                    NodeView toView = edge.input.node as NodeView;
                    dialogueAsset.LinkNodes(fromView.node, toView.node);
                }
            }
            return graphViewChange;
        }

        private void CreateNodeView(Node _node)
        {
            NodeView nodeView = new NodeView(_node, dialogueAsset);
            AddElement(nodeView);   
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            Vector2 _position = evt.localMousePosition;
            base.BuildContextualMenu(evt);
            var _types = TypeCache.GetTypesDerivedFrom<DialogueNode>();
            foreach (var type in _types)
            {
                evt.menu.AppendAction($"Node/{type.BaseType.Name}/{type.Name}", (action) => CreateNode(type, _position));
            }

            /// Local Method /// 
            void CreateNode(Type _type, Vector2 _position)
            {
                Node _node = dialogueAsset.CreateNode(_type, _position);
                CreateNodeView(_node);
            }

        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();
            foreach (var port in ports)
            {
                if (port.direction != startPort.direction &&
                   port.node != startPort.node)
                    compatiblePorts.Add(port);
            }
            return compatiblePorts;
            // return base.GetCompatiblePorts(startPort, nodeAdapter);
        }
        #endregion
    }
}
