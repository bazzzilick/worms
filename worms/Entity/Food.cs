using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worms
{
    class Food
    {
        const int SHELF_LIFE_TIME = 10;

        public int expireIn;
        public Point pos { get; set; }

        public Food(Point pos)
        {
            expireIn = SHELF_LIFE_TIME;
            this.pos = pos;
        }

    }
}
