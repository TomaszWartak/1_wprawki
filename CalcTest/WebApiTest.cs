using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using ConsoleApp;
public class WebApiTest {
    const string API_URL = "https://jsonplaceholder.typicode.com/posts";
    [Fact]
    public async Task TestPost() {
        HttpClient httpClient = new();
        // userId, title, body 
        NewPostDto newPostDto = new( 2,"title-title", "body-body" );
        string newPostJson = JsonConvert.SerializeObject( newPostDto );
        StringContent jsonContent = new( newPostJson, Encoding.UTF8, "application/json");
        try {
            HttpResponseMessage response = await httpClient.PostAsync(API_URL, jsonContent);
            if (response.IsSuccessStatusCode) {
                string responseContent = await response.Content.ReadAsStringAsync();
                // Process the response data here
                Post post = JsonConvert.DeserializeObject<Post>(responseContent);
        } else {
                switch (response.StatusCode) {
                    case HttpStatusCode.NotFound:
                    // Handle 404 Not Found
                        break;
                    case HttpStatusCode.Unauthorized:
                    // Handle 401 Unauthorized
                        break;
                    default:
                        break;
                }
        	}
        } catch( HttpRequestException exception ) {
            Console.WriteLine( exception.Message );
        } catch (TaskCanceledException ex) {
            // Obsłuż anulowanie zadania (timeout)
            Console.WriteLine($"Anulowanie zadania: {ex.Message}");
        } catch (InvalidOperationException ex) {
            // Obsłuż inne błędy związane z niepoprawnym stanem aplikacji
            Console.WriteLine($"Błąd operacji: {ex.Message}");
        } catch (IOException ex) {
            // Obsłuż błędy wejścia-wyjścia
            Console.WriteLine($"Błąd wejścia-wyjścia: {ex.Message}");
        } catch (UriFormatException ex) {
            // Obsłuż błędy związane z niepoprawnym formatem adresu URL
            Console.WriteLine($"Błąd formatu URL: {ex.Message}");
        } catch (Exception ex) {
            // Obsłuż pozostałe wyjątki, które nie zostały wyraźnie obsłużone powyżej
            Console.WriteLine($"Nieoczekiwany wyjątek: {ex.Message}");
        } finally {
            httpClient.Dispose();
        }
        //Assert.Equal( 3, result );
    }
}