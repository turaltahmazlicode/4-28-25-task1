using _4_28_25_task1.Model_s;
using Microsoft.Data.SqlClient;
using System.Data;

namespace _4_28_25_task1.Services;

internal class CarService
{
    public CarService()
    {
        _dt = new DataTable();
    }

    public CarService(SqlConnection connection) : this()
    {
        _connection = connection;
    }

    private readonly SqlConnection _connection;
    private DataTable _dt;

    #region Create
    public int CreateCar(Car car)
    {
        string query = @"INSERT INTO Cars VALUES (@brand, @model, @year, @dailyPrice, @isAvaiable)";
        SqlCommand cmd = new SqlCommand(query, _connection);
        cmd.Parameters.Add(new() { SqlDbType = SqlDbType.NVarChar, ParameterName = "@brand", Value = car.Brand });
        cmd.Parameters.Add(new() { SqlDbType = SqlDbType.NVarChar, ParameterName = "@model", Value = car.Model });
        cmd.Parameters.Add(new() { SqlDbType = SqlDbType.SmallInt, ParameterName = "@year", Value = car.Year });
        cmd.Parameters.Add(new() { SqlDbType = SqlDbType.Decimal, ParameterName = "@dailyPrice", Value = car.DailyPrice });
        cmd.Parameters.Add(new() { SqlDbType = SqlDbType.Bit, ParameterName = "@isAvaiable", Value = car.IsAvailable });
        return cmd.ExecuteNonQuery();
    }
    #endregion

    #region Read
    public DataTable ReadCar()
    {
        string query = @"SELECT * FROM Cars WHERE 1=1";
        using SqlCommand cmd = new SqlCommand(query, _connection);
        using SqlDataAdapter da = new(cmd);
        da.Fill(_dt);
        return _dt;
    }
    #endregion

    #region Update
    public int UpdateCar(Car car)
    {
        string query = @"UPDATE Cars SET Brand = @brand, Model = @model, Year = @year, DailyPrice = @dailyPrice, IsAvailable = @isAvaiable WHERE Id = @id";
        SqlCommand cmd = new SqlCommand(query, _connection);
        cmd.Parameters.Add(new() { SqlDbType = SqlDbType.Int, ParameterName = "@id", Value = car.Id });
        cmd.Parameters.Add(new() { SqlDbType = SqlDbType.NVarChar, ParameterName = "@brand", Value = car.Brand });
        cmd.Parameters.Add(new() { SqlDbType = SqlDbType.NVarChar, ParameterName = "@model", Value = car.Model });
        cmd.Parameters.Add(new() { SqlDbType = SqlDbType.SmallInt, ParameterName = "@year", Value = car.Year });
        cmd.Parameters.Add(new() { SqlDbType = SqlDbType.Decimal, ParameterName = "@dailyPrice", Value = car.DailyPrice });
        cmd.Parameters.Add(new() { SqlDbType = SqlDbType.Bit, ParameterName = "@isAvaiable", Value = car.IsAvailable });
        return cmd.ExecuteNonQuery();
    }
    #endregion

    #region Delete
    public int DeleteCar(Car car)
    {
        string query = @"DELETE FROM Cars WHERE Id = @id";
        SqlCommand cmd = new SqlCommand(query, _connection);
        cmd.Parameters.Add(new() { SqlDbType = SqlDbType.Int, ParameterName = "@id", Value = car.Id });
        return cmd.ExecuteNonQuery();
    }
    #endregion

    #region Methods
    public int CreateCars(params Car[] cars)
    {
        int result = 0;
        foreach (var car in cars)
            result += CreateCar(car);
        return result;
    }

    public int DeleteCars(params Car[] cars)
    {
        int result = 0;
        foreach (var car in cars)
            result += DeleteCar(car);
        return result;
    }

    public void ShowData()
    {
        if (_dt is null)
            ReadCar();

        foreach (var item in _dt.Columns)
            Console.Write("{0} ", item);
        Console.WriteLine();

        foreach (DataRow row in _dt.Rows)
        {
            foreach (var item in row.ItemArray)
                Console.Write("{0} ", item);
            Console.WriteLine();
        }
    }
    #endregion

}
