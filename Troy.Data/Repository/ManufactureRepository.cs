using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troy.Data.DataContext;
using Troy.Model;

namespace Troy.Data.Repository
{
    public class ManufactureRepository : BaseRepository, IManufactureRepository
    {
        private ManufactureContext manufactureContext = new ManufactureContext();

        public bool InsertFileUploadDetails(List<Manufacture> manufacture)
        {
            try
            {
                manufactureContext.Manufacture.AddRange(manufacture);
                manufactureContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
