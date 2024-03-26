//using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace database3
{
    class Program1
    {
        static async Task Main(string[] args)
        {

            string connectionString = "Server=DESKTOP-5BD88QO\\SQLEXPRESS;Database=school;Trusted_Connection=True;";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                int n;
                SqlCommand command;
                SqlDataReader reader;
                int i = Convert.ToInt32(Console.ReadLine());
                while (i == 1)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine($"Для того чтобы изменить стоимость - 1\nДля того чтобы обновить - 2 \nДля удаления заказа - 3\nДля того чтобы добавить заказ - 4\nДля того чтобы Осортировать по стоимости - 5\nДля того чтобы осортировать по числу - 6\nДля того чтобы отсортировать по имени - 7\nДля того чтобы Вывести среднию цену товара - 8");

                        n = Convert.ToInt32(Console.ReadLine());

                        switch (n)
                        {
                            case 1: update(connection); break;
                            case 3: delete(connection); break;
                            case 8: Avg(connection); break;
                            case 4: add(connection); break;
                            case 2: command = new SqlCommand("SELECT * FROM Zakaz", connection); reader = await command.ExecuteReaderAsync(); search(reader); break;
                            case 5: command = new SqlCommand("SELECT * FROM Zakaz ORDER BY Cost", connection); reader = await command.ExecuteReaderAsync(); search(reader); break;
                            case 6: command = new SqlCommand("SELECT * FROM Zakaz ORDER BY Enemy", connection); reader = await command.ExecuteReaderAsync(); search(reader); break;
                            case 7: command = new SqlCommand("SELECT * FROM Zakaz ORDER BY Item", connection); reader = await command.ExecuteReaderAsync(); search(reader); break;
                        }


                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    } while (n != 0);
                }
                while (i == 2)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine($"Для того чтобы изменить стоимость - 1\nДля того чтобы обновить - 2 \nДля удаления заказа - 3\nДля того чтобы добавить заказ - 4\nДля того чтобы Осортировать по стоимости - 5\nДля того чтобы осортировать по числу - 6\nДля того чтобы отсортировать по имени - 7\nДля того чтобы Вывести среднию цену товара - 8");

                        n = Convert.ToInt32(Console.ReadLine());

                        switch (n)
                        {
                            case 1: update(connection); break;
                            case 3: delete(connection); break;
                            case 8: Avg(connection); break;
                            case 4: add(connection); break;
                            case 2: command = new SqlCommand("SELECT * FROM Zakaz", connection); reader = await command.ExecuteReaderAsync(); search(reader); break;
                            case 5: command = new SqlCommand("SELECT * FROM Zakaz ORDER BY Cost", connection); reader = await command.ExecuteReaderAsync(); search(reader); break;
                            case 6: command = new SqlCommand("SELECT * FROM Zakaz ORDER BY Enemy", connection); reader = await command.ExecuteReaderAsync(); search(reader); break;
                            case 7: command = new SqlCommand("SELECT * FROM Zakaz ORDER BY Item", connection); reader = await command.ExecuteReaderAsync(); search(reader); break;
                        }


                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    } while (n != 0);
                }
            }

            Console.Read();
        }
        static async void search(SqlDataReader reader)
        {

            if (reader.HasRows) // если есть данные
            {

                string column1 = reader.GetName(0);
                string column2 = reader.GetName(1);
                string column3 = reader.GetName(2);
                


                Console.WriteLine($"{column1}\t      {column2}\t                   {column3}\t           ");

                while (await reader.ReadAsync()) // построчно считываем данные
                {
                    object ID = reader.GetValue(0);
                    object Student = reader.GetValue(1);
                    object Class = reader.GetValue(2);


                    Console.WriteLine($"{ID}\t      {Student}\t                   {Class}                  ");
                }

            }
            await reader.CloseAsync();
        }

        static async void Avg(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT AVG(Cost) FROM Zakaz", connection);
            double averageCost = Convert.ToDouble(await command.ExecuteScalarAsync());

            Console.WriteLine($"Средняя стоимость товаров: {averageCost:F2}");
        }
        static async void add(SqlConnection connection)
        {
            Console.WriteLine("Введите Товар");
            string Item = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите Кол-во");
            string Enemy = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите Стоимость");
            string Cost = Convert.ToString(Console.ReadLine());
            SqlCommand command = new SqlCommand($"INSERT INTO Zakaz (Item, Enemy, Cost) VALUES ('{Item}','{Enemy}','{Cost}')", connection);
            int number = await command.ExecuteNonQueryAsync();
            Console.WriteLine($"Добавлено объектов: {number}");
        }
        static async void delete(SqlConnection connection)
        {
            int ID = Convert.ToInt32(Console.ReadLine());
            SqlCommand command = new SqlCommand($"DELETE  FROM Zakaz WHERE ID = {ID}", connection);
            int number = await command.ExecuteNonQueryAsync();
            Console.WriteLine($"Удалено объектов: {number}");
        }
        static async void update(SqlConnection connection)
        {
            Console.WriteLine("Введите ID Предмета");
            int ID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите Cost");
            double Cost = double.Parse(Console.ReadLine());
            Console.WriteLine(Cost);
            SqlCommand command = new SqlCommand($"UPDATE Zakaz SET Cost={Cost.ToString().Replace(',', '.')} WHERE ID={ID}", connection);
            int number = await command.ExecuteNonQueryAsync();
            Console.WriteLine($"Обновлено объектов: {number}");
        }

    }
}