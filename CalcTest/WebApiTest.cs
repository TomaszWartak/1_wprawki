using System.Net;
using System.Text;
using ConsoleApp;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace CalcTest;

public class WebApiTest {
    //private readonly ITestOutputHelper output;
    private readonly ConsoleOutputHelper _output;
    
    const string API_URL = "https://jsonplaceholder.typicode.com/posts1";

    public WebApiTest(/*ConsoleOutputHelper output*/)
    {
        this._output = new();
    }

    [Fact]
    public async Task TestPost() {
        
        /*
        Zrób nowy test, który tylko sprawdza zawartość nowego post
        Poźniej GetAll i Get
        Później testy, które sprawdzają rzucanie wyjątkami
        */
        
        
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
                Post? post = JsonConvert.DeserializeObject<Post>(responseContent);
            } else {
                switch (response.StatusCode) {
                    case HttpStatusCode.NotFound:
                    // Handle 404 Not Found
                        _output.WriteLine("404 Not Found");
                        break;
                    case HttpStatusCode.Unauthorized:
                    // Handle 401 Unauthorized
                        _output.WriteLine("401 Unauthorized");
                        break;
                    default:
                        break;
                }
        	}
        } catch( HttpRequestException exception ) {
            _output.WriteLine( exception.Message );
        } catch (TaskCanceledException ex) {
            // Obsłuż anulowanie zadania (timeout)
            _output.WriteLine($"Anulowanie zadania: {ex.Message}");
        } catch (InvalidOperationException ex) {
            // Obsłuż inne błędy związane z niepoprawnym stanem aplikacji
            _output.WriteLine($"Błąd operacji: {ex.Message}");
        } catch (IOException ex) {
            // Obsłuż błędy wejścia-wyjścia
            _output.WriteLine($"Błąd wejścia-wyjścia: {ex.Message}");
        } catch (UriFormatException ex) {
            // Obsłuż błędy związane z niepoprawnym formatem adresu URL
            _output.WriteLine($"Błąd formatu URL: {ex.Message}");
        } catch (Exception ex) {
            // Obsłuż pozostałe wyjątki, które nie zostały wyraźnie obsłużone powyżej
            _output.WriteLine($"Nieoczekiwany wyjątek: {ex.Message}");
        } finally {
            httpClient.Dispose();
        }
        //Assert.Equal( 3, result );
    }
    
    [Fact]
    public async Task PostProperDataShouldReturnNewJsonObject() {
        
        HttpClient httpClient = new();
        NewPostDto newPostDto = new( 2,"title-title", "body-body" );
        string newPostJson = JsonConvert.SerializeObject( newPostDto );
        StringContent jsonContent = new( newPostJson, Encoding.UTF8, "application/json");
        Post? dataFromResponse = null;
        
        try {
            HttpResponseMessage response = await httpClient.PostAsync(API_URL, jsonContent);
            if (response.IsSuccessStatusCode) {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataFromResponse = JsonConvert.DeserializeObject<Post>(responseContent);
            } else {
                switch (response.StatusCode) {
                    case HttpStatusCode.NotFound:
                        _output.WriteLine("404 Not Found");
                        break;
                    case HttpStatusCode.Unauthorized:
                        _output.WriteLine("401 Unauthorized");
                        break;
                    default:
                        break;
                }
        	}
        } catch( Exception ex) {
            // _testOutputHelper.WriteLine($"Nieoczekiwany wyjątek: {ex.Message}");
            _output.WriteLine($"Nieoczekiwany wyjątek: {ex.Message}");
        } finally {
            httpClient.Dispose();
        }
        
        Assert.Equal( new Post( 2,101, "title-title", "body-body"), dataFromResponse );
    }
    
}