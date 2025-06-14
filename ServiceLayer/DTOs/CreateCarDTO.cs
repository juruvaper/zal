using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public record CreateCarDTO(string brand, string model, int horsePower);
    
}
