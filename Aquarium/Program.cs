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
        private const string NumberAddFish = "1";
        private const string NumberAddFishes = "2";
        private const string NumberRemoveFish = "3";
        private const string NumberRemoveFishes = "4";
        private const string Exit = "5";
        private int _maximumNumberFish = 10;
        private int _minimumNumberFish = 0;
        private List<Fish> _fishes = new List<Fish>();
        private bool _isWork = true;

        public void Start()
        {
            while (_isWork)
            {
                if (_fishes.Count != 0)
                {
                    ShowInfo();
                }
                else
                {
                    Console.WriteLine("В вашем аквариуме еще нет рыбок");
                }

                Console.WriteLine($"Для добавления одной рыбки в аквариум напишите {NumberAddFish},для добаление нескольких {NumberAddFishes}");
                Console.WriteLine($"Для того чтоб убрать рыбу из аквариума {NumberRemoveFish},для того чтоб убрать нескольких {NumberRemoveFishes}");
                Console.WriteLine($"Для мониторинга за рыбами нажмите на любую клавишу. Для выхода напишите {Exit}");

                switch (Console.ReadLine())
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
                    case Exit:
                        _isWork = false;
                        break;
                    default:
                        SkipTime();
                        break;
                }
            }
        }

        private void AddFish()
        {
            if (_fishes.Count <= _maximumNumberFish)
            {
                Console.WriteLine("Введите число здоровья рыбы");
                bool isCorrectHealth = int.TryParse(Console.ReadLine(), out int health);
                Console.WriteLine("Введите сколько лет рыбе");
                bool isCorrectAge = int.TryParse(Console.ReadLine(), out int age);

                if ((isCorrectHealth & isCorrectAge) == true)
                {
                    Fish fish = new Fish(health, age);
                    _fishes.Add(fish);
                    Console.WriteLine("Вы добавили рыбку в аквариум");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Вы вели не корректное значение числа здоровья или сколько лет рыбке");
                }
            }
            else
            {
                Console.WriteLine("Вы пытаетесь запихнуть больше рыбок чем влазит в аквариум так нельзя");
            }
        }

        private void AddFishes()
        {
            if (_fishes.Count <= _maximumNumberFish)
            {
                Console.WriteLine($"Введите количество рыбок которых хотите добавить, максимальное кол-во место для рыбок в аквариуме {_maximumNumberFish - _fishes.Count}");
                int.TryParse(Console.ReadLine(), out int numberAddFish);

                if ((numberAddFish + _fishes.Count) <= _maximumNumberFish)
                {
                    for (int i = 0; i < numberAddFish; i++)
                    {
                        AddFish();
                    }
                }
            }
            else
            {
                Console.WriteLine("Вы пытаетесь запихнуть больше рыбок чем влазит в аквариум так нельзя");
            }
        }

        private void PullFish()
        {
            if (_fishes.Count != 0)
            {
                ShowInfo();
                Console.WriteLine("Введите номер рыбки,которую хотите вытащить из аквариума");
                int.TryParse(Console.ReadLine(), out int index);

                if ((_minimumNumberFish < index) & (index <= _fishes.Count))
                {
                    _fishes.RemoveAt(index - 1);
                }
                else
                {
                    Console.WriteLine("Вы вели не корректный номер рыбки");
                }
            }
        }

        private void PullFishes()
        {
            if (_fishes.Count != 0)
            {
                Console.WriteLine($"Введите кол-во рыб которых вы хотите вытащить из аквариума,всего рыбок в аквариуме {_fishes.Count}");
                int.TryParse(Console.ReadLine(), out int numberFish);

                if ((_fishes.Count - numberFish) >= _minimumNumberFish)
                {
                    for (int i = 0; i < numberFish; i++)
                    {
                        PullFish();
                    }
                }
                else
                {
                    Console.WriteLine("Вы  вели не корректное число");
                }
            }
            else
            {
                Console.WriteLine("Вы пытаетесь вытащить больше рыбок чем есть в наличии в аквариуме");
            }
        }

        private void SkipTime()
        {
            int damageHealth = 2;

            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].Age(damageHealth);
            }
        }

        private void ShowInfo()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                Console.WriteLine($"Номер - {i + 1}, {_fishes[i].NumberAge} число лет {_fishes[i].Health} осталось жизней, Состояние - {_fishes[i].Status} ");
            }
        }
    }

    class Fish
    {
        private bool _isAlive;

        public Fish(int health, int age)
        {
            Health = health;
            NumberAge = age;
            _isAlive = true;
            Status = "Жив";
        }

        public string Status { get; private set; }
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
                    Status = "Мертв";
                    return;
                }

                NumberAge++;
            }
        }
    }
}
