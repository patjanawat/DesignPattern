using System;

namespace Momemto
{
    public class Editor
    {
        public Editor(string title, string text, int fontSize, string fontFace)
        {
            Title = title;
            Text = text;
            FontSize = fontSize;
            FontFace = fontFace;
        }

        public string Title { get; private set; }
        public string Text { get; private set; }
        public int FontSize { get; private set; }
        public string FontFace { get; private set; }

        public EditorState<Editor> CreateState(){
            return new EditorState<Editor>(this);
        }

        public void RestoreState(EditorState<Editor> editorState){
            Title = editorState.Object.Title;
            Text = editorState.Object.Text;
            FontFace = editorState.Object.FontFace;
            FontSize = editorState.Object.FontSize;
        }

        public override string ToString()
        {
            return $"{Title} {Text} {FontFace} {FontSize}";
        }
    }
}
