using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worms
{
    class Logger
    {
        private FileStream stream;


        public Logger(string path)
        {
            stream = new FileStream(path, FileMode.OpenOrCreate);
        }

        public void WriteTurnInfo(List<Worm> worms, List<Food> foods, int currentTurn)
        {
            string text = $"Step: {currentTurn} Worms: [";
            foreach (Worm worm in worms)
            {
                text += $" {worm.name}-{worm.lifeStrength} ({worm.pos.x},{worm.pos.y})";
            }
            text += "]  Foods: [";
            foreach(Food food in foods)
            {
                text += $" ({food.pos.x},{food.pos.y})";
            }
            text += "]\n";

            byte[] array = System.Text.Encoding.Default.GetBytes(text);
            stream.Write(array, 0, array.Length);

        }

    }
}
