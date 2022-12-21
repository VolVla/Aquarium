using System;
using System.Collections.Generic;

namespace Aquarium
{
    internal class Program
    {
        static void Main()
        {
            Aquarium aquarium = new Aquarium();
            aquarium.Start();
        }
    }

    class Aquarium
    {
        private const int NumberAddFish = 1;
        private const int NumberAddFishes = 2;
        private const int NumberRemoveFish = 3;
        private const int NumberRemoveFishes = 4;
        private const int Exit = 5;
        private const int MaximumNumberFish = 10;
        private const int MinimumNumberFish = 0;
        private List<Fish> _fishes = new List<Fish>();
        private bool _isWork;

        public Aquarium()
        {
            _isWork = true;
        }

        public void Start()
        {
            while (_isWork)
            {
                if (_fishes.Count != 0)
                {
                    InfoShow();
                }
                else
                {
                    Console.WriteLine("В вашем аквариуме еще нет рыбок");
                }

                Console.WriteLine($"Для добавления одной рыбки в аквариум напишите {NumberAddFish},для добаление нескольких {NumberAddFishes}\nДля того чтоб убрать рыбу из аквариума {NumberRemoveFish},для того чтоб убрать нескольких {NumberRemoveFishes}");
                Console.WriteLine($"Для мониторинга за рыбами нажмите на любую клавишу.Для выхода напишите {Exit}");
                int.TryParse(Console.ReadLine(), out int input);

                switch (input)
                {
                    case NumberAddFish:
                        if (_fishes.Count < MaximumNumberFish)
                        {
                            AddFish();
                        }
                        else
                        {
                            ErrorMessenge(true);
                        }
                        break;
                    case NumberAddFishes:
                        if (_fishes.Count < MaximumNumberFish)
                        {
                            AddFishes();
                        }
                        else
                        {
                            ErrorMessenge(true);
                        }
                        break;
                    case NumberRemoveFish:
                        if (_fishes.Count != 0)
                        {
                            PullFish();
                        }
                        else
                        {
                            ErrorMessenge(false);
                        }
                        break;
                    case NumberRemoveFishes:
                        if (_fishes.Count != 0)
                        {
                            PullFishes();
                        }
                        else
                        {
                            ErrorMessenge(false);
                        }
                        break;
                    case Exit:
                        _isWork = false;
                        break;
                    default:
                        TimeSkip();
                        break;
                }
            }
        }

        public void AddFish()
        {
            bool correctInput = true;

            while (correctInput)
            {
                Console.WriteLine("Введите число здоровья рыбы");
                bool isHealth = int.TryParse(Console.ReadLine(), out int health);
                Console.WriteLine("Введите сколько лет рыбе");
                bool isAge = int.TryParse(Console.ReadLine(), out int age);

                if ((isHealth & isAge) == true)
                {
                    Fish fish = new Fish(health, age);
                    _fishes.Add(fish);
                    correctInput = false;
                    Console.WriteLine("Вы добавили рыбку в аквариум");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Вы вели не корректное значение числа здоровья или сколько лет рыбке");
                }
            }
        }

        public void AddFishes()
        {
            Console.WriteLine($"Введите количество рыбок которых хотите добавить, максимальное кол-во место для рыбок в аквариуме {MaximumNumberFish - _fishes.Count}");
            int.TryParse(Console.ReadLine(), out int numberAddFish);

            if ((numberAddFish + _fishes.Count) <= MaximumNumberFish)
            {
                for (int i = 0; i < numberAddFish; i++)
                {
                    AddFish();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Вы  вели не корректное число или Вы пытаеть добавить рыбок больше чем влезает в аквариуму");
            }
        }

        public void PullFish()
        {
            bool correctNumber = true;
            InfoShow();
            Console.WriteLine("Введите номер рыбки,которую хотите вытащить из аквариума");

            while (correctNumber)
            {
                int.TryParse(Console.ReadLine(), out int index);

                if ((MinimumNumberFish < index) & (index <= _fishes.Count))
                {
                    _fishes.Remove(_fishes[index - 1]);
                    correctNumber = false;
                }
                else
                {
                    Console.WriteLine("Вы вели не корректный номер рыбки");
                }
            }

        }

        public void PullFishes()
        {
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"Введите кол-во рыб которых вы хотите вытащить из аквариума,всего рыбок в аквариуме {_fishes.Count}");
                int.TryParse(Console.ReadLine(), out int numberFish);

                if ((_fishes.Count - numberFish) >= MinimumNumberFish)
                {
                    for (int i = 0; i < numberFish; i++)
                    {
                        PullFish();
                    }

                    isWork = false;
                }
                else
                {
                    Console.WriteLine("Вы  вели не корректное число.");
                }
            }
        }

        public void TimeSkip()
        {
            int damageHealth = 2;

            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].Age(damageHealth);
            }
        }

        public void InfoShow()
        {
            string status;

            for (int i = 0; i < _fishes.Count; i++)
            {
                if (_fishes[i]._isAlive == true)
                {
                    status = "Жив";
                }
                else
                {
                    status = "Мертв";
                }

                Console.WriteLine($"Номер - {i + 1}, {_fishes[i].AgeFish} число лет {_fishes[i].Health} осталось жизней, Состояние - {status} ");
            }
        }

        public void ErrorMessenge(bool isFullAquarium)
        {
            if (isFullAquarium == true)
            {
                Console.Clear();
                Console.WriteLine("У вас переполнены аквариум");
            }
            else if (isFullAquarium == false)
            {
                Console.Clear();
                Console.WriteLine("Вы пытаетесь забрать рыбок больше чем вы имеете попробуйте ещё раз");
            }
        }
    }

    class Fish
    {
        public int Health { get; private set; }
        public int AgeFish { get; private set; }

        public bool _isAlive;

        public Fish(int health, int age)
        {
            Health = health;
            AgeFish = age;
            _isAlive = true;
        }

        public void Age(int damage)
        {
            if (_isAlive == true)
            {
                Health -= damage;
                if (Health <= 0)
                {
                    _isAlive = false;
                    return;
                }
                AgeFish++;
            }
        }
    }
}
