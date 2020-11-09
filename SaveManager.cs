using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;


namespace Corneroids
{
    public class SaveManager
    {

        public delegate void Action();

        public SaveManager()
        {
            //Engine.OnStart += Save;
        }

        public void Save()
        {
            /*ItemSet set = new ItemSet();

            string text = JsonSerializer.Serialize<ItemSet>(set);

            set.BlocksCount = 0;

            Console.WriteLine(text + " /Saves");

            set = JsonSerializer.Deserialize<ItemSet>(text);

            Console.WriteLine(set.BlocksCount + " /Loaded");*/

            _ = SaveToFile();

            _ = LoadFromFile();
        }

        private async Task SaveToFile()
        {
            // сохранение данных
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                ItemSet tom = new ItemSet() { Name = "Tom", BlocksCount = 35 };

                await JsonSerializer.SerializeAsync<ItemSet>(fs, tom);
                Console.WriteLine("Data has been saved to file");
            }

        }

        private async Task LoadFromFile()
        {
           

            // чтение данных
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                ItemSet restoredPerson = await JsonSerializer.DeserializeAsync<ItemSet>(fs);

                Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.BlocksCount}");
            }
        }

    }

    public class ItemSet
    {
        public string Name { get; set; }
        public int BlocksCount { get; set; }
    }
}
