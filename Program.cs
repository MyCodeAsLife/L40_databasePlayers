﻿using System;
using System.Collections.Generic;

namespace L40_databasePlayers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int CommandCreatePlayer = 1;
            const int CommandRemovePlayer = 2;
            const int CommandBanedPalayer = 3;
            const int CommandUnbanedPlayer = 4;
            const int CommandShowAllPlayers = 5;
            const int CommandExit = 6;

            Database database = new Database();
            bool isOpen = true;

            database.CreatePlayer(1, "Jon");
            database.CreatePlayer(3, "Sam");
            database.CreatePlayer(4, "Igor");

            while (isOpen)
            {
                Console.Clear();
                Console.WriteLine($"{CommandCreatePlayer} - Создать игрока.\n{CommandRemovePlayer} - Удалить игрока по индексу.\n" +
                                  $"{CommandBanedPalayer} - Забанить игрока по индексу.\n{CommandUnbanedPlayer} - Разбанить " +
                                  $"игрока по индексу.\n{CommandShowAllPlayers} - Показать всех игроков.\n{CommandExit} - Выход из прогрммы.");

                Console.Write("Выбирете действие: ");

                if (int.TryParse(Console.ReadLine(), out int menuNumber))
                {
                    Console.Clear();

                    switch (menuNumber)
                    {
                        case CommandCreatePlayer:
                            database.CreatePlayer();
                            break;

                        case CommandRemovePlayer:
                            database.RemovePlayer();
                            break;

                        case CommandBanedPalayer:
                            database.BanPlayer();
                            break;

                        case CommandUnbanedPlayer:
                            database.UnbanPlayer();
                            break;

                        case CommandShowAllPlayers:
                            database.ShowAllPlayers();
                            break;

                        case CommandExit:
                            isOpen = false;
                            continue;

                        default:
                            Erorr.Show();
                            break;
                    }
                }
                else
                {
                    Erorr.Show();
                }

                Console.WriteLine("\nДля возврата в меню нажмите любую клавишу...");
                Console.ReadKey(true);
            }
        }
    }

    class Erorr
    {
        public static void Show()
        {
            Console.WriteLine("Вы ввели некорректное значение.");
        }
    }

    class Player
    {
        private int _level;
        private string _name;

        public Player(int id, int level, string name, bool isBaned = false)
        {
            _level = level;
            _name = name;
            IsBaned = isBaned;
            Id = id;
        }

        public int Id { get; private set; }

        public bool IsBaned { get; private set; }

        public void Baned()
        {
            IsBaned = true;
        }

        public void Unbaned()
        {
            IsBaned = false;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя игрока: {_name}, id: {Id}, уровень: {_level}\nЗабанен ли игрок: {IsBaned}\n");
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();
        private static int s_uniqueId = 0;

        public void RemovePlayer()
        {
            Console.Write("Введите id игрока: ");
            int id = GetNumber();

            if (TryGetPlayer(id, out Player player))
                _players.Remove(player);
        }

        public void CreatePlayer()
        {
            Console.Write("Введите Имя нового игрока: ");
            string name = Console.ReadLine();

            Console.Write("Введите  уровень нового игрока: ");
            int level = GetNumber();

            CreatePlayer(level, name);
        }

        public void CreatePlayer(int level, string name)
        {
            _players.Add(new Player(++s_uniqueId, level, name));
            Console.WriteLine("Игрок создан.");
        }

        public void BanPlayer()
        {
            Console.Write("Введите id игрока: ");
            int id = GetNumber();

            if (TryGetPlayer(id, out Player player))
            {
                if (player.IsBaned)
                {
                    Console.WriteLine("Данный игрок уже забанен.");
                }
                else
                {
                    player.Baned();
                    Console.WriteLine("Бан игроку успешно выдан.");
                }
            }
        }

        public void UnbanPlayer()
        {
            Console.Write("Введите id игрока: ");
            int id = GetNumber();

            if (TryGetPlayer(id, out Player player))
            {
                if (player.IsBaned)
                {
                    player.Unbaned();
                    Console.WriteLine("Игрок успешно разбанен.");
                }
                else
                {
                    Console.WriteLine("Данный игрок не был забанен.");
                }
            }
        }

        public void ShowAllPlayers()
        {
            foreach (Player player in _players)
                player.ShowInfo();
        }

        private bool TryGetPlayer(int id, out Player player)
        {
            foreach (Player playerToFind in _players)
                if (playerToFind.Id == id)
                {
                    player = playerToFind;
                    return true;
                }

            player = null;
            Console.WriteLine("Не найдено игрока с таким id.");
            return false;
        }

        private int GetNumber()
        {
            int number = 0;
            bool isNotCorrect = true;

            while (isNotCorrect)
            {
                if (int.TryParse(Console.ReadLine(), out number))
                    isNotCorrect = false;
                else
                    Erorr.Show();
            }

            return number;
        }
    }
}