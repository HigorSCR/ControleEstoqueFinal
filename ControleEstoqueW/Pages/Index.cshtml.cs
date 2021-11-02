using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ControleEstoqueW.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ControleEstoqueW.Pages
{
    public class IndexModel : PageModel
    {
        public List<Produto> Produtos { get; private set; } = new List<Produto>();
        public int contador;
        public int ContadorFeminino = 0;
        public int ContadorMasculino = 0;
        public int ContadorInfantil = 0;

        string baseUrl = "http://localhost:5000/";
        public async Task OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/v1/Produtos");
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Produtos = JsonConvert.DeserializeObject<List<Produto>>(result);
                    contador = Produtos.Count();
                    foreach (Produto produto in Produtos)
                    {
                        if (produto.Categoria.Contains("Feminino"))
                        {
                            ContadorFeminino += 1;
                        }
                        else if (produto.Categoria.Contains("Masculino"))
                        {
                            ContadorMasculino += 1;
                        }
                        else if (produto.Categoria.Contains("Infantil"))
                        {
                            ContadorInfantil += 1;
                        }
                    }
                }
            }
        }
    }
}
