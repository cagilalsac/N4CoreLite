using Business.Models.Reports;
using N4CoreLite.Responses.Bases;

namespace Business.Services.Reports.Bases
{
    public interface IOgrenciRaporuService
    {
        public Task<Response<OgrenciRaporuViewModel>> Getir(OgrenciRaporuViewModel viewModel = null, CancellationToken cancellationToken = default);
    }
}
