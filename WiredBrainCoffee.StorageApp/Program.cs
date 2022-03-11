using System;
using WiredBrainCoffee.StorageApp.Data;
using WiredBrainCoffee.StorageApp.Entities;
using WiredBrainCoffee.StorageApp.Repositories;

namespace WiredBrainCoffee.StorageApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // ItemAdded<Employee> itemAdded = EmployeeAdded; (new ItemAdded<Employee>(EmployeeAdded));
            var employeeRepository = new SqlRepository<Employee>(new StorageAppDbContext()); // removed EmployeeAdded since the constructor doesnt have the parameter anymore - was orginally: var employeeRepository = new SqlRepository<Employee>(new StorageAppDbContext(),EmployeeAdded);
            employeeRepository.ItemAdded += EmployeeRepository_ItemAdded;
            AddEmployees(employeeRepository);
            AddManagers(employeeRepository);
            GetEmployeeById(employeeRepository);
            WriteAllToConsole(employeeRepository);

            var organizationRepository = new ListRepository<Organization>();
            AddOrganizations(organizationRepository);
            WriteAllToConsole(organizationRepository);

            Console.ReadLine();
        }

        private static void EmployeeRepository_ItemAdded(object? sender, Employee e)
        {
            Console.WriteLine($"Employee added => {e.FirstName}");
        }

        /* private static void EmployeeAdded(Employee employee)
        {
            var employee = (Employee)item;
            Console.WriteLine($"Employee added => {employee.FirstName}");
        } */

        private static void AddManagers(IWriteRepository<Manager> managerRepository)
        {
            var saraManager = new Manager { FirstName = "Sara" };
            var saraManagerCopy = saraManager.Copy();
            managerRepository.Add(saraManager);

            if(saraManagerCopy is not null)
            {
                saraManagerCopy.FirstName += " _Copy";
                managerRepository.Add(saraManagerCopy);
            }

            managerRepository.Add(new Manager { FirstName = "Henry" });
            managerRepository.Save();
        }

        private static void WriteAllToConsole(IReadRepository<IEntity> repository)
        {
            var items = repository.GetAll();
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        private static void GetEmployeeById(IRepository<Employee> employeeRepository)
        {
            var employee = employeeRepository.GetById(2);
            Console.WriteLine($"Employee with Id 2: {employee.FirstName}");
        }

        private static void AddEmployees(IRepository<Employee> employeeRepository)
        {
            var employees = new[]
            {
                new Employee { FirstName = "Julia" },
                new Employee { FirstName = "Anna" },
                new Employee { FirstName = "Thomas" }
            };
            employeeRepository.AddBatch(employees);             // RepositoryExtensions.AddBatch(employeeRepository, employees);
        }

        private static void AddOrganizations(IRepository<Organization> organizationRepository)
        {
            var organizations = new[]
            {
                new Organization { Name = "Pluralsight" },
                new Organization { Name = "Globomantics" }
            };
            organizationRepository.AddBatch(organizations);     // RepositoryExtensions.AddBatch(organizationRepository, organizations);
        }
    }
}


/* 

Code from a Pluralsight course on C# Generics:
(https://app.pluralsight.com/library/courses/c-sharp-generics/table-of-contents)

*** Part 3. Implementing Generic classes ***
    
    - Implement a generic class
        * Inherit from a generic class
        * Use multiple type parameters
     
    - Add generic type constraints
        * Use a concrete class (EntityBase)
        * Work with class and struct constraints
        * Call constructor with new() constraint
 
    - Use a default keyword


*** Part 4. Working with Generic Interfaces ***

    - Create and use a generic interface
    - Generic type parameters are invariant
    - Declare type parameter as covariant
        * <out T> - works if T is used only for return values 
        * Use a less specific generic type argument on the generic interface
    - Declare type parameter as contravariant
        * <in T> - works if T is used only for input parameters
        * Use a more specific generic type argument on the generic interface
    - Work with interface inheritance 


*** Part 5. Creating Generic Methods and Delegates ***

    - Create and use generic method
        * Build a generic extension method for a generic interface
        * Use type constraints
    - Create a and use a generic delegate
        * Understand covariance and contravariance
    - Work with the existing delegates like Action<T> and EventHandler<T>

 */