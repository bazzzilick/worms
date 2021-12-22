using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worms
{
    class Worm
    {
        const int START_LIFE_STRENGTH = 10;

        public int lifeStrength;
        public string name { get; set; }
        public Point pos { get; set; }

        public Worm(string name, Point pos)
        {
            lifeStrength = START_LIFE_STRENGTH;
            this.name = name;
            this.pos = pos;
        }

        public NextStep GetNextStep(List<Food> foods, List<Worm> worms)
        {
            if (this.lifeStrength >= 20)
            {
                return NextStep.REPRODUCTION;
            }
            
            int targetX = 0, targetY = 0, distance; 
            int minDistance = int.MaxValue;
            foreach (Food food in foods)
            {
                distance = Math.Abs(this.pos.x - food.pos.x) +
                            Math.Abs(this.pos.y - food.pos.y);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    targetX = food.pos.x;
                    targetY = food.pos.y;
                } 
            }
            int xDistance = this.pos.x - targetX; 
            int yDistance = this.pos.y - targetY;
            if (Math.Abs(xDistance) > Math.Abs(yDistance))
            {
                if (xDistance < 0)
                    return NextStep.RIGHT;
                else
                    return NextStep.LEFT;
            }
            else
            {
                if (yDistance < 0)
                    return NextStep.UP;
                else
                    return NextStep.DOWN;
            }


            //сделать проверку на аут оф рендж
            //размножение!!!!
            //TODO
        }

        /*private NextStep Reproduction()
        {
            return NextStep.




        }*/

    }
}
