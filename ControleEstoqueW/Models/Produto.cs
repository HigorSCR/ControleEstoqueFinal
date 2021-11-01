using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleEstoqueW.Models
{

    public class Produto
    {
        public int ID { get; set; }
        public int Status { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public int Quantidade { get; set; }
        public double Custo { get; set; }
        public string Imagem { get; set; }
    }
}
