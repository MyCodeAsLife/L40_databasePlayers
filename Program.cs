using System;
using System.Collections.Generic;

namespace L40_databasePlayers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabasePlayers database = new DatabasePlayers();
            Player player1 = new Player(1, "Jon");
            Player palyer2 = new Player(3, "Sam");
            Player player3 = new Player(4, "Igor");

            database.AddPlayer(player1);
            database.AddPlayer(palyer2);

            Console.WriteLine();
            database.BanPlayer(1);

            Console.WriteLine();
            database.UnbanPlayer(2);

            Console.WriteLine();
            database.ShowAllPlayers();

            database.RemovePlayer(player3);
            database.RemovePlayer(player1);
        }

        class Player
        {
            private int _level;
            private string _name;

            public Player(int level, string name, bool isBaned = false)
            {
                _level = level;
                _name = name;
                IsBaned = isBaned;
                Id = -1;
            }

            public int Id { get; private set; }

            public bool IsBaned { get; private set; }

            public void SetId(int id)
            {
                Id = id;
            }

            public void ChangeBan(bool isBaned)
            {
                IsBaned = isBaned;
            }

            public void ShowInfo()
            {
                Console.WriteLine($"Имя игрока: {_name}, id: {Id}, уровень: {_level}\nЗабанен ли игрок: {IsBaned}\n");
            }
        }

        class DatabasePlayers
        {
            private List<Player> _players = new List<Player>();
            private static int _uniqueId = 0;

            public DatabasePlayers() { }

            public void AddPlayer(Player player)
            {
                player.SetId(++_uniqueId);
                _players.Add(player);
                Console.WriteLine("Игрок добавлен в базу данных.");
            }

            public void RemovePlayer(Player player)
            {
                if (_players.Contains(player))
                {
                    _players.Remove(player);
                    Console.WriteLine("Игрок удален из базы данных.");
                }
                else
                {
                    Console.WriteLine("Такого игрока нет в базе данных.");
                }
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

            private bool GetPlayer(int id, out Player getPlayer)
            {
                foreach (Player player in _players)
                    if (player.Id == id)
                    {
                        getPlayer = player;
                        return true;
                    }

                getPlayer = null;
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
}
