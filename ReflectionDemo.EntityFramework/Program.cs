using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace ReflectionDemo.EntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        static void CrateSQLTable(Type type, string tableName)
        {
            //Create a connection
            var connection = new SqlConnection("Server=.\\SQlExpress;Database=ReflectionDemoDb;Integerated");

            // construct the query
            // 1 Get the properties

            var properties = type.GetProperties();

            var queryBuilder = new StringBuilder();

            queryBuilder.Append($"CREATE TABLE {tableName} (");

            foreach( var property in properties ) 
            {
                // check if the propery is primaray key
                if (property.Name == "id" && property.PropertyType.Name == "Int32")// Auto Incrementd Primery key
                    queryBuilder.Append("Id");
            }

        }
    }
}
