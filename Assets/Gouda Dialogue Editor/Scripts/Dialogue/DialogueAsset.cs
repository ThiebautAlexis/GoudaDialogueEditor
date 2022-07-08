using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace GoudaDialogueEditor
{ 
    [CreateAssetMenu(fileName = "Dialogue_", menuName = "Gouda/Dialogue Asset", order = 150)]
    public class DialogueAsset : ScriptableObject
    {
        public static string DatabaseName = "Database";

        #region Fields and Properties
        public RootNode RootNode;
        public List<Node> nodes = new List<Node>();

        public DialogueDatabaseAsset[] Database = new DialogueDatabaseAsset[] { };
        #endregion

        #region Methods 

        #region Database 
        public string GetLineFromID(int _id)
        {
            for (int i = 0; i < Database.Length; i++)
            {
                for (int j = 0; j < Database[i].DialogueLines.Length; j++)
                {
                    if(Database[i].DialogueLines[j].LineID == _id)
                    {
                        return Database[i].DialogueLines[j].Lines[0];
                    }
                }
            }
            return string.Empty;
        }
        #endregion


        #region Nodes
        public Node CreateNode(Type _type, Vector2 _position)
        {
            Node _node = CreateInstance(_type) as Node;
            _node.name = _type.Name;
            _node.Guid = GUID.Generate().ToString();
            _node.Position = _position;

            if(_type != typeof(RootNode))
                nodes.Add(_node);

            AssetDatabase.AddObjectToAsset(_node, this);
            AssetDatabase.SaveAssets();
            return _node;
        }

        public void DeleteNode(Node _node)
        {
            nodes.Remove(_node);
            AssetDatabase.RemoveObjectFromAsset(_node);
            AssetDatabase.SaveAssets();
        }

        public void LinkNodes(Node _fromNode, Node _toNode)
        {
            SingleLineNode _actionNode = _fromNode as SingleLineNode;
            if (_actionNode)
            {
                _actionNode.LinkedNode = _toNode;
                return;
            }
            RootNode _rootNode = _fromNode as RootNode;
            if(_rootNode)
            {
                _rootNode.LinkedNode = _toNode;
                return;
            }
            
            throw new NotImplementedException();
        }

        public void DelinkNodes(Node _fromNode, Node _toNode)
        {
            SingleLineNode _actionNode = _fromNode as SingleLineNode;
            if (_actionNode)
            {
                _actionNode.LinkedNode = null;
                return;
            }
            RootNode _rootNode = _fromNode as RootNode;
            if (_rootNode)
            {
                _rootNode.LinkedNode = null;
                return;
            }
            SequentialNode _sequentialNode = _fromNode as SequentialNode;
            if (_sequentialNode)
            {
                _sequentialNode.LinkedNode = null;
                return;
            }
            throw new NotImplementedException();
        }

        public List<Node> GetLinkedNodes(Node _node)
        {
            List<Node> _linkedNodes = new List<Node>();
            SingleLineNode _singleLineNode = _node as SingleLineNode;
            if (_singleLineNode)
            {
                if( _singleLineNode.LinkedNode != null)
                    _linkedNodes.Add(_singleLineNode.LinkedNode);
                return _linkedNodes;
            }
            RootNode _rootNode = _node as RootNode;
            if(_rootNode)
            {
                if (_rootNode.LinkedNode != null)
                    _linkedNodes.Add(_rootNode.LinkedNode);
                return _linkedNodes;
            }
            SequentialNode _sequentialNode = _node as SequentialNode;
            if(_sequentialNode)
            {
                if (_sequentialNode.LinkedNode != null)
                    _linkedNodes.Add(_sequentialNode.LinkedNode);
                return _linkedNodes;
            }
            throw new NotImplementedException();

        }
        #endregion

        #endregion
    }
}
