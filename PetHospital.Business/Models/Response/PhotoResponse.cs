using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHospital.Business.Models.Response
{
    public class PhotoResponse : BaseResponse.BaseResponse
    {
        public string AnimalId { get; set; } = null!;
        public string FilePath { get; set; } = null!;
    }
}
