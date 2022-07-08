using System;
using UnityEngine;

namespace GoudaDialogueEditor
{
    [Serializable]
    public class DialogueLine
    {
        public static readonly string KeyPropertyName = "LineKey";

        #region Fields and Properties
        public string LineKey;
        public int LineID;

        public string[] Lines = new string[] { };
        #endregion

        #region Constructor
        public DialogueLine(string _lineKey, string[] _lines)
        {
            LineKey = _lineKey;
            LineID = LineKey.GetHashCode();
            Lines = _lines;
        }
        #endregion


        #region Methods 
        #endregion
    }
}