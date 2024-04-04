using System.Net;
using System.Text;
using ConsoleApp;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace CalcTest;

public class WebApiTest {
    private readonly ITestOutputHelper _testOutputHelper;

    public WebApiTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

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
                Post? post = JsonConvert.DeserializeObject<Post>(responseContent);
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
            _testOutputHelper.WriteLine( exception.Message );
        } catch (TaskCanceledException ex) {
            // Obsłuż anulowanie zadania (timeout)
            _testOutputHelper.WriteLine($"Anulowanie zadania: {ex.Message}");
        } catch (InvalidOperationException ex) {
            // Obsłuż inne błędy związane z niepoprawnym stanem aplikacji
            _testOutputHelper.WriteLine($"Błąd operacji: {ex.Message}");
        } catch (IOException ex) {
            // Obsłuż błędy wejścia-wyjścia
            _testOutputHelper.WriteLine($"Błąd wejścia-wyjścia: {ex.Message}");
        } catch (UriFormatException ex) {
            // Obsłuż błędy związane z niepoprawnym formatem adresu URL
            _testOutputHelper.WriteLine($"Błąd formatu URL: {ex.Message}");
        } catch (Exception ex) {
            // Obsłuż pozostałe wyjątki, które nie zostały wyraźnie obsłużone powyżej
            _testOutputHelper.WriteLine($"Nieoczekiwany wyjątek: {ex.Message}");
        } finally {
            httpClient.Dispose();
        }
        //Assert.Equal( 3, result );
    }
}