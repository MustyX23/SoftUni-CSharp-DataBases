using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new CarDealerContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            ImportData(db);

            //testing all the exports here:
            Console.WriteLine(GetSalesWithAppliedDiscount(db));
        }
        public static void ImportData(CarDealerContext context)
        {
            string supplierInputJSON = File.ReadAllText(@"../../../Datasets/suppliers.json");
            ImportSuppliers(context, supplierInputJSON);

            string partsInputJSON = File.ReadAllText(@"../../../Datasets/parts.json");
            ImportParts(context, partsInputJSON);

            string carsInputJSON = File.ReadAllText(@"../../../Datasets/cars.json");
            ImportCars(context, carsInputJSON);

            string customersInputJSON = File.ReadAllText(@"../../../Datasets/customers.json");
            ImportCustomers(context, customersInputJSON);

            string salesInputJSON = File.ReadAllText(@"../../../Datasets/sales.json");
            ImportSales(context, salesInputJSON);
        } 
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}.";
        }
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<Part[]>(inputJson)
                .Where(p => p.SupplierId <= 31);

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count()}.";
        }
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert.DeserializeObject<List<CarDTO>>(inputJson);

            foreach (var car in cars)
            {
                Car currentCar = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TraveledDistance = car.TravelledDistance
                };

                foreach (var part in car.PartsId)
                {
                    bool isValid = currentCar.PartsCars.FirstOrDefault(x => x.PartId == part) == null;
                    bool isPartValid = context.Parts.FirstOrDefault(p => p.Id == part) != null;

                    if (isValid && isPartValid)
                    {
                        currentCar.PartsCars.Add(new PartCar()
                        {
                            PartId = part
                        });
                    }
                }

                context.Cars.Add(currentCar);
            }

            context.SaveChanges();

            return $"Successfully imported {context.Cars.Count()}.";
        }
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count()}.";
        }
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count()}.";
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                    IsYoungDriver = c.IsYoungDriver

                })
                .ToList();

            return JsonConvert.SerializeObject(customers, Formatting.Indented);
        }
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .Select(c => new
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance
                })
                .ToArray();


            return JsonConvert.SerializeObject(cars, Formatting.Indented);

        }
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            return JsonConvert.SerializeObject (suppliers, Formatting.Indented);
        }
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TraveledDistance                       
                    },
                    parts = c.PartsCars.Select(ps => new
                    {
                        Name = ps.Part.Name,
                        Price = $"{ps.Part.Price:f2}"
                    })

                })
                .ToList();

            return JsonConvert.SerializeObject(cars, Formatting.Indented);
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count() > 0)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count(),
                    spentMoney = c.Sales.Sum(s => s.Car.PartsCars.Sum(pc => pc.Part.Price))
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars)
                .ToArray();

            var jsonFile = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return jsonFile;
        }
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TraveledDistance = s.Car.TraveledDistance
                    },
                    customerName = s.Customer.Name,
                    discount = $"{s.Discount:F2}",
                    price = $"{s.Car.PartsCars.Sum(pc => pc.Part.Price):F2}",
                    priceWithDiscount = $"{s.Car.PartsCars.Sum(pc => pc.Part.Price) * (1 - (s.Discount / 100)):F2}",
                })
                .ToArray();

            string salesToJson = JsonConvert.SerializeObject(sales, Formatting.Indented);
            return salesToJson;
        }
    }
}