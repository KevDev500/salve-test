using System;

namespace PatientService.Models;

public readonly record struct Patient(int Id, string FirstName, string LastName, DateTime DateOfBirth);