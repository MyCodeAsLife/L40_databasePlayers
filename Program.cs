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
            Player player1 = new Player(1, "Jon");      // Не стал выделять уровни и имена в отдельные переменные,
            Player palyer2 = new Player(3, "Sam");      // так как здесь проверяется функционал классов, и весь код в методе Main является тестовым

            database.AddPlayer(player1);
            database.AddPlayer(palyer2);

            Console.WriteLine();
            database.BanPlayer(1);

            Console.WriteLine();
            database.UnbanPlayer(2);

            Console.WriteLine();
            database.ShowAllPlayers();
        }

        class Player
        {
            private int _id = -1;
            private int _level;
            private string _name;
            private bool _isBaned = false;

            public Player(int level, string name, bool isBaned = false)
            {
                _level = level;
                _name = name;
                _isBaned = isBaned;
            }

            public int Id
            {
                get
                {
                    return _id;
                }
                set
                {
                    _id = value;
                }
            }

            public bool IsBaned
            {
                get
                {
                    return _isBaned;
                }
                set
                {
                    _isBaned = value;
                }
            }

            public void ShowInfo()
            {
                Console.WriteLine($"Имя игрока: {_name}, id: {_id}, уровень: {_level}\nЗабанен ли игрок: {_isBaned}\n");
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
                foreach (var player in players)
                    if (player.Id == id)
                        if (player.IsBaned)
                        {
                            Console.WriteLine("Данный игрок уже забанен.");
                            break;
                        }
                        else
                        {
                            player.IsBaned = true;
                            Console.WriteLine("Бан игроку успешно выдан.");
                            break;
                        }
            }

            public void UnbanPlayer(int id)
            {
                foreach (var player in players)
                    if (player.Id == id)
                        if (player.IsBaned)
                        {
                            player.IsBaned = false;
                            Console.WriteLine("Игрок успешно разбанен.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Данный игрок не был забанен.");
                            break;
                        }
            }

            public void ShowAllPlayers()
            {
                foreach (var player in players)
                    player.ShowInfo();

                Console.WriteLine();
            }
        }
    }
}
