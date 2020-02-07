using Favv.BeCert.Certificate.Dto;
using System;
using System.Collections.Generic;

namespace Favv.BeCert.Certificate.Domain.ReadModel.Infrastructure
{

    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : In memory database for the read model (for test purpose only)
    /// </summary>
    public static class CertificateInMemoryDatabase
    {
        public static readonly Dictionary<Guid, CertificateItemDetailsDto> Details = new Dictionary<Guid, CertificateItemDetailsDto>();
        public static readonly List<CertificateItemListDto> List = new List<CertificateItemListDto>();
    }
}
