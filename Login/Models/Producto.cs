using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Login.Models
{
    public partial class Producto
    {
        
        [Key]
        public int Id { get; set; }
        
        public string Nombre { get; set; }
        public string Marca { get; set;}
        public string Usuario { get; set; }
    }
}
