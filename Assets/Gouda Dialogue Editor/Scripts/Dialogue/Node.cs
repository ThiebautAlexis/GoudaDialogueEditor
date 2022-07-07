using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoudaDialogueEditor
{
    public abstract class Node : ScriptableObject
    {
        #region Fields and Properties
        public string Guid;
        public Vector2 Position;
        public abstract string ClassListName { get; protected set; }
        #endregion

        #region Methods 
        protected abstract void OnEnterNode(DialogueAsset _asset);
        protected abstract void OnEndNode();
        #endregion
    }
}
