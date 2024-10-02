using System.Net.Http;
using TestSaga.WebFrontend.Contracts;

namespace TestSaga.WebFrontend.Services;

public class OrderServiceClient(HttpClient httpClient)
{
	public async Task<string> SubmitOrderAsync(OrderRequest request)
	{
		var response = await httpClient.PostAsJsonAsync("api/orders", request);
		response.EnsureSuccessStatusCode();

		var responseData = await response.Content.ReadFromJsonAsync<OrderResponse>();
		return responseData?.OrderId.ToString() ?? Guid.Empty.ToString();
	}
}