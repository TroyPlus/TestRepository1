using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troy.Model;

namespace Troy.Data.Repository
{
    public interface IManufactureRepository
    {
        bool InsertFileUploadDetails(List<Manufacture> manufacture);
    }
}
