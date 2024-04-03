namespace Basic;

using Newtonsoft.Json;
using System.IO;

public class ConsoleApp {

    
    //const string API_URL = "https://api.example.com/data";
    const string API_URL = "https://jsonplaceholder.typicode.com/posts";

    public static async Task Main( string[] args) {
        await Get(1);
    }
 
    static async Task Get( int postId ) {
        HttpClient httpClient = new();
        string apiUrlGetPost = Path.Combine( API_URL, postId.ToString() );
        try {
            HttpResponseMessage response = await httpClient.GetAsync( apiUrlGetPost );
            if (response.IsSuccessStatusCode) {
                string responseContent = await response.Content.ReadAsStringAsync();
                // Process the response data here
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
    }
 
    // static async void GetAll() {
    //     HttpClient httpClient = new();
    //     try {
    //         HttpResponseMessage response = await httpClient.GetAsync(API_URL);
    //         if (response.IsSuccessStatusCode) {
    //             string responseContent = await response.Content.ReadAsStringAsync();
    //             // Process the response data here
    //      }
    //     } catch( ??? z ChatGPT) {
    //         Console.WriteLine(“”);
    //     } finally {
    //      httpClient.Dispose ();
    //     }
    // }
 
    // static async void Post ( int userId, string title, string body ){
    //     HttpClient httpClient = new();
    //     NewPostDto newPostDto = new( userId, title, body );
    //     String newPostJson = Json.Stringify( newPostDto );
    //     StringContent jsonContent = new( newPostJson, Encoding.UTF8, "application/json");
    //     try {
    //         HttpResponseMessage response = await httpClient.PostAsync(API_URL, jsonContent);
    //         if (response.IsSuccessStatusCode) {
    //             string responseContent = await response.Content.ReadAsStringAsync();
    //             // Process the response data here
    //     } else {
    //         switch (response.StatusCode) {
    //                     HttpStatusCode.NotFound
    //                                 // Handle 404 Not Found
    //                     HttpStatusCode.Unauthorized
    //                                 // Handle 401 Unauthorized
    //         }
    //     }
    //     } catch( ??? z ChatGPT) {
    //         Console.WriteLine(“”);
    //     } finally {
    //         httpClient.Dispose ();
    //     }
    // }
 
}
