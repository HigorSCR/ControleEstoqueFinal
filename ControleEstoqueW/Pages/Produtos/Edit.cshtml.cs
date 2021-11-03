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
using System.Web.Helpers;
using Microsoft.Ajax.Utilities;

namespace ControleEstoqueW.Pages.Produtos
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Produto Produto { get; set; }
        public string Imagem;
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
                HttpResponseMessage response = await client.GetAsync("api/v1/Produtos/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    List<Produto> Produto_ = JsonConvert.DeserializeObject<List<Produto>>(result);
                    Produto = Produto_.First();
                    Imagem = Produto.Imagem;

                }

            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client
                    .PutAsJsonAsync("api/v1/Produtos/" + Produto.ID, Produto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./SucessoCadastro");
                }
                else
                {
                    return Page();
                }
            }
        }
    }
}
