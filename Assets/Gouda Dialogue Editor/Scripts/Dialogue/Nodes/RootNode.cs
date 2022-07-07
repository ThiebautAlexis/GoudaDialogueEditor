using System;
using UnityEngine;

namespace GoudaDialogueEditor
{
    public class RootNode : Node
    {
        #region Fields and Properties
        public Node LinkedNode;

        public override string ClassListName { get; protected set; } = "root";
        #endregion

        #region Methods 

        protected override void OnEndNode()
        {
        }

        protected override void OnEnterNode(DialogueAsset _asset)
        {
        }

        #endregion
    }
}
