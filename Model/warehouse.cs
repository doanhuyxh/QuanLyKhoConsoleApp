using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoConsoleApp.Model
{
    public class warehouse
    {
        [Key]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int Quantity { get; set; }
    }
}
