using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagementLoan.Application.Catalogs
{    
    public sealed class CatalogCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Code { get; set; }
    }
}
