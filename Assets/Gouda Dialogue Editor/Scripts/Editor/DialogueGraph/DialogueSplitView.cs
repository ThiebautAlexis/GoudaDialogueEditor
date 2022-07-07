using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace GoudaDialogueEditor.Editor
{
    public class DialogueSplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<DialogueSplitView, TwoPaneSplitView.UxmlTraits> { }

    }
}
