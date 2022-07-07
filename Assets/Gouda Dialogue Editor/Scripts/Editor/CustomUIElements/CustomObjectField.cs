using System;
using UnityEngine;
using UnityEditor.UIElements;

namespace GoudaDialogueEditor.Editor
{
    public class CustomObjectField<T> : ObjectField
    {
        #region Fields and Properties

        #endregion

        #region Constructor
        public CustomObjectField() : base()
        {
            this.objectType = typeof(T);
        }

        public CustomObjectField(string label) : base(label)
        {
            this.objectType = typeof(T);
        }
        #endregion

        #region Methods 
        #endregion
    }
}
