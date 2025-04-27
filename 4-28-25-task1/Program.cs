using _4_28_25_task1.Model_s;
using _4_28_25_task1.Services;
using DotNetEnv;
using Microsoft.Data;
using Microsoft.Data.SqlClient;

namespace _4_28_25_task1;

internal class Program
{
    static void Main(string[] args)
    {
        Env.Load(@"C:\Users\tural\source\repos\4-28-25-task1\4-28-25-task1\.env");
        using SqlConnection con = new SqlConnection(Env.GetString("DB_CONNECTION_STRING"));
        con.Open();

        Car car = new Car()
        {
            Id = 9,
            Brand = "BMW",
            Model = "X5",
            Year = 2020,
            DailyPrice = 100,
            IsAvailable = true
        };

        Car car1 = new Car()
        {
            Id = 10,
            Brand = "Mercedes",
            Model = "C-Class",
            Year = 2021,
            DailyPrice = 120,
            IsAvailable = true
        };

        Car car2 = new Car()
        {
            Id = 11,
            Brand = "Audi",
            Model = "A6",
            Year = 2022,
            DailyPrice = 150,
            IsAvailable = true
        };

        CarService carService = new CarService(con);

        carService.CreateCars(car, car1, car2);
        carService.DeleteCars(car, car2);

    }


    /*
1.Yeni maşın əlavə et.

2.Yeni müştəri əlavə et.

3.Mövcud olan (IsAvailable = true olan) maşınları siyahıla.

4.Müştəriyə maşın kirayə ver (RentDate bugünkü gün olsun və maşının mövcudluğu d
əyişsin).

5.Maşını geri qaytar (ReturnDate doldurulsun və maşın yenidən mövcud olsun).

6.Bütün kirayə qeydlərini siyahıla (müştəri və maşın adları ilə birlikdə).*/
}
