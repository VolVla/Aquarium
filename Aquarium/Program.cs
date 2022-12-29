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

        public void Start()
        {
            while (true)
            {
                if (_fishes.Count != 0)
                {
                    InfoShow();
                }
                else
                {
                    Console.WriteLine("В вашем аквариуме еще нет рыбок");
                }

                Console.WriteLine($"Для добавления одной рыбки в аквариум напишите {NumberAddFish},для добаление нескольких {NumberAddFishes}");
                Console.WriteLine($"Для того чтоб убрать рыбу из аквариума {NumberRemoveFish},для того чтоб убрать нескольких {NumberRemoveFishes}");
                Console.WriteLine($"Для мониторинга за рыбами нажмите на любую клавишу. Для выхода напишите {Exit}");
                int.TryParse(Console.ReadLine(), out int input);

                switch (input)
                {
                    case NumberAddFish:
                        AddFish();
                        break;
                    case NumberAddFishes:
                        AddFishes();
                        break;
                    case NumberRemoveFish:
                        PullFish();
                        break;
                    case NumberRemoveFishes:
                        PullFishes();
                        break;
                    default:
                        TimeSkip();
                        break;
                }

                if (input == Exit)
                {
                    break;
                }
            }
        }

        private void AddFish()
        {
            if (_fishes.Count < MaximumNumberFish)
            {
                bool isCorrectInput = true;

                while (isCorrectInput)
                {
                    Console.WriteLine("Введите число здоровья рыбы");
                    bool isCorrectHealth = int.TryParse(Console.ReadLine(), out int health);
                    Console.WriteLine("Введите сколько лет рыбе");
                    bool isCorrectAge = int.TryParse(Console.ReadLine(), out int age);

                    if ((isCorrectHealth & isCorrectAge) == true)
                    {
                        Fish fish = new Fish(health, age);
                        _fishes.Add(fish);
                        isCorrectInput = false;
                        Console.WriteLine("Вы добавили рыбку в аквариум");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Вы вели не корректное значение числа здоровья или сколько лет рыбке");
                    }
                }
            }
            else
            {
                ErrorMessenge(true);
            }
        }

        private void AddFishes()
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

        private void PullFish()
        {
            if (_fishes.Count != 0)
            {
                bool isCorrectNumber = true;
                InfoShow();
                Console.WriteLine("Введите номер рыбки,которую хотите вытащить из аквариума");

                while (isCorrectNumber)
                {
                    int.TryParse(Console.ReadLine(), out int index);

                    if ((MinimumNumberFish < index) & (index <= _fishes.Count))
                    {
                        _fishes.RemoveAt(index - 1);
                        isCorrectNumber = false;
                    }
                    else
                    {
                        Console.WriteLine("Вы вели не корректный номер рыбки");
                    }
                }
            }
            else
            {
                ErrorMessenge(false);
            }
        }

        private void PullFishes()
        {
            if (_fishes.Count != 0)
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
                        Console.WriteLine("Вы  вели не корректное число");
                    }
                }
            }
            else
            {
                ErrorMessenge(false);
            }
        }

        private void TimeSkip()
        {
            int damageHealth = 2;

            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].Age(damageHealth);
            }
        }

        private void InfoShow()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                Console.WriteLine($"Номер - {i + 1}, {_fishes[i].NumberAge} число лет {_fishes[i].Health} осталось жизней, Состояние - {_fishes[i].status} ");
            }
        }

        private void ErrorMessenge(bool isFullAquarium)
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
        public bool _isAlive;
        public string status;

        public Fish(int health, int age)
        {
            Health = health;
            NumberAge = age;
            _isAlive = true;
            status = "Жив";
        }

        public int Health { get; private set; }
        public int NumberAge { get; private set; }

        public void Age(int damage)
        {
            if (_isAlive == true)
            {
                Health -= damage;

                if (Health <= 0)
                {
                    _isAlive = false;
                    status = "Мертв";
                    return;
                }

                NumberAge++;
            }
        }
    }
}
