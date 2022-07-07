using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GoudaDialogueEditor.Editor
{
    public class DialogueBlackboardView : VisualElement
    {
        #region Fields and Properties
        private SerializedObject dialogueAsset;
        #endregion

        #region Constructors
        public new class UxmlFactory : UxmlFactory<DialogueBlackboardView, VisualElement.UxmlTraits> { }

        public DialogueBlackboardView()
        {

        }
        #endregion

        #region Methods
        internal void PopulateBlackboard(DialogueAsset _dialogueAsset)
        {
            dialogueAsset = new SerializedObject(_dialogueAsset);

            ListView databaseListView = this.Q<ListView>("database-list");
            databaseListView.itemsSource = _dialogueAsset.Database;
            databaseListView.fixedItemHeight = EditorGUIUtility.singleLineHeight;
            databaseListView.makeItem = MakeDatabaseItem;
            databaseListView.bindItem = BindDatabaseItem;
            databaseListView.RefreshItems();

            /// Get Conditions Script here
            // ListView conditionsListView = this.Q<ListView>("condition-list");
        }

        private VisualElement MakeDatabaseItem()
        {
            CustomObjectField<DialogueDatabaseAsset> _objectField = new CustomObjectField<DialogueDatabaseAsset>();
            _objectField.allowSceneObjects = false;
            dialogueAsset.FindProperty(DialogueAsset.DatabaseName).InsertArrayElementAtIndex(dialogueAsset.FindProperty(DialogueAsset.DatabaseName).arraySize);
            return _objectField;
        }

        private void BindDatabaseItem(VisualElement _element, int _index) => (_element as ObjectField).BindProperty(dialogueAsset.FindProperty(DialogueAsset.DatabaseName).GetArrayElementAtIndex(_index));
        #endregion
    }
}
