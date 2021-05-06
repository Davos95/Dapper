using DapperRepositoryBase;
using ExampleDapper.Model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ExampleDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
        }


        //This Method is only an example of syntax

        private static void ExampleTransaction()
        {
            using (var dapper = new DapperBase())
            {
                try
                {
                    dapper.StartTransaction();

                    var cars = new Car().GetCarsDefault();

                    foreach (var car in cars)
                    {
                        dapper.SetExecuteInTransaction<object>("InsertProdecureExample", new { car.Brand, car.Model, car.Color });
                    }

                    dapper.CommitTransaction();
                }
                catch(Exception ex)
                {
                    dapper.RollbackTransaction();

                    throw ex;
                }
            }
        }

        

    }
}
