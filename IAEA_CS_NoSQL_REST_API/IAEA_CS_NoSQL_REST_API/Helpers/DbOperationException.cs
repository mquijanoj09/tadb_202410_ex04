﻿/*
DbOperationException:
Excepcion creada para enviar mensajes relacionados 
con problemas asociados a operaciones en base de datos
*/

namespace IAEA_CS_NoSQL_REST_API.Helpers
{
    public class DbOperationException : Exception
    {
        public DbOperationException(string message) : base(message) { }
    }
}

