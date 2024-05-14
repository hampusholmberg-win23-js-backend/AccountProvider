﻿using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class AddressEntity
{
    [Key]
    public int Id { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
}