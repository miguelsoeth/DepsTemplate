﻿using System.Collections.Generic;

namespace DepsTemplate.Web.Endpoints.PerfilEndpoints
{
    public class UpdateOrdenacaoPerfilRequest
    {
        public const string Route = "perfil/ordenacao";

        public List<OrdenacaoPerfil> Ordenacao { get; set; }
    }

    public class OrdenacaoPerfil
    {
        public int Id { get; set; }
        public int Ordem { get; set; }
    }
}
