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

namespace ControleEstoqueW.Pages.Produtos
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Produto Produto { get; set; }

        string baseUrl = "http://localhost:5000/";
        public async Task<IActionResult> OnPostAsync()
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client
                    .PostAsJsonAsync("api/v1/Produtos", Produto);

                if (response.IsSuccessStatusCode)
                {
                    //Produtos/Index
                    return RedirectToPage("./SucessoCadastro");
                }
                else
                {
                    return RedirectToPage("./Create");
                }
            }
        }
    }
}
