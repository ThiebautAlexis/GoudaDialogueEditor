using System;
using UnityEngine;

namespace GoudaDialogueEditor
{
    public abstract class DialogueNode : Node
    {
        #region Fields and Properties
        public Node LinkedNode;

        [SerializeField] protected int[] linesKeys = new int[1] {-1};
        public int[] LineKeys => linesKeys;

        public override string ClassListName { get; protected set; } = "dialogueNode";
        #endregion

        #region Methods 

        protected override void OnEnterNode(DialogueAsset _asset)
        {

        }

        protected override void OnEndNode()
        {
        }
        #endregion
    }
}
