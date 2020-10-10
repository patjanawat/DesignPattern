using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Momemto
{
    public class CareTaker<T>
    {
        public CareTaker(){
            _histories = new List<EditorState<T>>();
            lastIndex = -1;
        }
        private IList<EditorState<T>> _histories;
        private int lastIndex;

        public void PushState(EditorState<T> state){
            for (int i = lastIndex + 1; i < _histories.Count; i++)
            {
                _histories.RemoveAt(i);
            }
            
            _histories.Add(state);
            lastIndex +=1;
        }

        public EditorState<T> Undo(){
            return _histories[--lastIndex];
        }

        public EditorState<T> Redo(){
            return _histories[++lastIndex];
        }

        public void Reset()
        {
            _histories = new List<EditorState<T>>();
            lastIndex = -1;
        }

        public int Index => lastIndex;
        public List<EditorState<T>> States => (List<EditorState<T>>)_histories;
    }
}