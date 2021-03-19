using System.IO;
using System.Collections.Generic;
using UnforgettableMemo.Shared.Models;
using System.Text.Json;

namespace UnforgettableMemo.Shared.Data
{
    public class JsonPersistence<T> : IPersistence<T>
    {
        private string FileDirctory { get; }
        private string Filename { get; }
        public JsonPersistence(string fileDirctory, string filename)
        {
            this.FileDirctory = fileDirctory;
            this.Filename = filename;
        }
        public T Load()
        {
            string filePath = Path.Combine(this.FileDirctory, this.Filename);
            if (!File.Exists(filePath))
            {
                return default;
            }
            string fileText = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(fileText);
        }

        public void Save(T memos)
        {
            Directory.CreateDirectory(this.FileDirctory);
            string fileText = JsonSerializer.Serialize(memos, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            File.WriteAllText(Path.Combine(this.FileDirctory, this.Filename), fileText);
        }
    }
}
