using System;
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
                int menuNumber = GetNumber();
                Console.Clear();

                switch (menuNumber)
                {
                    case CommandCreatePlayer:
                        CreatePalyer(database);
                        break;

                    case CommandRemovePlayer:
                        RemovePalyer(database);
                        break;

                    case CommandBanedPalayer:
                        BanedPlayer(database);
                        break;

                    case CommandUnbanedPlayer:
                        UnbanedPlayer(database);
                        break;

                    case CommandShowAllPlayers:
                        database.ShowAllPlayers();
                        break;

                    case CommandExit:
                        isOpen = false;
                        continue;

                    default:
                        ShowError();
                        break;
                }

                Console.WriteLine("\nДля возврата в меню нажмите любую клавишу...");
                Console.ReadKey(true);
            }
        }

        static void ShowError()
        {
            Console.WriteLine("Вы ввели некорректное значение.");
        }

        static int GetNumber()
        {
            int number = 0;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out number))
                    return number;
                else
                    ShowError();
            }
        }

        static void CreatePalyer(Database database)
        {
            Console.Write("Введите Имя нового игрока: ");
            string playerName = Console.ReadLine();

            Console.Write("Введите  уровень нового игрока: ");
            int playerLevel = GetNumber();

            database.CreatePlayer(playerLevel, playerName);
        }

        static void RemovePalyer(Database database)
        {
            Console.Write("Введите id игрока: ");
            int id = GetNumber();

            database.RemovePlayer(id);
        }

        static void BanedPlayer(Database database)
        {
            Console.Write("Введите id игрока: ");
            int id = GetNumber();

            database.BanPlayer(id);
        }

        static void UnbanedPlayer(Database database)
        {
            Console.Write("Введите id игрока: ");
            int id = GetNumber();

            database.UnbanPlayer(id);
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

        public void ChangeBan(bool isBaned)
        {
            IsBaned = isBaned;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя игрока: {_name}, id: {Id}, уровень: {_level}\nЗабанен ли игрок: {IsBaned}\n");
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();
        private static int _uniqueId = 0;

        public Database() { }

        public void RemovePlayer(int id)
        {
            Player player;

            if (GetPlayer(id, out player))
                _players.Remove(player);
        }

        public void CreatePlayer(int level, string name)
        {
            _players.Add(new Player(++_uniqueId, level, name));
            Console.WriteLine("Игрок создан.");
        }

        public void BanPlayer(int id)
        {
            Player player;

            if (GetPlayer(id, out player))
            {
                if (player.IsBaned)
                {
                    Console.WriteLine("Данный игрок уже забанен.");
                }
                else
                {
                    player.ChangeBan(true);
                    Console.WriteLine("Бан игроку успешно выдан.");
                }
            }
        }

        public void UnbanPlayer(int id)
        {
            Player player;

            if (GetPlayer(id, out player))
            {
                if (player.IsBaned)
                {
                    player.ChangeBan(false);
                    Console.WriteLine("Игрок успешно разбанен.");
                }
                else
                {
                    Console.WriteLine("Данный игрок не был забанен.");
                }
            }
        }

        private bool GetPlayer(int id, out Player player)
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

        public void ShowAllPlayers()
        {
            foreach (Player player in _players)
                player.ShowInfo();
        }
    }
}
