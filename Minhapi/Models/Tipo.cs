using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Minhaapi.models;

public class Tipo
{    
    public int IdProduto { get; set;}

    public string NomeProduto { get; set;}
    = string.Empty;
   
}

