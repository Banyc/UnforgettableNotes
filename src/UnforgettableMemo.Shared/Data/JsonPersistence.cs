using System.IO;
using System.Collections.Generic;
using UnforgettableMemo.Shared.Models;
using System.Text.Json;

namespace UnforgettableMemo.Shared.Data
{
    public class JsonPersistence : IMemoPersistence
    {
        private string FilePath { get; }
        private string Filename { get; }
        public JsonPersistence(string path, string filename = "memos.json")
        {
            this.FilePath = path;
            this.Filename = filename;
        }
        public List<Memo> Load()
        {
            string fileText = File.ReadAllText(Path.Combine(this.FilePath, this.Filename));
            List<Memo> memos = JsonSerializer.Deserialize<List<Memo>>(fileText);
            return memos;
        }

        public void Save(List<Memo> memos)
        {
            string fileText = JsonSerializer.Serialize(memos);
            File.WriteAllText(Path.Combine(this.FilePath, this.Filename), fileText);
        }
    }
}
