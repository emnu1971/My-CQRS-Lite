using Favv.BeCert.Certificate.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Favv.BeCert.Certificate.Dto
{
    public class DeliverCertificateItemRequestDto
    {
        public Guid CertificateId { get; set; }
        public CertificateStatus Status { get; set; }
    }
}
