﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient 
    {
        public Patient()
        {
            Diagnoses = new HashSet<Diagnose>();
            Visitations = new HashSet<Visitation>();
            Prescriptions = new HashSet<PatientMedicament>();
        }

        public int PatientId { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(80)]
        [Unicode(false)]
        public string Email { get; set; }
        public bool HasInsurance { get; set; }

        public ICollection<Diagnose> Diagnoses { get; set; }

        public ICollection<Visitation> Visitations { get; set; }

        public ICollection<PatientMedicament> Prescriptions {  get; set; }

    }
}
