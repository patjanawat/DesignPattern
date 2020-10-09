using Newtonsoft.Json;

namespace Momemto
{
    public class EditorState<T>
    {
        public string Type {get;}
        private readonly string _serializedData;
        public T Object => JsonConvert.DeserializeObject<T>(_serializedData);

        public EditorState(T serializeObject){
            Type = typeof(T).ToString();
            _serializedData = JsonConvert.SerializeObject(serializeObject);
        }
    }
}