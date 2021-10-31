using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ControleEstoqueW.Models;

namespace ControleEstoqueW.Pages.Produtos
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Produto Produto { get; set; }
        string baseUrl = "http://localhost:5000/";
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                //Fazendo método GET http://localhost:44387/api/v1/Produto/{id}
                HttpResponseMessage response = await client.GetAsync("api/v1/Produto/" + id);

                //Booleano que nos diz se deu certo ou se teve algum erro
                if (response.IsSuccessStatusCode)
                {
                    //Captura a string do json
                    string result = response.Content.ReadAsStringAsync().Result;
                    //Tranformar a string json em um objeto do tipo Produto
                    Produto = JsonConvert.DeserializeObject<Produto>(result);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (Produto.ID != id)
            {
                return BadRequest();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client
                    .DeleteAsync("api/v1/Produto/" + Produto.ID);
                if (response.IsSuccessStatusCode)
                {
                    //Sucesso! Quero ir para a minha página http://localhost:port/Produtos
                    return RedirectToPage("./Index");
                }
                else
                {
                    return Page();
                }
            }
        }
    }
}
