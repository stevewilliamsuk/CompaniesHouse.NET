using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LiberisLabs.CompaniesHouse.Request;
using LiberisLabs.CompaniesHouse.Response.CompanySearch;
using LiberisLabs.CompaniesHouse.UriBuilders;

namespace LiberisLabs.CompaniesHouse
{
    public class CompaniesHouseSearchCompanyClient : ICompaniesHouseSearchCompanyClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICompanySearchUriBuilder _companySearchUriBuilder;

        public CompaniesHouseSearchCompanyClient(IHttpClientFactory httpClientFactory, ICompanySearchUriBuilder companySearchUriBuilder)
        {
            _httpClientFactory = httpClientFactory;
            _companySearchUriBuilder = companySearchUriBuilder;
        }

        public async Task<CompaniesHouseClientResponse<CompanySearch>> SearchCompanyAsync(CompanySearchRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var httpClient = _httpClientFactory.CreateHttpClient())
            {
                var requestUri = _companySearchUriBuilder.Build(request);

                var response = await httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsAsync<CompanySearch>(cancellationToken).ConfigureAwait(false);

                return new CompaniesHouseClientResponse<CompanySearch>(result);
            }

        }
    }
}