using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Internal;

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

            Console.WriteLine(GetCarsWithDistance(db));
        }
        private static IMapper InitializeAutoMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();

            }));
           
        }

        public static void ImportData(CarDealerContext context)
        {
            string suppliersXML = File.ReadAllText(@"../../../Datasets/suppliers.xml");
            ImportSuppliers(context, suppliersXML);

            string partsXML = File.ReadAllText(@"../../../Datasets/parts.xml");
            ImportParts(context, partsXML);

            string carsXML = File.ReadAllText(@"../../../Datasets/cars.xml");
            ImportCars(context, carsXML);

            string customersXML = File.ReadAllText(@"../../../Datasets/customers.xml");
            ImportCustomers(context, customersXML);

            string salesXML = File.ReadAllText(@"../../../Datasets/sales.xml");
            ImportSales(context, salesXML);
        }
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();

            XmlHelper xmlHelper = new XmlHelper();

            var suppliers = new HashSet<Supplier>(); 

            var supplierDTOS = xmlHelper.Deserialize<ImportSupplierDTO[]>(inputXml, "Suppliers");

            foreach (var supplierDTO in supplierDTOS)
            {
                if (string.IsNullOrEmpty(supplierDTO.Name))
                {
                    continue;
                }

                Supplier supplier = mapper.Map<Supplier>(supplierDTO);

                suppliers.Add(supplier);
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}";
        }
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlHelper xmlHelper = new XmlHelper();

            IMapper mapper = InitializeAutoMapper();

            var parts = new HashSet<Part>();

            var partsDTOS = xmlHelper.Deserialize<ImportPartsDTO[]>(inputXml, "Parts");

            foreach (var partsDTO in partsDTOS)
            {
                if (string.IsNullOrEmpty(partsDTO.Name))
                {
                    continue;
                }

                if (!partsDTO.SupplierId.HasValue || !context.Suppliers.Any(s => s.Id == partsDTO.SupplierId))
                {
                    continue;
                }

                Part part = mapper.Map<Part>(partsDTO);

                parts.Add(part);
            }


            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlHelper xmlHelper = new XmlHelper();

            IMapper mapper = InitializeAutoMapper();

            var cars = new HashSet<Car>();

            var carDTOS = xmlHelper.Deserialize<ImportCarDTO[]>(inputXml, "Cars");

            foreach(var carDTO in carDTOS)
            {
                if (string.IsNullOrEmpty(carDTO.Make) || string.IsNullOrEmpty(carDTO.Model))
                {
                    continue;
                }

                Car car = mapper.Map<Car>(carDTO);

                foreach (var part in carDTO.Parts.DistinctBy(p => p.PartId))
                {
                    if (!context.Parts.Any(p => p.Id == part.PartId))
                    {
                        continue;
                    }

                    PartCar carPart = new PartCar()
                    {
                        PartId = part.PartId
                    };

                    car.PartsCars.Add(carPart);
                }

                cars.Add(car);
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();
            
            return $"Successfully imported {cars.Count()}";

        }
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            XmlHelper xmlHelper = new XmlHelper();

            IMapper mapper = InitializeAutoMapper();

            var customers = new HashSet<Customer>();

            var customerDTOS = xmlHelper.Deserialize<ImportCustomerDTO[]>(inputXml, "Customers");

            foreach (var customerDTO in customerDTOS)
            {
                if (string.IsNullOrEmpty(customerDTO.Name))
                {
                    continue;
                }

                Customer customer = mapper.Map<Customer>(customerDTO);                
                customers.Add(customer);
            }
            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
            
        }
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlHelper xmlHelper = new XmlHelper();

            IMapper mapper = InitializeAutoMapper();

            var sales = new HashSet<Sale>();
            var salesDTOS = xmlHelper.Deserialize<ImportSaleDTO[]>(inputXml, "Sales");

            foreach(var salesDTO in salesDTOS)
            {
                if (!salesDTO.CarId.HasValue || context.Cars.All(c => c.Id != salesDTO.CarId))
                {
                    continue;
                }

                Sale sale = mapper.Map<Sale>(salesDTO);
                sales.Add(sale);
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            IMapper mapper = InitializeAutoMapper();

            ExportCarDTO[] cars = context.Cars
                .Where(c => c.TraveledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<ExportCarDTO>(mapper.ConfigurationProvider)
                .ToArray();

            return xmlHelper.Serialize<ExportCarDTO[]>(cars, "cars");
        }
    }
}