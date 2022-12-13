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
        private const string _NumberAddFish = "1";
        private const string _NumberRemoveFish = "2";
        private const string _Exit = "3";
        private List<Fish> _fish = new List<Fish>();
        private int _maximumNumberFish = 10;
        private int _minimumNumberFish = 0;
        private bool _isWork = true;

        public void Start()
        {
            while (_isWork)
            {
                if (_fish.Count != 0)
                {
                    ShowInfo();
                }
                else
                {
                    Console.WriteLine("В вашем аквариуме еще нет рыбок");
                }

                Console.WriteLine($"Для добавления рыб аквариум напишите {_NumberAddFish}, для того чтоб убрать рыбу из аквариума {_NumberRemoveFish}");
                Console.WriteLine($"Для мониторинга за рыбами нажмите на любую клавишу.Для выхода напишите {_Exit}");

                switch (Console.ReadLine())
                {
                    case _NumberAddFish:
                        AddFishes();
                        break;
                    case _NumberRemoveFish:
                        GetFishes();
                        break;
                    case _Exit:
                        _isWork = false;
                        break;
                    default:
                        SkipTime();
                        break;
                }
            }
        }

        public void AddFishes()
        {
            if (_fish.Count < _maximumNumberFish)
            {
                Console.WriteLine($"Введите количество рыбок которых хотите добавить, максимальное кол-во рыбок в аквариуме {_maximumNumberFish}");
                int.TryParse(Console.ReadLine(), out int numberAddFish);

                if ((numberAddFish + _fish.Count) <= _maximumNumberFish)
                {
                    for (int i = 0; i < numberAddFish; i++)
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
                                _fish.Add(fish);
                                Console.WriteLine("Вы добавили рыбку в аквариум");
                                correctInput = false;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Вы вели не корректное значение числа здоровья или сколько лет рыбке");
                            }
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Вы  вели не корректное число или Вы пытаеть добавить рыбок больше чем влезает в аквариуму");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("У вас переполнены аквариум");
            }
        }

        public void GetFishes()
        {
            if (_fish.Count != 0)
            {
                bool isWork = true;

                while (isWork)
                {
                    Console.WriteLine("Введите кол-во рыб которых вы хотите вытащить из аквариума");
                    int.TryParse(Console.ReadLine(), out int numberFish);

                    if ((_fish.Count - numberFish) >= _minimumNumberFish)
                    {
                        for (int i = 0; i <= numberFish; i++)
                        {
                            bool correctNumber = true;
                            ShowInfo();
                            Console.WriteLine("Введите номер рыбки,которую хотите вытащить из аквариума");

                            while (correctNumber)
                            {
                                int.TryParse(Console.ReadLine(), out int index);

                                if ((_minimumNumberFish <= index) & (index <= _fish.Count))
                                {
                                    _fish.Remove(_fish[index - 1]);
                                    correctNumber = false;
                                }
                                else
                                {
                                    Console.WriteLine("Вы вели не корректный номер рыбки");
                                }
                            }
                        }

                        isWork = false;
                    }
                    else
                    {
                        Console.WriteLine("Вы  вели не корректное число.");
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Вы пытаетесь забрать  рыбок больше чем вы имеете попробуйте ещё раз");
            }
        }

        public void SkipTime()
        {
            int damageHealth = 2;

            for (int i = 0; i < _fish.Count; i++)
            {
                _fish[i].Aging();
                _fish[i].LossHealth(damageHealth);
                _fish[i].ChangedStatus();
            }
        }

        public void ShowInfo()
        {
            for (int i = 0; i < _fish.Count; i++)
            {
                Console.WriteLine($"Номер - {i + 1}, {_fish[i].Age} число лет {_fish[i].Health} осталось жизней, Состояние - {_fish[i].Status} ");
            }
        }
    }

    class Fish
    {
        private string _statusFirst = "Мертв";
        private string _statusSecond = "Жив";

        public int Health { get; private set; }
        public int Age { get; private set; }
        public string Status { get; private set; }

        public Fish(int health, int age)
        {
            Health = health;
            Age = age;
            Status = _statusSecond;
        }

        public void Aging()
        {
            if (Status == _statusSecond)
            {
                Age++;
            }
        }

        public void LossHealth(int damage)
        {
            if (Status == _statusSecond)
            {
                Health -= damage;
            }
        }

        public void ChangedStatus()
        {
            if (Health <= 0)
            {
                Status = _statusFirst;
            }
            else
            {
                Status = _statusSecond;
            }
        }
    }
}
