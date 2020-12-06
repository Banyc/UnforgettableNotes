using System.IO;
using System.Collections.Generic;
using UnforgettableMemo.Shared.Models;
using System.Text.Json;

namespace UnforgettableMemo.Shared.Data
{
    public class JsonPersistence : IMemoPersistence
    {
        private string FileDirctory { get; }
        private string Filename { get; }
        public JsonPersistence(string fileDirctory, string filename = "memos.json")
        {
            this.FileDirctory = fileDirctory;
            this.Filename = filename;
        }
        public List<Memo> Load()
        {
            string filePath = Path.Combine(this.FileDirctory, this.Filename);
            if (!File.Exists(filePath))
            {
                return new List<Memo>();
            }
            string fileText = File.ReadAllText(filePath);
            List<Memo> memos = JsonSerializer.Deserialize<List<Memo>>(fileText);
            return memos;
        }

        public void Save(List<Memo> memos)
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
