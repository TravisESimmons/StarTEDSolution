﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace StarTEDSystem.Entities
{
    public partial class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateHired { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int PositionID { get; set; }
        public int ProgramID { get; set; }
        public string LoginID { get; set; }

        public virtual Position Position { get; set; }
        public virtual Programs Program { get; set; }
    }
}