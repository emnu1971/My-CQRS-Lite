using System;

namespace Favv.BeCert.Certificate.Common
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 17-06-2019
    /// Purpose     : Represents the status of a certificate
    /// </summary>
    public enum CertificateStatus
    {
        Created = 0,
        Draft,
        Signed,
        Delivered
    }
}
