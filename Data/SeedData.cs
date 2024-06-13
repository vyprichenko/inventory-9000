using InventoryApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApplication.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new InventoryApplicationContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<InventoryApplicationContext>>()))
        {
            if (context.Supplier.Any() == false)
            {
                context.Supplier.AddRange(
                    new Supplier
                    {
                        Name = "КиївСпец2000"
                    },
                    new Supplier
                    {
                        Name = "Промавтоматика"
                    },
                    new Supplier
                    {
                        Name = "Космотек"
                    }
                );
            }
            if (context.Employee.Any() == false)
            {
                context.Employee.AddRange(
                    new Employee
                    {
                        FirstName = "Дмитро Випріченко"
                    },
                    new Employee
                    {
                        FirstName = "Марія Ткаченко"
                    }
                );
            }
            if (context.Department.Any() == false)
            {
                context.Department.AddRange(
                    new Department
                    {
                        Name = "Головний офіс"
                    },
                    new Department
                    {
                        Name = "Сервісний центр"
                    }
                );
            }
            if (context.Operation.Any() == false)
            {
                context.Operation.AddRange(
                    new Operation
                    {
                        Name = "Видача зі складу"
                    },
                    new Operation
                    {
                        Name = "Повернення на склад"
                    },
                    new Operation
                    {
                        Name = "Видача на ремонт"
                    },
                    new Operation
                    {
                        Name = "Повернення з ремонту"
                    }
                );
            }
            if (context.Inventory.Any() == false)
            {
                context.Inventory.AddRange(
                    new Inventory
                    {
                        Name = "Dell XPS 9500",
                        SerialNumber = "SN87362885"
                    },
                    new Inventory
                    {
                        Name = "Lenovo Carbon X1",
                        SerialNumber = "SN90624334"
                    },
                    new Inventory
                    {
                        Name = "Baseus PowerCombo Tower 2AC",
                        SerialNumber = "SN09887741"
                    }
                 );
            }
            context.SaveChanges();
        }
    }
}