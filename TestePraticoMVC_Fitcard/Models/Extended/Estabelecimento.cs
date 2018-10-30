using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TestePraticoMVC_Fitcard.Models
{
    [MetadataType(typeof(EstabelecimentoMetadata))]
    public partial class Estabelecimento
    {

    }

    public class EstabelecimentoMetadata
    {       
        [Required(AllowEmptyStrings = false, ErrorMessage = "Preencha o campo Razão")]
        public string razao { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Preencha o campo CNPJ")]
        public string CNPJ { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um E-mail válido")]
        public string email { get; set; }
    }
}