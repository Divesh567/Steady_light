using System.Collections.Generic;
using UnityEngine;

namespace Immersiveorama.EditorTools.Immersiveorama.Notes.Runtime
{
    public class NotesDatabase : ScriptableObject
    {
        public List<NotesSO> notes = new List<NotesSO>();
    }
}