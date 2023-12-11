using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                this.IsBaned = isBaned;
                this.Id = -1;
            }

            public int Id { get; set; }

            public bool IsBaned { get; set; }

            public void ShowInfo()
            {
                Console.WriteLine($"Имя игрока: {_name}, id: {Id}, уровень: {_level}\nЗабанен ли игрок: {IsBaned}\n");
            }
        }

        class DatabasePlayers
        {
            private List<Player> players = new List<Player>();
            private static int uniqueId = 0;

            public DatabasePlayers() { }

            public void AddPlayer(Player player)
            {
                player.Id = ++uniqueId;
                players.Add(player);
                Console.WriteLine("Игрок добавлен в базу данных.");
            }

            public void RemovePlayer(Player player)
            {
                if (players.Contains(player))
                {
                    players.Remove(player);
                    Console.WriteLine("Игрок удален из базы данных.");
                }
                else
                {
                    Console.WriteLine("Такого игрока нет в базе данных.");
                }
            }

            public void BanPlayer(int id)
            {
                Player player = GetPlayer(id);

                if (player != null)
                {
                    if (player.IsBaned)
                    {
                        Console.WriteLine("Данный игрок уже забанен.");
                    }
                    else
                    {
                        player.IsBaned = true;
                        Console.WriteLine("Бан игроку успешно выдан.");
                    }
                }
            }

            public void UnbanPlayer(int id)
            {
                Player player = GetPlayer(id);

                if (player != null)
                {
                    if (player.IsBaned)
                    {
                        player.IsBaned = false;
                        Console.WriteLine("Игрок успешно разбанен.");
                    }
                    else
                    {
                        Console.WriteLine("Данный игрок не был забанен.");
                    }
                }
            }

            private Player GetPlayer(int id)
            {
                foreach (var player in players)
                    if (player.Id == id)
                        return player;

                Console.WriteLine("Не найдено игрока с таким id.");
                return null;
            }

            public void ShowAllPlayers()
            {
                foreach (var player in players)
                    player.ShowInfo();
            }
        }
    }
}
