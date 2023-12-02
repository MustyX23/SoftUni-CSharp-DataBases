namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ExportDtos;
    using Newtonsoft.Json;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text.Json;
    using System.Xml.Linq;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            XmlHelper xmlHelper = new XmlHelper();

            var patients = context.Patients
                .Where(p => p.PatientsMedicines.Count >= 1 && p.PatientsMedicines.Any(pm => pm.Medicine.ProductionDate > DateTime.Parse(date)))  
                .ToArray()
                .Select(p => new ExportPatientDto()
                {
                    Gender = p.Gender.ToString().ToLower(),
                    Name = p.FullName,
                    AgeGroup = p.AgeGroup.ToString(),
                    Medicines = p.PatientsMedicines
                    .Where(pm => pm.Medicine.ProductionDate > DateTime.Parse(date) && pm.Medicine.PatientsMedicines.Count >= 1)
                    .ToArray()
                    .OrderByDescending(pm => pm.Medicine.ExpiryDate)
                    .ThenBy(pm => pm.Medicine.Price)
                    .Select(pm => new ExportMedicineDto()
                    {
                        Category = pm.Medicine.Category.ToString().ToLower(),
                        Name = pm.Medicine.Name,
                        Price = $"{pm.Medicine.Price:f2}",
                        Producer = pm.Medicine.Producer,
                        BestBefore = pm.Medicine.ExpiryDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    })            
                    .ToArray()
                })
                .OrderByDescending(p => p.Medicines.Count())
                .ThenBy(p => p.Name)
                .ToArray();

            return xmlHelper.Serialize<ExportPatientDto[]>(patients, "Patients");
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {
            var medicines = context.Medicines
                .Where(m => m.Category == (Category)medicineCategory &&
                        m.Pharmacy.IsNonStop == true)
                .ToArray()
                .Select(m => new
                {
                    Name = m.Name,
                    Price = $"{m.Price:f2}",
                    Pharmacy = new
                    {
                        Name = m.Pharmacy.Name,
                        PhoneNumber = m.Pharmacy.PhoneNumber
                    }
                })
                .OrderBy(m => m.Price)
                .ThenBy(m => m.Name)
                .ToArray();

            return JsonConvert.SerializeObject(medicines, Formatting.Indented);
        }
    }
}
