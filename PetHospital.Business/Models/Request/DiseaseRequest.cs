﻿
namespace PetHospital.Business.Models.Request
{
    public class DiseaseRequest
    {
        public string NameOfDisease { get; set; } = string.Empty;
        public string DiseaseDescription { get; set; } = string.Empty;
    }
}