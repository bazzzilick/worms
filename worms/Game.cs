using System;
using System.Threading;
using System.Collections.Generic;

namespace worms
{
    class Game
    {
        static Random random;
        const int size = 20;
        const int numOfTurn = 100;
        private int curentTurn;
        private PlayField playField { get; set; }
        private List<Worm> worms;
        private List<Food> foods;
        private Logger logger;

        public Game()
        {
            random = new Random();
            curentTurn = 0;
            playField = new PlayField(size);
            worms = new List<Worm>();
            foods = new List<Food>();
            logger = new Logger("D:/MyProjects/LabsC#/worms/worms/Resourses/GameProgress.txt");
        }

        public void Start()
        {
            playField.Init();

            var worm = new Worm("Jeembo", new Point(0, 0, FieldState.WORM));
            worms.Add(worm);
            playField.SetFieldState(worm.pos.x, worm.pos.y, worm.pos.state);

            GameLoop();

        }

        private void GameLoop()
        {

            for (; curentTurn < numOfTurn; curentTurn++)
            {
                while (true)
                {
                    int x = GetFoodRandomCoord(random);
                    int y = GetFoodRandomCoord(random);
                    if (playField.IsOutOfRange(x, y))
                        continue;

                    if (playField.GetFieldState(x, y) == FieldState.EMPTY)
                    {
                        var food = new Food(new Point(x, y, FieldState.FOOD));
                        foods.Add(food);
                        playField.SetFieldState(x, y, FieldState.FOOD);
                        break;
                    }
                    else
                    {
                        if (playField.GetFieldState(x, y) == FieldState.WORM)
                        {
                            foreach(Worm worm in worms)
                            {
                                if (worm.pos.x == x && worm.pos.y == y)
                                {
                                    worm.lifeStrength += 10;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                var newWorms = new List<Worm>();
                var deletedWorms = new List<Worm>();

                foreach (Worm worm in worms)
                {
                    if (worm.lifeStrength == 0)
                    {
                        playField.SetFieldState(worm.pos.x, worm.pos.y, FieldState.EMPTY);
                        deletedWorms.Add(worm);
                        continue;
                    }
                    worm.lifeStrength--;

                    var nextStep = worm.GetNextStep(foods, worms);

                    switch (nextStep)
                    {
                        case NextStep.UP:
                            switch (playField.GetFieldState(worm.pos.x, worm.pos.y + 1))
                            {
                                case FieldState.EMPTY:
                                    playField.SetFieldState(worm.pos.x, worm.pos.y + 1, FieldState.WORM);
                                    playField.SetFieldState(worm.pos.x, worm.pos.y, FieldState.EMPTY);
                                    worm.pos.y++;
                                    break;
                                case FieldState.WORM:
                                    break;
                                case FieldState.FOOD:
                                    playField.SetFieldState(worm.pos.x, worm.pos.y + 1, FieldState.WORM);
                                    playField.SetFieldState(worm.pos.x, worm.pos.y, FieldState.EMPTY);
                                    worm.pos.y++;
                                    worm.lifeStrength += 10;
                                    RemoveFood(worm.pos.x, worm.pos.y);
                                    break;
                            }
                            break;
                        case NextStep.DOWN:
                            switch (playField.GetFieldState(worm.pos.x, worm.pos.y - 1))
                            {
                                case FieldState.EMPTY:
                                    playField.SetFieldState(worm.pos.x, worm.pos.y - 1, FieldState.WORM);
                                    playField.SetFieldState(worm.pos.x, worm.pos.y, FieldState.EMPTY);
                                    worm.pos.y--;
                                    break;
                                case FieldState.WORM:
                                    break;
                                case FieldState.FOOD:
                                    playField.SetFieldState(worm.pos.x, worm.pos.y - 1, FieldState.WORM);
                                    playField.SetFieldState(worm.pos.x, worm.pos.y, FieldState.EMPTY);
                                    worm.pos.y--;
                                    worm.lifeStrength += 10;
                                    RemoveFood(worm.pos.x, worm.pos.y);
                                    break;
                            }
                            break;
                        case NextStep.RIGHT:
                            switch (playField.GetFieldState(worm.pos.x + 1, worm.pos.y))
                            {
                                case FieldState.EMPTY:
                                    playField.SetFieldState(worm.pos.x + 1, worm.pos.y, FieldState.WORM);
                                    playField.SetFieldState(worm.pos.x, worm.pos.y, FieldState.EMPTY);
                                    worm.pos.x++;
                                    break;
                                case FieldState.WORM:
                                    break;
                                case FieldState.FOOD:
                                    playField.SetFieldState(worm.pos.x + 1, worm.pos.y, FieldState.WORM);
                                    playField.SetFieldState(worm.pos.x, worm.pos.y, FieldState.EMPTY);
                                    worm.pos.x++;
                                    worm.lifeStrength += 10;
                                    RemoveFood(worm.pos.x, worm.pos.y);
                                    break;
                            }
                            break;
                        case NextStep.LEFT:
                            switch (playField.GetFieldState(worm.pos.x - 1, worm.pos.y))
                            {
                                case FieldState.EMPTY:
                                    playField.SetFieldState(worm.pos.x - 1, worm.pos.y, FieldState.WORM);
                                    playField.SetFieldState(worm.pos.x, worm.pos.y, FieldState.EMPTY);
                                    worm.pos.x--;
                                    break;
                                case FieldState.WORM:
                                    break;
                                case FieldState.FOOD:
                                    playField.SetFieldState(worm.pos.x - 1, worm.pos.y, FieldState.WORM);
                                    playField.SetFieldState(worm.pos.x, worm.pos.y, FieldState.EMPTY);
                                    worm.pos.x--;
                                    worm.lifeStrength += 10;
                                    RemoveFood(worm.pos.x, worm.pos.y);
                                    break;
                            }
                            break;
                        case NextStep.INPLACE:
                            break;
                        case NextStep.REPRODUCTION:
                            switch (playField.GetFieldState(worm.pos.x, worm.pos.y + 1))
                            {
                                case FieldState.EMPTY:
                                    newWorms.Add(new Worm($"Jeembo{worms.Count}", new Point(worm.pos.x, worm.pos.y + 1, FieldState.WORM)));
                                    playField.SetFieldState(worm.pos.x, worm.pos.y + 1, FieldState.WORM);
                                    worm.lifeStrength -= 10;
                                    break;
                                case FieldState.WORM:
                                    break;
                                case FieldState.FOOD:
                                    break;
                            }
                            break;
                    }


                }

                foreach(Worm worm in deletedWorms)
                {
                    worms.Remove(worm);
                }

                foreach(Worm worm in newWorms)
                {
                    worms.Add(worm);
                }
                

                Food rottenFood = null;
                foreach(Food food in foods)
                {
                    food.expireIn--;
                    if(food.expireIn == 0)
                    {
                        rottenFood = food;
                        playField.SetFieldState(rottenFood.pos.x, rottenFood.pos.y, FieldState.EMPTY);
                    }
                }
                foods.Remove(rottenFood);

                logger.WriteTurnInfo(worms, foods, curentTurn);


                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine($"TURN: {curentTurn}");
                PrintPlayField();

            }

        }

        private static int GetFoodRandomCoord(Random r)
        {
            const int mu = 0;
            const int sigma = 5;
            var u1 = r.NextDouble();
            var u2 = r.NextDouble();
            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            var randNormal = mu + sigma * randStdNormal;
            return (int)Math.Round(randNormal);
        }

        private void RemoveFood(int x, int y)
        {
            foreach(Food food in foods)
            {
                if (food.pos.x == x && food.pos.y == y)
                {
                    foods.Remove(food);
                    break;
                }
            }
        }

        private void PrintPlayField() 
        {
            for(int i = (size/2); i > -((size/2)+1); i--)
            {
                for(int j = -(size / 2); j < (size / 2) + 1; j++)
                {
                    switch(playField.GetFieldState(j, i))
                    {
                        case FieldState.EMPTY:
                            Console.Write(" ");
                            break;
                        case FieldState.WORM:
                            Console.Write("W");
                            break;
                        case FieldState.FOOD:
                            Console.Write("X");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

    }
}