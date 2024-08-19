using pro_GestorTareasProyectos.Models;
using System.Text;
using System.Text.Json;


namespace pro_GestorTareasProyectos.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        public LoginService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7175/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

       

        public async Task<Usuario> validarLogin(string email, string contraseña)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/Login/Login?usuario=" + email + "&contraseña=" + contraseña);
            if (response.IsSuccessStatusCode)
            {
                var usuario = await response.Content.ReadAsAsync<Usuario>();
                return usuario;
            }
            else
            {
                return null; // Retorna null si la respuesta no es exitosa
            }
        }

        public async Task<int> RegistrarUsuario(Usuario usuario)
        {
            try
            {
                var json = JsonSerializer.Serialize(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Login/Registrar", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserializa el contenido de la respuesta como un entero
                    if (int.TryParse(responseContent, out int resultado))
                    {
                        return resultado;
                    }
                    else
                    {
                        return -2; // Un código que representa un error en el contenido
                    }
                }
                else
                {
                    return -2; // Un código que representa un error en la solicitud
                }
            }
            catch (Exception)
            {
                return -2; // Un código que representa un error en la solicitud
            }
        }



    }
}
