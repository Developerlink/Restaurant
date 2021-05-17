using RestaurantLibrary.DataAccess;
using RestaurantLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantCL
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlConnector db = new SqlConnector(); 

            DinnerTable newTable = new DinnerTable("31");

            Console.WriteLine(db.CreateDinnerTable(newTable));

            

            List<DinnerTable> buffetTables = new List<DinnerTable>();
            List<DinnerTable> sushiTables = new List<DinnerTable>();

            for (int i = 1; i <= 50; i++)
            {
                buffetTables.Add(new DinnerTable() { Id = i, Name = i.ToString(), Seats = 2 });
            }

            for (int i = 1; i <= 30; i++)
            {
                sushiTables.Add(new DinnerTable() { Id = i, Name = i.ToString(), Seats = 2 });
            }

            Area buffet = new Area() { Id = 1, Name = "buffet", DinnerTables = buffetTables };
            Area sushi = new Area() { Id = 2, Name = "sushi", DinnerTables = sushiTables };

            List<Area> areas = new List<Area>();
            areas.Add(buffet);
            areas.Add(sushi);

            Restaurant bamboo = new Restaurant() { Id = 1, Name = "Bamboo", PhoneNumber = 12345678, Areas = areas };

            //Console.WriteLine(bamboo.ToString());

            //SqlConnector conn = new SqlConnector();
            //string result = conn.Connect();
            //Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
