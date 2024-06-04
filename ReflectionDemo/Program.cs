using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace ReflectionDemo

{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var person = new Person();

            //Type PersonType = person.GetType();

            Type personType = typeof(Person);

            var properties = personType.GetProperties();

            foreach (var property in properties)
            {
                Console.WriteLine($"Proterty: Type: {property.PropertyType.Name} | Name : {property.Name}");
            }

            Person person = new();

            var methods = personType.GetMethods();

            foreach (var method in methods)
            {
                Console.WriteLine($"Method : {method.Name} | Return Type: {method.ReturnType}");
                if (method.Name == "Pirnt")
                {
                    method.Invoke(person, null);
                }
            }

            static void AttributeTest(Type type)
            {
                // Get all methods 
                var allMethods = type.GetMethods();
                var methodsWithAttribute = allMethods.Where(m => m.GetCustomAttribute(typeof(MethodForRunAttribute)) != null);

                var obj = Activator.CreateInstance(type);

                foreach (var method in methodsWithAttribute)
                {
                    var attribute = (MethodForRunAttribute)method.GetCustomAttribute(typeof(MethodForRunAttribute));
                    Console.WriteLine($"{method.Name} run for {attribute.RunCount} times");
                    method.Invoke(obj, null);
                    for (int i  = 0; i < attribute.RunCount; i++)
                    {
                        method.Invoke(obj, null);
                    }
                }


            }


            Console.WriteLine("---------------------");
            AttributeTest(typeof(Person));
        }
    }

    public class Person
    {
        //private string _firstName;
        //public string GetFirstName() { return _firstName; } 
        //public void SetFirstName(string firstName) { _firstName = firstName; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int ZipCode { get; set; }

        [MethodForRun(RunCount = 3)]
        public void Pirnt()
        {
            Console.WriteLine($"{FirstName} {LastName}");
        }

        public void Move(int newZipCode)
        {
            ZipCode = newZipCode;
            Console.WriteLine($"{FirstName} {LastName} has been moved to {newZipCode}");
        }

        [MethodForRun(RunCount = 1)]
        public void sayHi()
        {
            Console.WriteLine("Hi");
        }

    }


    public class MethodForRunAttribute : Attribute
    {
        public int RunCount { get; set; }

    }
}
